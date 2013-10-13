using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMemoryMapper
{
    public class AddressAttribute : Attribute
    {
        public readonly int Address;
        public readonly int Offset;

        public AddressAttribute(int address, int offset = 0)
        {
            this.Address = address;
            this.Offset = offset;
        }
    }
}
