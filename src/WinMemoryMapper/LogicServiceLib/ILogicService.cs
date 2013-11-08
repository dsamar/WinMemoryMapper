using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicServiceLib
{
    public interface ILogicService
    {
        /// <summary>
        /// Gets or sets the game service.
        /// </summary>
        /// <value>
        /// The game service.
        /// </value>
        GameControlService GameService { get; set; }

        /// <summary>
        /// Runs the logic.
        /// </summary>
        void RunLogic();

        /// <summary>
        /// Stops the logic.
        /// </summary>
        void AbortLogic();
    }
}
