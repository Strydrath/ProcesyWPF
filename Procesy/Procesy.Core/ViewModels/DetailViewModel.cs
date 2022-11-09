using System.Data;
using System.Diagnostics;
using System.Windows.Input;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using MvvmCross.Binding;
using Procesy.Core.Services;

namespace Procesy.Core.ViewModels
{

    public class DetailViewModel : MvxViewModel<int>
    {
        readonly IDetailService _detailService;

        private readonly IMvxNavigationService _navigationService;
        private ListViewModel _parentModel;

        public ListViewModel ParentModel
        {
            get => _parentModel;
            set
            {
                _parentModel = value;
                RaisePropertyChanged(() => ParentModel);
            }
        }

        public DetailViewModel(IDetailService detailService, IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            _detailService = detailService;
        }
        public override void Prepare(int param)
        {
            _detailService.SetId(param);
            base.Prepare();
            Reload();
        }


        private int _processId;

        public int ProcessId
        {
            get => _processId;
            set
            {
                _detailService.SetId(value);
                _processId = value;
                RaisePropertyChanged(() => ProcessId);
            }
        }

        private string _processName;

        public string ProcessName
        {
            get => _processName;
            set
            {
                _processName = value;
                RaisePropertyChanged(() => ProcessName);
            }
        }

        private string _processPriority;

        public string ProcessPriority
        {
            get => _processPriority;
            set
            {
                _processPriority = value;
                RaisePropertyChanged(() => ProcessPriority);
            }
        }

        private string _processMemory;

        public string ProcessMemory
        {
            get => _processMemory;
            set
            {
                _processMemory = value;
                RaisePropertyChanged(() => ProcessMemory);
            }
        }

        private string _startTime;

        public string StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                RaisePropertyChanged(() => StartTime);
            }
        }

        private List<ThreadClass> _threadsList;

        public List<ThreadClass> ThreadsList
        {
            get => _threadsList;
            set
            {
                _threadsList = value;
                RaisePropertyChanged(() => ThreadsList);
            }
        }


        private bool CanDo()
        {
            return true;
        }

        public void Reload()
        {
            if (ProcessId != -1)
            {
                ProcessName = _detailService.GetProcessName();
                ProcessId = _detailService.GetProcessId();
                ProcessPriority = _detailService.GetProcessPriority();
                ProcessMemory = _detailService.GetProcessMemory();
                StartTime = _detailService.GetStartTime();
                ThreadsList = _detailService.GetThreads();
            }
        }

        #region commands
        public ICommand KillCommand
        {
            get { return new MvxCommand(Kill, CanDo); }
        }

        public ICommand RaisePriorityCommand
        {
            get { return new MvxCommand(RaisePriority, CanDo); }
        }
        public ICommand ReducePriorityCommand
        {
            get { return new MvxCommand(ReducePriority, CanDo); }
        }

        public void Kill()
        {
            _detailService.Kill();
            ProcessId = -1;
            Reload();
            ParentModel.Reload();
            Thread.Sleep(200);
            Reload();
            ParentModel.Reload();
        }
        public void RaisePriority()
        {
            _detailService.RaisePriority();
            Reload();
        }

        public void ReducePriority()
        {
            _detailService.ReducePriority();
            Reload();
        }

        #endregion
        public override async Task Initialize()
        {

        }
    }
}

