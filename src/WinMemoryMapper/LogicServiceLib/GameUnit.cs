using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicServiceLib
{
    public class GameUnit
    {
        public GameUnit()
        {

        }

        /// <summary>
        /// Gets or sets the name of the unit.
        /// </summary>
        /// <value>
        /// The name of the unit.
        /// </value>
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the unit identifier.
        /// </summary>
        /// <value>
        /// The unit identifier.
        /// </value>
        public uint UnitId { get; set; }

        /// <summary>
        /// Gets or sets the unit hp.
        /// </summary>
        /// <value>
        /// The unit hp.
        /// </value>
        public uint UnitHP { get; set; }
    }
}
