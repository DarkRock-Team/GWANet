using System.ComponentModel;

namespace TitleHelper.Common
{
    public abstract class ViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract void InitializeCommands();
    }
}