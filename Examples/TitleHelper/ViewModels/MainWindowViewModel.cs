using GWANet.Examples.TitleHelper.Common;
using System;
using System.Windows.Input;
using TitleHelper.Common;
using TitleHelper.Services;

namespace TitleHelper.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Commands
        public ICommand ESCCommand { get; private set; }
        public ICommand InitializeGWANetCommand { get; private set; }
        public ICommand GetAllTitlesCommand { get; private set; }
        public ICommand MaxOutSweetTitleCommand { get; private set; }
        #endregion

        private string _statusTextBoxText = "Waiting for user interaction...";
        public string StatusTextBoxText
        {
            get => _statusTextBoxText;
            set
            {
                if(value != _statusTextBoxText)
                {
                    _statusTextBoxText = value;
                    OnPropertyChanged(nameof(StatusTextBoxText));
                }
            }
        }

        private bool _isInitialized = false;

        private readonly IGWANetService _gwaNetService;

        public MainWindowViewModel(IGWANetService gwaNetService)
        {
            _gwaNetService = gwaNetService;

            InitializeCommands();
            _isInitialized = true;
        }

        public override void InitializeCommands()
        {
            if (_isInitialized)
            {
                throw new InvalidOperationException($"{nameof(MainWindowViewModel)}'s commands are already initialized");
            }
            ESCCommand = new RelayCommand(ExitProgram);
            InitializeGWANetCommand = new RelayCommand(InitializeGWANet);
            GetAllTitlesCommand = new RelayCommand(GetAllTitles);
            MaxOutSweetTitleCommand = new RelayCommand(MaxOutSweetTitle);
        }

        private void InitializeGWANet()
        {
            _gwaNetService.InitializeWithoutCharName();
        }
        private void GetAllTitles()
        {
            var titles = _gwaNetService.GetAllCharacterTitles();
        }
        private void MaxOutSweetTitle()
        {
            _gwaNetService.MaxOutSweettoothTitle();
        }

        private void ExitProgram()
            => Environment.Exit(0);
    }
}
