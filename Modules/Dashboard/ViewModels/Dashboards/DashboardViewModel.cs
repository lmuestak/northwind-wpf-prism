using Northwind.Mvvm;

namespace Northwind.ViewModels.Dashboards
{
    public class DashboardViewModel : ViewModelBase
    {
        //public DashboardViewModel(IContainerExtension container) : base(container)
        //{
        //    Title = "Dashboard";
        //}
    }

    //public class DashboardViewModel : ViewModelBase
    //{

    //    private string _selectedCategoryYear;
    //    private string _selectedCategoryName;
    //    private string _title = "Dashboard";

    //    private ObservableCollection<string> _categoryYears;
    //    private ObservableCollection<string> _categoryNames;

    //    private ObservableCollection<SalesOverviewByCategory> _salesOverviews;
    //    private ObservableCollection<CartesianChartViewModel> _cartesianCharts;
    //    private PieChartViewModel _selectedPieChart;
    //    private PieChartViewModel _selectedDoughnutChart;
    //    private CartesianChartViewModel _selectedCartesianChart;
    //    private AsyncDelegateCommand _reloadCommand;

    //    public DashboardViewModel(IContainerExtension container) : base(container)
    //    {
    //        LoadChartDataAsync();
    //    }

    //    public IDashboardDataManager DashboardDataManager => Container.Resolve<IDashboardDataManager>();

    //    public string Title
    //    {
    //        get { return _title; }
    //        set { SetProperty(ref _title, value); }
    //    }

    //    public string CurrentDate => DateTime.Now.ToString("dd.MM.yyyy");
    //    public ObservableCollection<string> CategoryYears
    //    {
    //        get { return _categoryYears; }
    //        set 
    //        { 
    //            if(SetProperty(ref _categoryYears, value))
    //            {

    //            }
    //        }
    //    }

    //    public string SelectedCategoryYear
    //    {
    //        get { return _selectedCategoryYear; }
    //        set
    //        {
    //            if (SetProperty(ref _selectedCategoryYear, value))
    //            {
    //                if (SelectedCartesianChart != null)
    //                {
    //                    if (value.Equals("-All-", StringComparison.CurrentCultureIgnoreCase))
    //                    {
    //                        foreach (LineSeries series in SelectedCartesianChart.Series)
    //                        {
    //                            series.Visibility = System.Windows.Visibility.Visible;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        foreach (LineSeries series in SelectedCartesianChart.Series)
    //                        {
    //                            if (series.Title.Equals(value, StringComparison.CurrentCultureIgnoreCase))
    //                            {
    //                                series.Visibility = System.Windows.Visibility.Visible;
    //                            }
    //                            else
    //                            {
    //                                series.Visibility = System.Windows.Visibility.Collapsed;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    public ObservableCollection<string> CategoryNames
    //    {
    //        get { return _categoryNames; }
    //        set { SetProperty(ref _categoryNames, value); }
    //    }

    //    public string SelectedCategoryName
    //    {
    //        get { return _selectedCategoryName; }
    //        set
    //        {
    //            if (SetProperty(ref _selectedCategoryName, value))
    //            {
    //                if (SelectedCartesianChart != null)
    //                {
    //                    if (value.Equals("-All-", StringComparison.CurrentCultureIgnoreCase))
    //                    {
    //                        foreach (var series in SelectedCartesianChart.Series)
    //                        {
    //                            if (series is LineSeries lines)
    //                            {
    //                                lines.Visibility = System.Windows.Visibility.Visible;
    //                            }
    //                            else if (series is ColumnSeries column)
    //                            {
    //                                column.Visibility = System.Windows.Visibility.Visible;
    //                            }
    //                        }
    //                    }
    //                    else
    //                    {
    //                        foreach (var series in SelectedCartesianChart.Series)
    //                        {
    //                            if (series is LineSeries line)
    //                            {
    //                                if (series.Title.Equals(value, StringComparison.CurrentCultureIgnoreCase))
    //                                {
    //                                    line.Visibility = System.Windows.Visibility.Visible;
    //                                }
    //                                else
    //                                {
    //                                    line.Visibility = System.Windows.Visibility.Collapsed;
    //                                }
    //                            }
    //                            else if (series is ColumnSeries column)
    //                            {
    //                                if (series.Title.Equals(value, StringComparison.CurrentCultureIgnoreCase))
    //                                {
    //                                    column.Visibility = System.Windows.Visibility.Visible;
    //                                }
    //                                else
    //                                {
    //                                    column.Visibility = System.Windows.Visibility.Collapsed;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    public ObservableCollection<SalesOverviewByCategory> SalesOverviewByCategories
    //    {
    //        get { return _salesOverviews; }
    //        set { SetProperty(ref _salesOverviews, value); }
    //    }

