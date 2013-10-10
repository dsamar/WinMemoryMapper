using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace WinMemoryMapper
{
    public class MemoryMapper : IMemoryMapper
    {
        /// <summary>
        /// The defaul t_ memor y_ size
        /// </summary>
        private const int DEFAULT_MEMORY_SIZE = 0x1000;

        /// <summary>
        /// Reads the process memory.
        /// </summary>
        /// <param name="hProcess">The h process.</param>
        /// <param name="lpBaseAddress">The lp base address.</param>
        /// <param name="lpBuffer">The lp buffer.</param>
        /// <param name="dwSize">Size of the dw.</param>
        /// <param name="lpNumberOfBytesRead">The lp number of bytes read.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, uint lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        /// <summary>
        /// Writes the process memory.
        /// </summary>
        /// <param name="hProcess">The h process.</param>
        /// <param name="lpBaseAddress">The lp base address.</param>
        /// <param name="lpBuffer">The lp buffer.</param>
        /// <param name="nSize">Size of the n.</param>
        /// <param name="lpNumberOfBytesWritten">The lp number of bytes written.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, uint lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        /// <summary>
        /// Opens the process.
        /// </summary>
        /// <param name="dwDesiredAccess">The dw desired access.</param>
        /// <param name="bInheritHandle">if set to <c>true</c> [b inherit handle].</param>
        /// <param name="dwProcessId">The dw process identifier.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll"), SuppressUnmanagedCodeSecurity]
        internal static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>
        /// The process.
        /// </value>
        public Process Process { get; set; }

        /// <summary>
        /// Maps the memory.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <param name="valueConfigFile">The value configuration file.</param>
        /// <returns></returns>
        public IMemContainer MapMemory(Process process, string valueConfigFile)
        {
            // Open process for reading
            this.Process = process;
            var xdoc = XDocument.Load(valueConfigFile);
            var entries = from e in xdoc.Descendants("val")
                          select new
                          {
                              Key = (string)e.Attribute("key"),
                              Type = (Type)Type.GetType(e.Attribute("type").ToString()),
                              Address = uint.Parse(e.Attribute("address").ToString(), NumberStyles.HexNumber)
                          };

            var ret = new GenericContainer();
            foreach (var e in entries)
            {
                var val = this.Read(e.Type, e.Address);
                ret.Values.Add(e.Key, val);
            }

            return ret;
        }

        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public byte[] ReadBytes(uint address, int count)
        {
            var ret = new byte[count];
            int numRead;
            if (ReadProcessMemory(this.Process.Handle, address, ret, count, out numRead) && numRead == count)
            {
                return ret;
            }
            return null;
        }

        /// <summary>
        /// Reads the specified addresses.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        public object Read(Type t, params uint[] addresses)
        {
            if (addresses.Length == 0)
            {
                return Activator.CreateInstance(t);
            }
            if (addresses.Length == 1)
            {
                return ReadInternal(t, addresses[0]);
            }

            uint last = 0;
            for (int i = 0; i < addresses.Length; i++)
            {
                if (i == addresses.Length - 1)
                {
                    return ReadInternal(t, addresses[i] + last);
                }
                last = (uint)ReadInternal(typeof(uint), last + addresses[i]);
            }

            return Activator.CreateInstance(t);
        }

        //private void example()
        //{
        //    int ObjectManagerPtr = MR.ReadInt(ObjectManagerAddress);
        //    int ObjectManagerStoragePtr = ObjectManagerPtr + ObjectManager_ofs_storage;
        //    int ObjectManagerStorageDataPtr = MR.ReadInt(ObjectManagerStoragePtr + ObjectManagerStorage_ofs__data);
        //    int ObjectManagerStorageLocalPtr = MR.ReadInt(ObjectManagerStoragePtr + ObjectManagerStorage_ofs__local);
        //    int MyPlayerIndex = MR.ReadInt(ObjectManagerStorageLocalPtr);

        //    for (int i = 0; i < 4; i++)
        //    {
        //        uint seed = MR.ReadUInt(ObjectManagerStorageDataPtr + i * player_data_size + 0x58 + player_ofs__seed);
        //        if (seed == 0) continue;
        //        string CharacterName = MR.ReadString(ObjectManagerStorageDataPtr + i * player_data_size + 0x58 + player_ofs__name, 12 + 1, Encoding.ASCII, true);
        //        uint class_idx = MR.ReadUInt(ObjectManagerStorageDataPtr + i * player_data_size + 0x58 + player_ofs__class); // 0=DH, 1=barb, 2=wiz, 3=wd, 4=monk

        //        uint levelarea_id = MR.ReadUInt(ObjectManagerStorageDataPtr + i * player_data_size + 0x58 + player_ofs__area);
        //        if (MyPlayerIndex == i)
        //        {
        //            // tadamm: this is our player's levelarea_id and other stuff
        //        }

        //        uint ActorID = MR.ReadUInt(ObjectManagerStorageDataPtr + i * player_data_size + 0x58 + player_ofs__actor_id);
        //        if ((ActorID == 0) || (ActorID == uint.MaxValue))
        //        {
        //            // our player's actor is always valid, but party members too far from us will "disappear"
        //        }
        //    }
        //}

        /// <summary>
        /// Reads the internal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        private object ReadInternal(Type t, uint address)
        {
            object ret;
            try
            {
                if (t == typeof(string))
                {
                    var chars = new List<char>();
                    uint offset = 0;
                    char lastChar;
                    while ((lastChar = (char)Read(typeof(byte), address + offset)) != '\0')
                    {
                        offset++;
                        chars.Add(lastChar);
                    }
                    ret = new string(chars.ToArray());

                    return ret;
                }

                int numBytes = Marshal.SizeOf(t);
                if (t == typeof(IntPtr))
                {
                    ret = (IntPtr)BitConverter.ToInt64(ReadBytes(address, numBytes), 0);
                    return ret;
                }

                else
                {
                    switch (Type.GetTypeCode(t))
                    {
                        case TypeCode.Boolean:
                            ret = ReadBytes(address, 1)[0] != 0;
                            break;
                        case TypeCode.Char:
                            ret = (char)ReadBytes(address, 1)[0];
                            break;
                        case TypeCode.SByte:
                            ret = (sbyte)ReadBytes(address, numBytes)[0];
                            break;
                        case TypeCode.Byte:
                            ret = ReadBytes(address, numBytes)[0];
                            break;
                        case TypeCode.Int16:
                            ret = BitConverter.ToInt16(ReadBytes(address, numBytes), 0);
                            break;
                        case TypeCode.UInt16:
                            ret = BitConverter.ToUInt16(ReadBytes(address, numBytes), 0);
                            break;
                        case TypeCode.Int32:
                            ret = BitConverter.ToInt32(ReadBytes(address, numBytes), 0);
                            break;
                        case TypeCode.UInt32:
                            ret = BitConverter.ToUInt32(ReadBytes(address, numBytes), 0);
                            break;
                        case TypeCode.Int64:
                            ret = BitConverter.ToInt64(ReadBytes(address, numBytes), 0);
                            break;
                        case TypeCode.UInt64:
                            ret = BitConverter.ToUInt64(ReadBytes(address, numBytes), 0);
                            break;
                        case TypeCode.Single:
                            ret = BitConverter.ToSingle(ReadBytes(address, numBytes), 0);
                            break;
                        case TypeCode.Double:
                            ret = BitConverter.ToDouble(ReadBytes(address, numBytes), 0);
                            break;
                        default:
                            IntPtr dataStore = Marshal.AllocHGlobal(numBytes);
                            byte[] data = ReadBytes(address, numBytes);
                            Marshal.Copy(data, 0, dataStore, numBytes);
                            ret = Marshal.PtrToStructure(dataStore, typeof(T));
                            Marshal.FreeHGlobal(dataStore);
                            break;
                    }
                }
                return ret;
            }
            catch { return Activator.CreateInstance(t); }
        }
    }
}
