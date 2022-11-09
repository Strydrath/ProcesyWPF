using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesy.Core.Services
{
    public interface IDetailService
    {
        public void SetId(int id);
        public Process GetProcess();

        public string GetProcessName();
        public int GetProcessId();
        public string GetProcessMemory();

        public string GetStartTime();
        public string GetProcessPriority();
        public List<ThreadClass> GetThreads();
        public int RaisePriority();
        public int ReducePriority();
        public bool Kill();
    }
}
