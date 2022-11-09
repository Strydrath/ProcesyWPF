using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Procesy.Core.ViewModels;

namespace Procesy.Core.Services
{
    public class ProcessClass
    {
        public ProcessClass(string name, int id, long memory)
        {
            ProcessName = name;
            ProcessId = id;
            ProcessMemory = memory;
            Checked = false;
        }
        public String ProcessName { get; set; }
        public long ProcessMemory { get; set; }
        public int ProcessId { get; set; }
        public bool Checked { get; set; }

        public void check()
        {
            Checked = true;
        }

        public void unCheck()
        {
            Checked = false;
        }
    }
}
