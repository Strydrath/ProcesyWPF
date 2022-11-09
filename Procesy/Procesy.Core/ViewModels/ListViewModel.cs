using System;
using System.Reactive;
using System.Windows;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Procesy.Core.Services;


namespace Procesy.Core.ViewModels
{

    public class ListViewModel : MvxViewModel
    {

        private CancellationToken _cancelReload;

        private CancellationTokenSource _cancelReloadSource;

        readonly IProcesyService _procesyService;

        private bool _sortedByName;
        private bool _sortedByMemory;

        private ProcessClass _chosenProcess;
        public ProcessClass ChosenProcess
        {
            get => _chosenProcess;
            set
            {
                if (value == null)
                {
                    ShowDetails = false;
                }
                else
                {
                    LastId = value.ProcessId;
                    ShowDetails = true;
                }
                _chosenProcess = value;
                RaisePropertyChanged(() => ChosenProcess);
                GoToDetail();
            }
        }

        private DetailViewModel _detailViewModel;
        public DetailViewModel DetailModel
        {
            get => _detailViewModel;
            set
            {
                _detailViewModel = value;
                RaisePropertyChanged(() => DetailModel);
            }
        }
        public int LastId { get; set; }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                RaisePropertyChanged(() => FilterText);
                Reload();
            }
        }
        
        private string _nameDirection;
        public string NameDirection
        {
            get => _nameDirection;
            set
            {
                _nameDirection = value;
                RaisePropertyChanged(() => NameDirection);
            }
        }

        private string _memoryDirection;
        public string MemoryDirection
        {
            get => _memoryDirection;
            set
            {
                _memoryDirection = value;
                RaisePropertyChanged(() => MemoryDirection);
            }
        }

        private List<ProcessClass> _processList;
        public List<ProcessClass> ProcessList
        {
            get => _processList;
            set
            {
                _processList = value;
                RaisePropertyChanged(() => ProcessList);
                ChosenProcess = ProcessList.Find(p => p.ProcessId == LastId)!;
            }
        }

        private int _reloadInterval;
        public int ReloadInterval
        {
            get => _reloadInterval;
            set
            {
                _reloadInterval = value;
                RaisePropertyChanged(() => ReloadInterval);
                RerunReload();
            }
        }
        
        private bool _showDetails;
        public bool ShowDetails
        {
            get => _showDetails;
            set
            {
                _showDetails = value;
                RaisePropertyChanged(() => ShowDetails);
            }
        }

        public ListViewModel(IProcesyService procesyService, DetailViewModel detailViewModel)
        {
            ShowDetails = false;
            _detailViewModel  = detailViewModel;
            _sortedByName = false;
            _sortedByMemory = false;
            DetailModel.ParentModel = this;
            ProcessList = new List<ProcessClass>();
            _procesyService = procesyService;
            NameDirection = "v";
            MemoryDirection = "v";
            ReloadInterval = 5;
            RerunReload();
            Reload();
        }
        

        private void RerunReload()
        {
            if (_cancelReloadSource != null)
            {
                _cancelReloadSource.Cancel();
            }
            var dueTime = TimeSpan.FromSeconds(5);
            var interval = TimeSpan.FromSeconds(ReloadInterval);
            _cancelReloadSource = new CancellationTokenSource();
            _cancelReload = _cancelReloadSource.Token;
            RunPeriodicAsync(Reload, dueTime, interval, _cancelReload);

        }
        

        public void Reload()
        {
            if (FilterText != "" && FilterText!=null)
            {
                if (_sortedByName)
                {
                    ProcessList = _procesyService.GetProcessesSortedByNameAndFiltered(NameDirection == "v" ? true : false, FilterText);

                }
                else if (_sortedByMemory)
                {
                    ProcessList = _procesyService.GetProcessesSortedByMemoryAndFiltered(MemoryDirection == "v" ? true : false, FilterText);

                }
                else
                {
                    ProcessList = _procesyService.GetProcessesFiltered(FilterText);
                }
            }
            else if (_sortedByName)
            {
                ProcessList = _procesyService.GetProcessesSortedByName(NameDirection=="v"?true:false);

            }
            else if (_sortedByMemory)
            {
                ProcessList = _procesyService.GetProcessesSortedByMemory(MemoryDirection == "v" ? true : false);

            }
            else {
                ProcessList = _procesyService.GetProcesses();
            }
        }
        
        public void GoToDetail()
        {
            if (ChosenProcess == null)
            {
                return;
            }
            DetailModel.ProcessId = ChosenProcess.ProcessId;
            DetailModel.Reload();
            RaisePropertyChanged(() => DetailModel);

        }


        #region commands
        public ICommand ReloadCommand => new MvxCommand(Reload, CanDo);
        public ICommand BlockReloadCommand => new MvxCommand(BlockReload, CanDo);
        public ICommand SortByNameCommand => new MvxCommand(SortByName, CanDo);
        public ICommand SortByMemoryCommand => new MvxCommand(SortByMemory, CanDo);

        private bool CanDo()
        {
            return true;
        }

        public void SortByName()
        {
            _sortedByName = true;
            _sortedByMemory = false;
            if (NameDirection == "v")
            {
                NameDirection = "^";
            }
            else
            {
                NameDirection = "v";
            }
            Reload();
        }

        public void SortByMemory()
        {
            _sortedByName = false;
            _sortedByMemory = true;
            if (MemoryDirection == "v")
            {
                MemoryDirection = "^";
            }
            else
            {
                MemoryDirection = "v";
            }
            Reload();
        }

        public void BlockReload()
        {
            ReloadInterval = 0;
            _cancelReloadSource.Cancel();
            _cancelReloadSource = null;
        }

        #endregion commands

        private static async Task RunPeriodicAsync(Action onTick,
            TimeSpan dueTime,
            TimeSpan interval,
            CancellationToken token)
        {
            if (dueTime > TimeSpan.Zero)
                await Task.Delay(dueTime, token);
            while (!token.IsCancellationRequested)
            {
                onTick?.Invoke();
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }

        public override async Task Initialize()
        {
            
        }
    }
}
