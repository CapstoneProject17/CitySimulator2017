using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ServerForTheLogic.Utilities
{
    public class FileProcessorService : ServiceControl
    {
        private static FileProcessorComponent _component;

        public bool Start(HostControl hostControl)
        {
            try
            {
                //Execute my existing console application code
                _component = new FileProcessorComponent();
                _component.Start();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                _component.Stop();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
