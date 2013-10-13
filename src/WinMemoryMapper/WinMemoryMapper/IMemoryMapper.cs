using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMemoryMapper
{
    public interface IMemoryMapper
    {
        /// <summary>
        /// Reads the bytes.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public byte[] ReadBytes(uint address, int count);

        /// <summary>
        /// Reads the specified addresses.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        public object Read(Type t, params uint[] addresses);

        /// <summary>
        /// Reads the specified addresses.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addresses">The addresses.</param>
        /// <returns></returns>
        public T Read<T>(params uint[] addresses);
    }
}
