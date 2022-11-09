using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Procesy.Core.Services
{
    public class ThreadClass
    {
        public ThreadClass(int id, string priority, string tState)
        {
            Id = id;
            Priority = priority;
            ThreadState = tState;
        }
        public int Id { get; set; }
        public string Priority { get; set; }
        public string ThreadState { get; set; }
    }
    public class DetailService : IDetailService
    {
        private Process process;

        public int Id { get; private set; }

        public void SetId(int id)
        {
            Id = id;
            updateProcess();
        }

        private void updateProcess()
        {
            try
            {
                process = Process.GetProcessById(Id);
            }
            catch (Exception ex)
            {
                process = null;
            }
        }

        public DetailService()
        {
            Id = 0;
            updateProcess();
        }

        public Process GetProcess()
        {
            updateProcess();
            return process;
        }

        public bool Kill()
        {
            updateProcess();
            if (process == null)
            {
                return false;
            }
            try
            {
                process.Kill();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetProcessName()
        {
            updateProcess();
            string res;
            if (process == null)
            {
                return "-";
            }
            try
            {
                res = process?.ProcessName;
            }
            catch(Exception ex)
            {
                res = "-";
            }

            return res;
        }

        public string GetProcessMemory()
        {
            updateProcess();
            string res;
            if (process == null)
            {
                return "-";
            }
            try
            {
                return process?.WorkingSet64 / 1024 + "KB";
            }
            catch (Exception ex)
            {
                res = "-";
            }

            return res;
        }
        public int GetProcessId()
        {
            updateProcess();
            int res;
            if (process == null)
            {
                return -1;
            }
            try
            {
                res = process.Id;
            }
            catch (Exception ex)
            {
                res = -1;
            }

            return res;
        }

        public string GetStartTime()
        {
            updateProcess();
            if (process == null)
            {
                return "-";
            }
            string res;
            try
            {
                res = process.StartTime.ToString("G", CultureInfo.GetCultureInfo("pl-PL"));
            }
            catch (Exception exception)
            {
                res = "-";
            }

            return res;
        }

        public List<ThreadClass> GetThreads()
        {
            updateProcess();
            List<ThreadClass> res = new List<ThreadClass>();
            try
            {
                foreach (ProcessThread t in process.Threads)
                {
                    var state = t.ThreadState.ToString();
                    var pTime = t.CurrentPriority.ToString();
                    res.Add(new ThreadClass(t.Id, pTime, state));
                }
            }
            catch (Exception ex)
            {
                res.Add(new ThreadClass(0,"-","-"));
            }

            return res;
        }

        public string GetProcessPriority()
        {
            updateProcess();
            if (process == null)
            {
                return "-";
            }
            try
            {
                var p = process.BasePriority;
                switch (p)
                {
                    case 4:
                        return "Idle";
                    case 8:
                        return "Normal";
                    case 13:
                        return "High";
                    case 24:
                        return "RealTime";
                }

                return p.ToString();
            }
            catch (Exception ex)
            {
                return "-";
            }
        }

        public int RaisePriority()
        {
            updateProcess();
            if (process == null)
            {
                return -1;
            }
            try
            {
                var p = process.BasePriority;
                switch (p)
                {
                    case 4:
                        process.PriorityClass = ProcessPriorityClass.Normal;
                        break;
                    case 8:
                        process.PriorityClass = ProcessPriorityClass.High;
                        break;
                    case 13:
                        process.PriorityClass = ProcessPriorityClass.RealTime;
                        break;
                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int ReducePriority()
        {
            updateProcess();
            if (process == null)
            {
                return -1;
            }
            try
            {
                var p = process.BasePriority;
                switch (p)
                {
                    case 8:
                        process.PriorityClass = ProcessPriorityClass.Idle;
                        break;
                    case 13:
                        process.PriorityClass = ProcessPriorityClass.Normal;
                        break;
                    case 24:
                        process.PriorityClass = ProcessPriorityClass.High;
                        break;
                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


    }
}
