using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesy.Core.Services
{
    public interface IProcesyService
    {
        public List<ProcessClass> GetProcesses();
        public List<ProcessClass> GetProcessesFiltered(string filter);
        public List<ProcessClass> GetProcessesSortedByName(bool direction);
        public List<ProcessClass> GetProcessesSortedByNameAndFiltered(bool direction, string filter);
        public List<ProcessClass> GetProcessesSortedByMemoryAndFiltered(bool direction, string filter);
        public List<ProcessClass> GetProcessesSortedByMemory(bool direction);
    }
}
