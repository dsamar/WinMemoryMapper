using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMemoryMapper
{
    public class GenericContainer : IMemContainer
    {
        public GenericContainer()
        {
            this.Values = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Values { get; private set; }
    }
}
