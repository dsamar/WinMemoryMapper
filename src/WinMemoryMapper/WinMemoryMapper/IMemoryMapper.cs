using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMemoryMapper
{
    public interface IMemoryMapper : IDisposable
    {
        public IMemContainer MapMemory(string processName, string valueConfigFile);
    }
}
