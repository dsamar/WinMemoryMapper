using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogicServiceLib
{
    [Serializable]
    public class LearningData
    {
        List<PositionInstrumentation> InstrumentationList { get; set; }
    }
}
