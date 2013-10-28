using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinMemoryMapper;

namespace WinMemoryMapper
{
    /// <summary>
    /// Used as a base class for all mem containers to access to memory mapper.
    /// </summary>
    public class MemoryContainerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryContainerBase"/> class.
        /// </summary>
        public MemoryContainerBase()
        {
        }

        /// <summary>
        /// Initializes the specified memory mapper.
        /// </summary>
        /// <param name="memMapper">The memory mapper.</param>
        public void Initialize(IMemoryMapper memMapper)
        {
            this.Mapper = memMapper;
        }

        /// <summary>
        /// Gets the memory mapper.
        /// </summary>
        /// <value>
        /// The memory mapper.
        /// </value>
        public IMemoryMapper Mapper { get; set; }
    }
}
