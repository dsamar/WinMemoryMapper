using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMemoryMapper
{
    public interface IMemContainer
    {
        public IDictionary<string, object> Values
        {
            get;
        }
    }
}
