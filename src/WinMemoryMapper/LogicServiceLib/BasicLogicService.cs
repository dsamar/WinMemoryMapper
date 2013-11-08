using D3MemDataLayer;
using SendInputLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinMemoryMapper;

namespace LogicServiceLib
{
    public class BasicLogicService : ILogicService
    {
        /// <summary>
        /// Gets or sets the game service.
        /// </summary>
        /// <value>
        /// The game service.
        /// </value>
        public GameControlService GameService { get; set; }

        /// <summary>
        /// Gets or sets the worker.
        /// </summary>
        /// <value>
        /// The worker.
        /// </value>
        public GameLogicWorker Worker { get; set; }

        /// <summary>
        /// Gets or sets the worker thread.
        /// </summary>
        /// <value>
        /// The worker thread.
        /// </value>
        public Thread WorkerThread { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicLogicService"/> class.
        /// </summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="inputService">The input service.</param>
        public BasicLogicService(ObjectManager dataService, ISendMessageService inputService)
        {
            this.GameService = new GameControlService(dataService, inputService);
        }

        /// <summary>
        /// Runs the logic.
        /// </summary>
        public void RunLogic()
        {
            this.Worker = new GameLogicWorker(this.GameService);
            this.WorkerThread = new Thread(this.Worker.StartLoop);
            this.WorkerThread.Start();
        }

        /// <summary>
        /// Stops the logic.
        /// </summary>
        public void AbortLogic()
        {
            this.Worker.RequestStop();
            this.WorkerThread.Join();
        }

        
    }
}
