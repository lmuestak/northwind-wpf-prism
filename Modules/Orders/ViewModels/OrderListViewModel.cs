using Northwind.Mvvm;
using Prism.Ioc;

namespace Northwind.ViewModels
{
    public class OrderListViewModel : ViewModelBase
    {
        //public OrderListViewModel(IContainerExtension container) : base(container)
        //{
        //}

    }
    //public class OrderListViewModel : ViewModelBase
    //{

    //    #region private fields

    //    private string _keyword = string.Empty;
    //    private string _message = "Category List";
    //    private OrderViewModel _selectedCategory;
    //    private AsyncDelegateCommand _reloadCommand;
    //    private DelegateCommand<OrderViewModel> _addCommand;
    //    private DelegateCommand<OrderViewModel> _editCommand;
    //    private DelegateCommand<OrderViewModel> _deleteCommand;
    //    private AsyncDelegateCommand _searchCommand;
    //    private AsyncDelegateCommand _saveAllCommand;
    //    private ObservableCollection<OrderViewModel> _categories;

    //    #endregion

    //    public OrderListViewModel(IContainerExtension container) : base(container)
    //    {
    //        LoadCategoriesAsync();
    //    }

    //    public string Message
    //    {
    //        get { return _message; }
    //        set { SetProperty(ref _message, value); }
    //    }
    //    public Services.Interfaces.IDialogService DialogService => Container.Resolve<Services.Interfaces.IDialogService>();
    //    public OrderViewModel SelectedCategory
    //    {
    //        get => _selectedCategory;
    //        set => SetProperty(ref _selectedCategory, value);
    //    }
    //    public ICategoryUnitOfWork UnitOfWork => Container.Resolve<ICategoryUnitOfWork>();
    //    public ObservableCollection<OrderViewModel> Categories
    //    {
    //        get => _categories;
    //        private set => SetProperty(ref _categories, value);
    //    }

    //    private async void LoadCategoriesAsync()
    //    {
    //        try
    //        {
    //            var result = await GetCategoriesAsync();
    //            Categories = new ObservableCollection<OrderViewModel>(result);
    //            SelectedCategory = Categories.FirstOrDefault();
    //        }
    //        catch (System.Exception ex)
    //        {
    //            Message = ex.StackTrace;
    //        }
    //    }
    //    private async Task<OrderViewModel[]> GetCategoriesAsync()
    //    {
    //        ShellViewModel.IsBusy = true;
    //        IsLoading = true;
    //        await Task.Delay(5000);
    //        try
    //        {
    //            var categories = await UnitOfWork.GetAsync();
    //            var result = new List<OrderViewModel>();
    //            foreach (var category in categories)
    //            {
    //                var vm = Container.Resolve<OrderViewModel>();
    //                vm.Model = category;
    //                result.Add(vm);
    //            }
    //            ShellViewModel.IsBusy = false;
    //            IsLoading = false;
    //            return result.ToArray();
    //        }
    //        catch (Exception ex)
    //        {
    //            Message = ex.StackTrace;
    //            return Array.Empty<OrderViewModel>();
    //        }
    //        finally
    //        {
    //            ShellViewModel.IsBusy = false;
    //            IsLoading = false;
    //        }

    //    }

    //    #region delete category command
    //    public DelegateCommand<OrderViewModel> DeleteCommand => _deleteCommand ??= new DelegateCommand<OrderViewModel>(OnDeleteCommand, OnCanDeleteCommand);

    //    private bool OnCanDeleteCommand(OrderViewModel model)
    //    {
    //        return model != null;
    //    }

    //    private void OnDeleteCommand(OrderViewModel model)
    //    {

    //    }

    //    #endregion

