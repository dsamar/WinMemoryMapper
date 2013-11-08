using D3MemDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicServiceLib;

namespace LogicServiceLib
{
    public class LearningDB
    {
        public LearningDB(string dbFile)
        {

        }

        public LearningData Data { get; set; }

        public void SaveTargetPos(LocationMemoryBased location)
        {
            // throw new NotImplementedException();
        }

        public void SavePlayerPos(LocationMemoryBased location)
        {
            // throw new NotImplementedException();
        }
    }
}
