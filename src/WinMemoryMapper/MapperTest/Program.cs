using D3MemDataLayer;
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
            var process = Process.GetProcessesByName("").FirstOrDefault();
            if (process == null)
            {
                throw new NullReferenceException("process");
            }

            var data = new ObjectManagerMemContainer(process);

            Console.Out.WriteLine(data.Player.CharacterName);
        }
    }
}
