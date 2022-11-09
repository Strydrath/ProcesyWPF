using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Procesy.Core.Services
{
    public class ProcesyService : IProcesyService
    {
        private List<Process> processes;

        public ProcesyService()
        {
            processes = Process.GetProcesses().ToList();
        }

        public List<ProcessClass> GetProcesses()
        {
            processes = Process.GetProcesses().ToList();
            return createList(processes);
        }

        private List<ProcessClass> createList(List<Process> processes)
        {
            var res = new List<ProcessClass>();
            foreach (var p in processes)
            {
                res.Add(new ProcessClass(p.ProcessName, p.Id,p.WorkingSet64/1024 ));
            }

            return res;
        }

        public List<ProcessClass> GetProcessesFiltered(string filter)
        {
            processes = Process.GetProcesses().ToList();
            var processesFiltered = processes.FindAll(p => p.ProcessName.StartsWith(filter, StringComparison.InvariantCultureIgnoreCase));
            return createList(processesFiltered);
        }

        public List<ProcessClass> GetProcessesSortedByName(bool direction)
        {
            processes = Process.GetProcesses().ToList();
            var res = createList(processes);

            if (direction)
            {
                res.Sort((p, q) => String.Compare(p.ProcessName, q.ProcessName, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {

                res.Sort((p, q) => String.Compare( q.ProcessName, p.ProcessName, StringComparison.InvariantCultureIgnoreCase));
            }

            return res;
        }

        public List<ProcessClass> GetProcessesSortedByNameAndFiltered(bool direction, string filter)
        {
            processes = Process.GetProcesses().ToList();
            var processesFiltered = processes.FindAll(p => p.ProcessName.StartsWith(filter, StringComparison.InvariantCultureIgnoreCase));
            var res = createList(processesFiltered);
            if (direction)
            {
                res.Sort((p, q) => String.Compare(p.ProcessName, q.ProcessName, StringComparison.InvariantCultureIgnoreCase));
            }
            else
            {

                res.Sort((p, q) => String.Compare(q.ProcessName, p.ProcessName, StringComparison.InvariantCultureIgnoreCase));
            }

            return res;
        }

        public List<ProcessClass> GetProcessesSortedByMemory(bool direction)
        {
            processes = Process.GetProcesses().ToList();
            var res = createList(processes);

            if (direction)
            {
                res.Sort((p, q) => p.ProcessMemory.CompareTo(q.ProcessMemory));
            }
            else
            {

                res.Sort((p, q) => q.ProcessMemory.CompareTo(p.ProcessMemory));
            }
            return res;
        }

        public List<ProcessClass> GetProcessesSortedByMemoryAndFiltered(bool direction, string filter)
        {
            processes = Process.GetProcesses().ToList();
            var processesFiltered = processes.FindAll(p => p.ProcessName.StartsWith(filter, StringComparison.InvariantCultureIgnoreCase));
            var res = createList(processesFiltered);
            if (direction)
            {
                res.Sort((p, q) => p.ProcessMemory.CompareTo(q.ProcessMemory));
            }
            else
            {

                res.Sort((p, q) => q.ProcessMemory.CompareTo(p.ProcessMemory));
            }

            return res;
        }
    }
}
