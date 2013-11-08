using D3MemDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicServiceLib
{
    [Serializable]
    public class PositionInstrumentation
    {
        public ILocation PlayerPos { get; set; }

        public ILocation TargetPos { get; set; }

        public ILocation ActualPos { get; set; }
        
        public ILocation Delta { get; set; }
    }
}
