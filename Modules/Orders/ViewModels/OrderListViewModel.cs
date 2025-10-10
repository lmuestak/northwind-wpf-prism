using Northwind.DataAccess;
using Northwind.Mvvm;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Modules.ViewModels
{
    public class OrderListViewModel : ViewModelBase
    {

        #region private fields

        private string _keyword = string.Empty;
        private string _message = "Category List";
        private IOrderViewModel _selectedCategory;
        private AsyncDelegateCommand _reloadCommand;
        private DelegateCommand<IOrderViewModel> _addCommand;
        private DelegateCommand<IOrderViewModel> _editCommand;
        private DelegateCommand<IOrderViewModel> _deleteCommand;
        private AsyncDelegateCommand _searchCommand;
        private AsyncDelegateCommand _saveAllCommand;
        private ObservableCollection<IOrderViewModel> _categories;

        #endregion

        public OrderListViewModel(IContainerExtension container) : base(container)
        {
            LoadCategoriesAsync();
        }

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public Services.Interfaces.IDialogService DialogService => Container.Resolve<Services.Interfaces.IDialogService>();
        public IOrderViewModel SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }
        public ICategoryUnitOfWork UnitOfWork => Container.Resolve<ICategoryUnitOfWork>();
        public ObservableCollection<IOrderViewModel> Categories
        {
            get => _categories;
            private set => SetProperty(ref _categories, value);
        }

        private async void LoadCategoriesAsync()
        {
            try
            {
                var result = await GetCategoriesAsync();
                Categories = new ObservableCollection<IOrderViewModel>(result);
                SelectedCategory = Categories.FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                Message = ex.StackTrace;
            }
        }
        private async Task<IOrderViewModel[]> GetCategoriesAsync()
        {
            ShellViewModel.IsBusy = true;
            IsLoading = true;
            await Task.Delay(5000);
            try
            {
                var categories = await UnitOfWork.GetAsync();
                var result = new List<IOrderViewModel>();
                foreach (var category in categories)
                {
                    var vm = Container.Resolve<IOrderViewModel>();
                    vm.Model = category;
                    result.Add(vm);
                }
                ShellViewModel.IsBusy = false;
                IsLoading = false;
                return result.ToArray();
            }
            catch (Exception ex)
            {
                Message = ex.StackTrace;
                return Array.Empty<IOrderViewModel>();
            }
            finally
            {
                ShellViewModel.IsBusy = false;
                IsLoading = false;
            }

        }

        #region delete category command
        public DelegateCommand<IOrderViewModel> DeleteCommand => _deleteCommand ??= new DelegateCommand<IOrderViewModel>(OnDeleteCommand, OnCanDeleteCommand);

        private bool OnCanDeleteCommand(IOrderViewModel model)
        {
            return model != null;
        }

        private void OnDeleteCommand(IOrderViewModel model)
        {

        }

        #endregion

        #region add category command
        public DelegateCommand<IOrderViewModel> AddCommand => _addCommand ??= new DelegateCommand<IOrderViewModel>(OnAddCommand, OnCanAddCommand);
        private void OnAddCommand(IOrderViewModel model)
        {
            var callback = new DialogCallback()
            .OnClose(result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    Console.WriteLine("Confirm cancelled");
                    return;
                }
                Console.WriteLine($"Confirm closed: {result.Result}");
            })
            .OnError(ex =>
            {
                Console.WriteLine($"Confirm error: {ex.GetType().Name}: {ex.Message}");
            });
            var parameters = new DialogParameters
            {
                { "Model", model }
            };
            DialogService.ShowDialog(nameof(AddCategoryDialogView), parameters, callback);
        }

        private bool OnCanAddCommand(IOrderViewModel model)
        {
            return true;
        }

        #endregion

        #region edit category command

        public DelegateCommand<IOrderViewModel> EditCommand => _editCommand ??= new DelegateCommand<IOrderViewModel>(OnEditCommand, OnCanEditCommand);

        private void OnEditCommand(IOrderViewModel model)
        {
            var callback = new DialogCallback()
            .OnClose(result =>
            {
                if (result.Result != ButtonResult.OK)
                {
                    Console.WriteLine("Confirm cancelled");
                    return;
                }
                Console.WriteLine($"Confirm closed: {result.Result}");
            })
            .OnError(ex =>
            {
                Console.WriteLine($"Confirm error: {ex.GetType().Name}: {ex.Message}");
            });
            var parameters = new DialogParameters
            {
                { "Model", model }
            };
            DialogService.ShowDialog(nameof(EditCategoryDialogView), parameters, callback);
        }

        private bool OnCanEditCommand(IOrderViewModel model)
        {
            return model != null;
        }

        #endregion

        #region reload command

        public AsyncDelegateCommand ReloadCommand => _reloadCommand ??= new AsyncDelegateCommand(OnExecuteReloadCommand);

        private async Task OnExecuteReloadCommand()
        {
            try
            {
                Categories.Clear();
                var result = await GetCategoriesAsync();
                Categories = new ObservableCollection<IOrderViewModel>(result);
                SelectedCategory = Categories.FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                Message = ex.StackTrace;
            }
        }

        #endregion

        #region search command

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value);
        }
        public AsyncDelegateCommand SearchCommand => _searchCommand ??= new AsyncDelegateCommand(OnExecuteSearchCommand);

        private async Task OnExecuteSearchCommand()
        {
            //try
            //{
            //    var result = await GetCategoriesAsync();
            //    Categories = new ObservableCollection<ICategoryViewModel>(result);
            //    SelectedCategory = Categories.FirstOrDefault();
            //}
            //catch (System.Exception ex)
            //{
            //    Message = ex.StackTrace;
            //}
        }

        #endregion

        #region save all command

        public AsyncDelegateCommand SaveAllCommand => _saveAllCommand ??= new AsyncDelegateCommand(OnExecuteSaveAllCommand);

        private async Task OnExecuteSaveAllCommand()
        {
            //try
            //{
            //    var result = await GetCategoriesAsync();
            //    Categories = new ObservableCollection<ICategoryViewModel>(result);
            //    SelectedCategory = Categories.FirstOrDefault();
            //}
            //catch (System.Exception ex)
            //{
            //    Message = ex.StackTrace;
            //}
        }

        #endregion
    }
}
