using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D3MemDataLayer
{
    public class AttributeValue
    {
        public string Name { get; set; }
        public uint Index { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Name, Value.ToString());
        }
    }
}
