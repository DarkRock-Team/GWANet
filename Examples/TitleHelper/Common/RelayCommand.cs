using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GWANet.Examples.TitleHelper.Common
{
	public class RelayCommand : ICommand
	{
		private readonly Action _execute = null;
		private readonly Func<bool> _canExecute = null;

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public RelayCommand(Action methodToExecute)
		{
			_execute = methodToExecute ?? throw new ArgumentNullException(nameof(methodToExecute), $"{nameof(methodToExecute)}");
		}

		public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
			: this(methodToExecute)
		{
			_canExecute = canExecuteEvaluator ?? throw new ArgumentNullException(nameof(canExecuteEvaluator), $"{nameof(canExecuteEvaluator)}");
		}

		public bool CanExecute(object parameter)
			=> _canExecute == null ? true : _canExecute.Invoke();

		[DebuggerStepThrough]
		public void Execute(object parameter)
			=> _execute();
	}
}