    //    public CartesianChartViewModel SelectedCartesianChart 
    //    { 
    //        get => _selectedCartesianChart;
    //        set => SetProperty(ref _selectedCartesianChart, value);
    //    }

    //    public PieChartViewModel SelectedPieChart
    //    {
    //        get => _selectedPieChart;
    //        set => SetProperty(ref _selectedPieChart, value);
    //    }

    //    public PieChartViewModel SelectedDoughnutChart
    //    {
    //        get => _selectedDoughnutChart;
    //        set => SetProperty(ref _selectedDoughnutChart, value);
    //    }

    //    public ObservableCollection<CartesianChartViewModel> CartesianCharts
    //    {
    //        get { return _cartesianCharts; }
    //        set { SetProperty(ref _cartesianCharts, value); }
    //    }

    //    private void LoadChartDataAsync()
    //    {
    //        try
    //        {
    //            ShellViewModel.IsBusy = true;
    //            IsLoading = true;
    //            CreateCategoryCharts();

    //        }
    //        catch (System.Exception ex)
    //        {
    //            //throw ex;
    //        }
    //        finally
    //        {
    //            ShellViewModel.IsBusy = false;
    //            IsLoading = false;
    //        }
    //    }

    //    private async Task<ObservableCollection<SalesOverviewByCategory>> GetSalesOverviewByCategoriesAsync()
    //    {

    //        try
    //        {
    //            var sales = await DashboardDataManager.GetSalesOverviewByCategoriesAsync();
    //            ShellViewModel.IsBusy = false;
    //            IsLoading = false;
    //            return sales;
    //        }
    //        catch (Exception ex)
    //        {
    //            //Message = ex.StackTrace;
    //            return [];
    //        }
    //    }

    //    private async Task<ObservableCollection<SalesOverviewByEmployee>> GetSalesOverviewByEmployeesAsync()
    //    {

    //        try
    //        {
    //            var sales = await DashboardDataManager.GetSalesOverviewByEmployeesAsync();
    //            ShellViewModel.IsBusy = false;
    //            IsLoading = false;
    //            return sales;
    //        }
    //        catch (Exception ex)
    //        {
    //            //Message = ex.StackTrace;
    //            return [];
    //        }
    //    }

    //    private async void CreateCategoryCharts()
    //    {
    //        SalesOverviewByCategories = await GetSalesOverviewByCategoriesAsync();
    //        SelectedCartesianChart = new CartesianChartViewModel(SelectedCategoryName, SalesOverviewByCategories);
    //        SelectedPieChart = new PieChartViewModel(SelectedCategoryName, SalesOverviewByCategories);
    //        SelectedDoughnutChart = new PieChartViewModel(SelectedCategoryName, SalesOverviewByCategories);
    //        CategoryNames = new ObservableCollection<string>(["-All-", .. SalesOverviewByCategories.Select(_ => _.CategoryName).Distinct()]);
    //        SelectedCategoryName = CategoryNames.FirstOrDefault();
    //    }

    //    #region reload command

    //    public AsyncDelegateCommand ReloadCommand => _reloadCommand ??= new AsyncDelegateCommand(OnExecuteReloadCommand);

    //    private Task OnExecuteReloadCommand()
    //    {
    //        return Task.Run(() => LoadChartDataAsync());
    //    }

    //    #endregion

    //}
}
