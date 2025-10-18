using MaterialDesignThemes.Wpf;
using Northwind.Mvvm;
using Northwind.Services.Interfaces;
using Northwind.Windows;
using Prism.Commands;
using Prism.Ioc;
using Prism.Navigation.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Northwind.ViewModels
{
    public class ShellWindowViewModel: ViewModelBase, IShellWindowViewModel
    {
        
        private bool _isBusy;
        private string _currentViewName = "Home";

        #region constructor

        //public ShellWindowViewModel(IContainerExtension container) : base(container)
        //{
         
        //}

        #endregion

        #region theming

        private DelegateCommand _setThemeCommand;
        private PaletteHelper _paletteHelper;

        public bool IsDarkTheme
        {
            get
            {
                var theme = PaletteHelper.GetTheme();
                return theme.GetBaseTheme() == BaseTheme.Dark;
            }
        }

        public string DarkThemeTooltip
        {
            get => $"Click to toggle to {(IsDarkTheme ? "LightMode" : "DarkMode")}";
        }

        public PaletteHelper PaletteHelper
        {
            get
            {
                if (_paletteHelper == null)
                {
                    _paletteHelper = new PaletteHelper();
                }
                return _paletteHelper;
            }
        }
        public DelegateCommand ToggleThemeCommand
        {
            get
            {
                return _setThemeCommand ??= new DelegateCommand(
                    () =>
                    {
                        SetTheme();
                    },
                    () => true);
            }
        }

        private void SetTheme()
        {
            var theme = PaletteHelper.GetTheme();
            if (theme.GetBaseTheme() == BaseTheme.Light)
            {
                theme.SetBaseTheme(BaseTheme.Dark);
            }
            else
            {
                theme.SetBaseTheme(BaseTheme.Light);
            }
            PaletteHelper.SetTheme(theme);
            RaisePropertyChanged(nameof(IsDarkTheme));
            RaisePropertyChanged(nameof(DarkThemeTooltip));
        }

        #endregion

        #region window management

        private string _title = "Northwind Traders";

        private double _width = 1920d;
        private double _height = 1080d;
        private WindowState _windowState = WindowState.Normal;
        private ResizeMode resizeMode = ResizeMode.CanResizeWithGrip;
        private WindowStartupLocation _windowStartupLocation = WindowStartupLocation.CenterScreen;
        private DelegateCommand _minimizeWindowStateCommand;
        private DelegateCommand _maximizeOrNormalizeWindowStateCommand;
        private DelegateCommand _applicationClodeCommand;
        private DelegateCommand _resizeWindowCommand;

        public string Title => _title?.ToUpperInvariant();

        public string Username
        {
            get
            {
                if (CurrentUser != null)
                {
                    return $"{CurrentUser.Title} {CurrentUser.FirstName} {CurrentUser.LastName}";
                }
                return _title;
            }
        }

        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        public double Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        public ResizeMode ResizeMode
        {
            get => resizeMode;
            set => SetProperty(ref resizeMode, value);
        }

        public WindowState WindowState
        {
            get => _windowState;
            set => SetProperty(ref _windowState, value);
        }

        public WindowStartupLocation StartupLocation
        {
            get => _windowStartupLocation;
            set => SetProperty(ref _windowStartupLocation, value);
        }

        public DelegateCommand MinimizeWindowStateCommand
        {
            get
            {
                return _minimizeWindowStateCommand ??= new DelegateCommand(
                    () =>
                    {
                        WindowState = WindowState.Minimized;
                    },
                    () => true);
            }
        }

        public DelegateCommand MaximizeOrNormalizeWindowStateCommand
        {
            get
            {
                return _maximizeOrNormalizeWindowStateCommand ??= new DelegateCommand(
                    () =>
                    {
                        WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
                        if (WindowState == WindowState.Normal)
                        {
                            Height = SystemParameters.PrimaryScreenHeight / 2;
                            Width = SystemParameters.PrimaryScreenWidth / 2;
                        }
                    },
                    () => true);
            }
        }

        public DelegateCommand ApplicationCloseCommand
        {
            get
            {
                return _applicationClodeCommand ??= new DelegateCommand(
                    () =>
                    {
                        Container.Resolve<IConfigurationService>().Save();
                        Application.Current.Shutdown();
                    },
                    () => true);
            }
        }

        public DelegateCommand ResizeWindowCommand
        {
            get
            {
                return _minimizeWindowStateCommand ??= new DelegateCommand(
                    () =>
                    {
                        if(WindowState == WindowState.Maximized)
                        {
                            WindowState = WindowState.Normal;
                        }
                        else if(WindowState == WindowState.Normal)
                        {
                            WindowState = WindowState.Maximized;
                        }
                    },
                    () => WindowState == WindowState.Maximized || WindowState == WindowState.Normal);
            }
        }

        #endregion

        #region authentication

        private bool _isAuthenticated = true;
        private EmployeeViewModel _currentUser;
        public EmployeeViewModel CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (SetProperty(ref _currentUser, value))
                {
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { SetProperty(ref _isAuthenticated, value); }
        }

        #endregion

        #region navigation

        private GridLength _menuWidth = new(220);
        public GridLength MenuWidth
        {
            get => _menuWidth;
            set
            {
                if (SetProperty(ref _menuWidth, value))
                {
                    RaisePropertyChanged(nameof(MenuWidthDouble));
                }
            }
        }
        public double MenuWidthDouble => MenuWidth.Value;

        private bool _isMenuOpen = true;
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set
            {
                if (SetProperty(ref _isMenuOpen, value))
                {
                    RaisePropertyChanged(nameof(MenuWidthDouble));
                }
            }
        }

        private AsyncDelegateCommand<INavigationItemViewModel> _navigateToCommand;
        public AsyncDelegateCommand<INavigationItemViewModel> NavigateToCommand => _navigateToCommand ??= new AsyncDelegateCommand<INavigationItemViewModel>(OnExecuteNavigateCommand, OnCanExecuteNavigateCommand);

        private bool OnCanExecuteNavigateCommand(INavigationItemViewModel arg)
        {
            return arg?.Target != null;
        }

        private Task OnExecuteNavigateCommand(INavigationItemViewModel obj)
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, obj.Target);//, HandleCompletedNavigation);
            var view = GetView(RegionManager.Regions[RegionNames.ContentRegion].Views, (Type)obj.Tag);
            if (view != null && view is INamedView namedView)
            {
                CurrentViewName = namedView.ViewName;
            }
            return Task.CompletedTask;
        }

        public object GetView(IViewsCollection views, Type viewType)
        {
            foreach(var view in views)
            {
                var type = view.GetType();
                if (type == viewType)
                    return view;
            }
            return null;
        }

        private DelegateCommand _toggleMenuCommand;
        public DelegateCommand ToggleMenuCommand
        {
            get
            {
                return _toggleMenuCommand ??= new DelegateCommand(
                    () =>
                    {
                        IsMenuOpen = !IsMenuOpen;
                        var window = Container.Resolve<ShellWindow>();
                        var sb = (Storyboard)window.FindResource(IsMenuOpen ? "OpenMenu" : "CloseMenu");
                        sb.Begin(window, true); // 
                    },
                    () => true);
            }
        }
        #endregion

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string CurrentViewName
        {
            get => _currentViewName;
            set => SetProperty(ref _currentViewName, value);
        }
    }
}