    //    #region add category command
    //    public DelegateCommand<OrderViewModel> AddCommand => _addCommand ??= new DelegateCommand<OrderViewModel>(OnAddCommand, OnCanAddCommand);
    //    private void OnAddCommand(OrderViewModel model)
    //    {
    //        var callback = new DialogCallback()
    //        .OnClose(result =>
    //        {
    //            if (result.Result != ButtonResult.OK)
    //            {
    //                Console.WriteLine("Confirm cancelled");
    //                return;
    //            }
    //            Console.WriteLine($"Confirm closed: {result.Result}");
    //        })
    //        .OnError(ex =>
    //        {
    //            Console.WriteLine($"Confirm error: {ex.GetType().Name}: {ex.Message}");
    //        });
    //        var parameters = new DialogParameters
    //        {
    //            { "Model", model }
    //        };
    //        DialogService.ShowDialog(nameof(AddCategoryDialogView), parameters, callback);
    //    }

    //    private bool OnCanAddCommand(OrderViewModel model)
    //    {
    //        return true;
    //    }

    //    #endregion

    //    #region edit category command

    //    public DelegateCommand<OrderViewModel> EditCommand => _editCommand ??= new DelegateCommand<OrderViewModel>(OnEditCommand, OnCanEditCommand);

    //    private void OnEditCommand(OrderViewModel model)
    //    {
    //        var callback = new DialogCallback()
    //        .OnClose(result =>
    //        {
    //            if (result.Result != ButtonResult.OK)
    //            {
    //                Console.WriteLine("Confirm cancelled");
    //                return;
    //            }
    //            Console.WriteLine($"Confirm closed: {result.Result}");
    //        })
    //        .OnError(ex =>
    //        {
    //            Console.WriteLine($"Confirm error: {ex.GetType().Name}: {ex.Message}");
    //        });
    //        var parameters = new DialogParameters
    //        {
    //            { "Model", model }
    //        };
    //        DialogService.ShowDialog(nameof(EditCategoryDialogView), parameters, callback);
    //    }

    //    private bool OnCanEditCommand(OrderViewModel model)
    //    {
    //        return model != null;
    //    }

    //    #endregion

    //    #region reload command

    //    public AsyncDelegateCommand ReloadCommand => _reloadCommand ??= new AsyncDelegateCommand(OnExecuteReloadCommand);

    //    private async Task OnExecuteReloadCommand()
    //    {
    //        try
    //        {
    //            Categories.Clear();
    //            var result = await GetCategoriesAsync();
    //            Categories = new ObservableCollection<OrderViewModel>(result);
    //            SelectedCategory = Categories.FirstOrDefault();
    //        }
    //        catch (System.Exception ex)
    //        {
    //            Message = ex.StackTrace;
    //        }
    //    }

    //    #endregion

    //    #region search command

    //    public string Keyword
    //    {
    //        get => _keyword;
    //        set => SetProperty(ref _keyword, value);
    //    }
    //    public AsyncDelegateCommand SearchCommand => _searchCommand ??= new AsyncDelegateCommand(OnExecuteSearchCommand);

    //    private async Task OnExecuteSearchCommand()
    //    {
    //        //try
    //        //{
    //        //    var result = await GetCategoriesAsync();
    //        //    Categories = new ObservableCollection<ICategoryViewModel>(result);
    //        //    SelectedCategory = Categories.FirstOrDefault();
    //        //}
    //        //catch (System.Exception ex)
    //        //{
    //        //    Message = ex.StackTrace;
    //        //}
    //    }

    //    #endregion

    //    #region save all command

    //    public AsyncDelegateCommand SaveAllCommand => _saveAllCommand ??= new AsyncDelegateCommand(OnExecuteSaveAllCommand);

    //    private async Task OnExecuteSaveAllCommand()
    //    {
    //        //try
    //        //{
    //        //    var result = await GetCategoriesAsync();
    //        //    Categories = new ObservableCollection<ICategoryViewModel>(result);
    //        //    SelectedCategory = Categories.FirstOrDefault();
    //        //}
    //        //catch (System.Exception ex)
    //        //{
    //        //    Message = ex.StackTrace;
    //        //}
    //    }

    //    #endregion
    //}
}
