using D3MemDataLayer;
using LogicServiceLib;
using SendInputLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeeThreeComptroller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var process = Process.GetProcessesByName("Diablo III").FirstOrDefault();
            if (process == null)
            {
                throw new NullReferenceException("process");
            }

            var InputService = new SendMessageService(process);
            var DataService = new ObjectManager(process, InputService);
            var LogicService = new BasicLogicService(DataService, InputService);

            Console.Out.WriteLine(DataService.Player.CharacterName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MonitorScreen(DataService, InputService, LogicService));
        }
    }
}
