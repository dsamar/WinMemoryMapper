using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinMemoryMapper;

namespace MapperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var process = Process.GetProcessesByName("diablo.exe").FirstOrDefault();
            var data = new MemoryMapper();
            var configFile = "some config file";

            data.MapMemory(process, configFile);
        }
    }
}
