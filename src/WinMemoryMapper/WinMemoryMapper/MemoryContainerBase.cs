using SendInputLib;
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
        public void Initialize(IMemoryMapper memMapper, ISendMessageService inputService)
        {
            this.Mapper = memMapper;
            this.Input = inputService;
        }

        /// <summary>
        /// Initializes the specified parent.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public void Initialize(MemoryContainerBase parent)
        {
            this.Mapper = parent.Mapper;
            this.Input = parent.Input;
        }

        /// <summary>
        /// Gets the memory mapper.
        /// </summary>
        /// <value>
        /// The memory mapper.
        /// </value>
        public IMemoryMapper Mapper { get; set; }

        /// <summary>
        /// Gets or sets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public ISendMessageService Input { get; set; }
    }
}
