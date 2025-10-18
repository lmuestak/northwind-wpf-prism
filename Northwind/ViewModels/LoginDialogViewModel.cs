using Northwind.DataAccess;
using Northwind.Mvvm;
using Northwind.Views;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Ioc;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace Northwind.ViewModels
{
    public class LoginDialogViewModel : ViewModelBase, IDialogAware
    {

        private EmployeeViewModel _selectedEmployee;
        private ObservableCollection<EmployeeViewModel> _employees;

        public LoginDialogViewModel()
        {
            GetEmployeesAsync().ContinueWith(t =>
            {
                if (t.Exception == null)
                {
                    Employees = new ObservableCollection<EmployeeViewModel>(t.Result);
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public IEmployeeUnitOfWork UnitOfWork => Container.Resolve<IEmployeeUnitOfWork>();

        public ObservableCollection<EmployeeViewModel> Employees
        {
            get => _employees;
            private set => SetProperty(ref _employees, value);
        }

        private async Task<EmployeeViewModel[]> GetEmployeesAsync()
        {
            IsLoading = true;
            await Task.Delay(5000);
            try
            {
                var employees = await UnitOfWork.GetAsync();
                var result = new List<EmployeeViewModel>();
                foreach (var employee in employees)
                {
                    var vm = employee.ToViewModel();
                    result.Add(vm);
                }
                IsLoading = false;
                return result.ToArray();
            }
            catch (Exception ex)
            {
                Message = ex.StackTrace;
                return [];
            }
            finally
            {
                IsLoading = false;
            }
            
        }

        private int _attempts = 0;
        private int _maxAttempts = 5;
        private string _username;
        private string _password = "Passw0rd";
        private string _message;
        private AsyncDelegateCommand _loginCommand;

        public EmployeeViewModel SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (SetProperty(ref _selectedEmployee, value))
                {
                    if (_selectedEmployee != null)
                    {
                        Username = _selectedEmployee.Email;
                    }
                }
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                {
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int Attempts
        {
            get => _attempts;
            set
            {
                if (SetProperty(ref _attempts, value))
                {
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public int MaxAttempts
        {
            get => _maxAttempts;
            set => SetProperty(ref _maxAttempts, value);
        }

        public ShellWindowViewModel ShellWindowViewModel => Container.Resolve<ShellWindowViewModel>();

        private bool OnCanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public DialogCloseListener RequestClose { get; }
        public bool CanCloseDialog() => true;
        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters) { }

        //public DelegateCommand LoginCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public AsyncDelegateCommand LoginCommand => _loginCommand ??= new AsyncDelegateCommand(OnExecuteLogin, OnCanExecuteLogin);

        private async Task OnExecuteLogin()
        {
            var employees = Employees ?? new ObservableCollection<EmployeeViewModel>(await GetEmployeesAsync());
            var employee = Employees.FirstOrDefault(_ => _.Email.Equals(Username, System.StringComparison.OrdinalIgnoreCase));
            if (employee != null && employee != default && Password == "Passw0rd")
            {
                //ShellWindowViewModel.WindowState = WindowState.Maximized;
                ShellWindowViewModel.ResizeMode = ResizeMode.CanResizeWithGrip;
                ShellWindowViewModel.CurrentUser = employee;
                ShellWindowViewModel.IsAuthenticated = true;
                RegionManager.RequestNavigate(RegionNames.ContentRegion, nameof(DashboardView));
                RequestClose.Invoke(new DialogResult() { Result = ButtonResult.OK });
            }
            else
            {
                Attempts++;
                if (Attempts >= MaxAttempts)
                {
                    RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
                    Application.Current.Shutdown();
                }
                else
                {
                    Message = $"Invalid credentials. Try again. Attempt {Attempts} of {MaxAttempts} attempts.";
                }
            }
        }
    }
}
