using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WinMemoryMapper
{
    public class MemoryMapper : IMemoryMapper
    {
        private bool disposed;
        public Process Process { get; set; }
        
        public IMemContainer MapMemory(string processName, string valueConfigFile)
        {
            // Open process for reading
            this.Process = Process.GetProcessesByName(processName).FirstOrDefault();

            // Open XML file to read values
            XmlDocument doc = new XmlDocument();
            doc.Load(valueConfigFile);

            return this.GetValues(doc);
        }

        private IMemContainer GetValues(XmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);

            // Call SupressFinalize in case a subclass implements a finalizer.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these  
            // operations, as well as in your methods that use the resource. 
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.Process != null)
                    {
                        this.Process.Dispose();
                        Console.WriteLine("Object disposed.");
                    }
                }

                this.Process = null;
                // Indicate that the instance has been disposed.
                this.disposed = true;
            }
        }
    }
}
