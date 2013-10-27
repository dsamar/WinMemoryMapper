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
        public MemoryMapper(Process process)
        {
            // Open process for reading
            this.Process = process;

        }

        private uint BaseAddress
        {
            get { return (uint)Process.MainModule.BaseAddress; }
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

        /// <summary>
        /// Reads the specified addresses.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        public T Read<T>(params uint[] addresses)
        {
            return (T)this.Read(typeof(T), addresses);
        }

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
                    while ((lastChar = Convert.ToChar(Read(typeof(byte), address + offset))) != '\0')
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
                            ret = Marshal.PtrToStructure(dataStore, t);
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
