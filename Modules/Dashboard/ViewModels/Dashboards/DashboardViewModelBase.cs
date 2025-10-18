using Northwind.Data;
using Northwind.DataAccess;
using Northwind.Mvvm;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Northwind.ViewModels.Dashboards
{
    public abstract class DashboardViewModelBase: ViewModelBase
    {

        #region private fields and properties
        
        private List<SalesReportData> _data;
        private const string DefaultAll = " - All - ";
        private AsyncDelegateCommand _loadCommand;
        private bool isInitialized;

        #endregion

        public static string CurrentDate => DateTime.Now.ToString("d");

        public List<SalesReportData> Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public IDashboardDataManager DashboardDataManager => Container.Resolve<IDashboardDataManager>();

        #region selectors

        #region regions

        private RegionViewModel _region;
        private ObservableCollection<RegionViewModel> _regions;

        public RegionViewModel Region
        {
            get => _region;
            set
            {
                if(SetProperty(ref _region, value))
                {
                    if(_region != null)
                    {
                        Territories = _region.Territories;
                        Territories.Insert(0, new TerritoryViewModel() { TerritoryId = null, Description = DefaultAll, RegionId = _region.RegionId});
                        Territory = Territories.FirstOrDefault();
                    }
                }
            }
        }

        public ObservableCollection<RegionViewModel> Regions
        {
            get => _regions;
            set => SetProperty(ref _regions, value);
        }

        #endregion

        #region territories

        private TerritoryViewModel _territory;
        private ObservableCollection<TerritoryViewModel> _territories;
        public TerritoryViewModel Territory
        {
            get => _territory;
            set => SetProperty(ref _territory, value);
        }
        public ObservableCollection<TerritoryViewModel> Territories
        {
            get => _territories;
            set => SetProperty(ref _territories, value);
        }

        #endregion

        #region date range

        private DateTime _minimumDate = DateTime.MinValue;
        private DateTime _maximumDate = DateTime.MinValue;
        private DateTime _startDate = DateTime.MinValue;
        private DateTime _endDate = DateTime.MinValue;
        
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }

        public DateTime MaximumDate
        {
            get => _maximumDate;
            set => SetProperty(ref _maximumDate, value);
        }

        #endregion

        #region categories

        private CategoryViewModel _category;
        private ObservableCollection<CategoryViewModel> _categories;

        public CategoryViewModel Category
        {
            get => _category;
            set
            {
                if(SetProperty(ref _category, value))
                {
                    if (_category != null)
                    {
                        Products = _category.Products;
                        Products.Insert(0, new ProductViewModel() { ProductId = null, ProductName = DefaultAll, CategoryId = _category.CategoryId, Category = _category });
                        foreach (var product in Products)
                        {
                            product.Category = _category;
                        }
                        Product = Products.FirstOrDefault();
                    }
                }
            }
        }

        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        #endregion

        #region employees

        private EmployeeViewModel _employee;
        private ObservableCollection<EmployeeViewModel> _employees;

        public EmployeeViewModel Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }
        public ObservableCollection<EmployeeViewModel> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value);
        }

        #endregion

        #region customers
        
        private CustomerViewModel _customer;
        private ObservableCollection<CustomerViewModel> _customers;
        
        public CustomerViewModel Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }
        public ObservableCollection<CustomerViewModel> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        #endregion

        #region shippers
        
        private ShipperViewModel _shipper;
        private ObservableCollection<ShipperViewModel> _shippers;
        public ShipperViewModel Shipper
        {
            get => _shipper;
            set => SetProperty(ref _shipper, value);
        }
        public ObservableCollection<ShipperViewModel> Shippers
        {
            get => _shippers;
            set => SetProperty(ref _shippers, value);
        }

        #endregion

        #region products
        
        private ProductViewModel _product;
        private ObservableCollection<ProductViewModel> _products;

        public ProductViewModel Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }
        public ObservableCollection<ProductViewModel> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }

        #endregion

        #region suppliers

        private SupplierViewModel _supplier;
        private ObservableCollection<SupplierViewModel> _suppliers;

        public SupplierViewModel Supplier
        {
            get => _supplier;
            set => SetProperty(ref _supplier, value);
        }
        public ObservableCollection<SupplierViewModel> Suppliers
        {
            get => _suppliers;
            set => SetProperty(ref _suppliers, value);
        }

        #endregion

        #endregion

        public bool IsInitialized
        {
            get => isInitialized;
            set => SetProperty(ref isInitialized, value);
        }

        #region selector data retrieval

        /// <summary>
        /// Asynchronously retrieves a collection of regions and populates the <see cref="Regions"/> property.
        /// </summary>
        /// <remarks>This method fetches regions from the data source, converts them to view models, and
        /// updates the <see cref="Regions"/> property with the results. A default "All" region is added to the
        /// collection as the first item. The <see cref="Region"/> property is set to the first region in the
        /// collection.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns></returns>
        public virtual async Task GetRegionsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await GetAsync(new QuerySpecification<Region>(Includes: [u => u.Territories]), cancellationToken);
                Regions = new ObservableCollection<RegionViewModel>(result.Select(_ => _.ToViewModel()));
                Regions.Insert(0, new RegionViewModel() { RegionId = null, Description = DefaultAll });
                Region = Regions.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calculates and sets the minimum, maximum, start, and end dates based on the available order data and the
        /// specified minimum number of days.
        /// </summary>
        /// <remarks>This method retrieves order data and determines a date range that satisfies the
        /// specified minimum number of days. If the total span of available data is less than the specified minimum,
        /// the range is adjusted to include as much data as possible while ensuring the end date is as close to today
        /// as feasible. If no order data is available, all date properties are set to <see langword="null"/>.</remarks>
        /// <param name="minimumDays">The minimum number of days for the date range. Must be a positive integer. Defaults to 7.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="minimumDays"/> is less than or equal to 0.</exception>
        public virtual async Task GetDatesAsync(int minimumDays = 7, CancellationToken cancellationToken = default)
        {
            if (minimumDays <= 0)
                throw new ArgumentOutOfRangeException(nameof(minimumDays), "minimumDays must be positive.");

            // Get your orders (assumes an overload that accepts CancellationToken; remove if not needed)
            var orders = await GetAsync(new QuerySpecification<Order>(), cancellationToken);

            // Handle no data
            if (orders == null)
            {
                MinimumDate = MaximumDate = StartDate = EndDate = DateTime.MinValue;
                return;
            }

            // If OrderDate is nullable, filter nulls
            var dated = orders
                .Select(o => o.OrderDate)                    // if nullable: keep as DateTime?
                .ToList();

            if (dated.Count == 0)
            {
                MinimumDate = MaximumDate = StartDate = EndDate = DateTime.MinValue;
                return;
            }

            var minDate = dated.Min();
            var maxDate = dated.Max();
            var today = DateTime.Today;

            MinimumDate = minDate;
            MaximumDate = maxDate;

            // Overall span of available data
            var overallSpanDays = (maxDate - minDate).TotalDays;

            DateTime chosenEnd;
            DateTime chosenStart;

            if (overallSpanDays < minimumDays)
            {
                // We cannot guarantee a >= minimumDays window within the data.
                // Prefer ending at 'today' if it doesn't precede the first data point; otherwise end at maxDate.
                chosenEnd = today >= minDate ? today : maxDate;

                // Start as far back as possible (bounded by minDate), aiming for 'minimumDays' lookback if feasible.
                chosenStart = chosenEnd.AddDays(-minimumDays);
                if (chosenStart < minDate) chosenStart = minDate;
                if (chosenStart > chosenEnd) chosenStart = chosenEnd; // safety
            }
            else
            {
                // We CAN achieve a >= minimumDays window fully within [minDate, maxDate].
                // Rule: "If possible the today must always be end date."
                // Interpreted as: use 'today' as EndDate WHEN it still allows a >= minimumDays window inside the data.
                var earliestEndThatAllowsMinSpan = minDate.AddDays(minimumDays);

                if (today >= earliestEndThatAllowsMinSpan && today <= maxDate)
                {
                    // Today works as an end date AND keeps the window fully inside the data
                    chosenEnd = today;
                }
                else if (today < earliestEndThatAllowsMinSpan)
                {
                    // Today would make the window too short inside the data;
                    // choose the earliest end that satisfies the minimum span
                    chosenEnd = earliestEndThatAllowsMinSpan;
                }
                else // today > maxDate
                {
                    // Can't end after the data if we want a >= minimumDays window inside the data
                    chosenEnd = maxDate;
                }

                chosenStart = chosenEnd.AddDays(-minimumDays);
                // Keep the window inside [minDate, maxDate]
                if (chosenStart < minDate) chosenStart = minDate;
            }

            StartDate = chosenStart;
            EndDate = chosenEnd;
        }

        /// <summary>
        /// Asynchronously retrieves a collection of categories, including their associated products, and populates the
        /// <see cref="Categories"/> property with the results.
        /// </summary>
        /// <remarks>The method fetches categories from the data source, converts them to view models, and
        /// adds a default "All" category at the beginning of the collection. The <see cref="Category"/> property is set
        /// to the first category in the collection.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns></returns>
        public virtual async Task GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await GetAsync(new QuerySpecification<Category>(Includes: [u => u.Products]), cancellationToken);
                Categories = new ObservableCollection<CategoryViewModel>(result.Select(_ => _.ToViewModel()));
                Categories.Insert(0, new CategoryViewModel() { CategoryId = null, CategoryName = DefaultAll, Products = [] });
                Category = Categories.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a collection of employees and updates the local employee data.
        /// </summary>
        /// <remarks>This method fetches employee data using a query specification and populates the
        /// <c>Employees</c> collection with the retrieved data. The first employee in the collection is set as the
        /// current <c>Employee</c>.</remarks>
        /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
        /// <returns></returns>
        public virtual async Task GetEmployeesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await GetAsync(new QuerySpecification<Employee>(), cancellationToken);
                Employees = new ObservableCollection<EmployeeViewModel>(result.Select(_=>_.ToViewModel()));
                Employee = Employees.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a collection of customers and updates the <see cref="Customers"/> property.
        /// </summary>
        /// <remarks>This method fetches customer data using a query specification and populates the <see
        /// cref="Customers"/> collection with the results. A default "All" customer entry is added to the collection,
        /// and the <see cref="Customer"/> property is set to the first item in the collection.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns></returns>
        public virtual async Task GetCustomersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await GetAsync(new QuerySpecification<Customer>(), cancellationToken);
                Customers = new ObservableCollection<CustomerViewModel>(result.Select(_ => _.ToViewModel()));
                Customers.Insert(0, new CustomerViewModel() { CustomerId = null, CompanyName = DefaultAll });
                Customer = Customers.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a collection of shippers and initializes the <see cref="Shippers"/> property.
        /// </summary>
        /// <remarks>This method fetches shippers using a query specification and converts the results
        /// into a collection of <see cref="ShipperViewModel"/> objects. A default "All" option is added to the
        /// collection, and the <see cref="Shipper"/> property is set to the first item in the collection.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
        /// <returns></returns>
        public virtual async Task GetShippersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await GetAsync(new QuerySpecification<Shipper>(), cancellationToken);
                Shippers = new ObservableCollection<ShipperViewModel>(result.Select(_=>_.ToViewModel()));
                Shippers.Insert(0, new ShipperViewModel() { ShipperId = null, CompanyName = DefaultAll });
                Shipper = Shippers.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a list of suppliers and updates the <see cref="Suppliers"/> collection.
        /// </summary>
        /// <remarks>This method fetches supplier data using a query specification and populates the <see
        /// cref="Suppliers"/> collection with the results. A default "All" supplier is added to the collection as the
        /// first item. The <see cref="Supplier"/> property is set to the first item in the updated
        /// collection.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation. The default value is <see
        /// langword="default"/>.</param>
        /// <returns></returns>
        public virtual async Task GetSuppliersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await GetAsync(new QuerySpecification<Supplier>(), cancellationToken);
                Suppliers = new ObservableCollection<SupplierViewModel>(result.Select(_=>_.ToViewModel()));
                Suppliers.Insert(0, new SupplierViewModel() { SupplierId = null, CompanyName = DefaultAll });
                Supplier = Suppliers.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create the filter expression based on selected filter criteria
        /// </summary>
        /// <returns></returns>
        public virtual Expression<Func<SalesReportData, bool>> CreateFilter()
        {
            Expression<Func<SalesReportData, bool>> filter = s => true; // Start with 'always true'

            if (Region != null && Region.RegionId.HasValue)
                filter = filter.And(s => s.RegionId == Region.RegionId.Value);

            if (Territory != null && !string.IsNullOrEmpty(Territory.TerritoryId))
                filter = filter.And(s => s.EmployeeTerritoryId == Territory.TerritoryId);

            if (Customer != null && !string.IsNullOrEmpty(Customer.CustomerId))
                filter = filter.And(s => s.CustomerId == Customer.CustomerId);

            if (Category != null && Category.CategoryId.HasValue)
                filter = filter.And(s => s.CategoryId == Category.CategoryId.Value);

            if (StartDate.IsValid())
                filter = filter.And(s => s.OrderAt >= StartDate);

            if (EndDate.IsValid())
                filter = filter.And(s => s.OrderAt <= EndDate);

            return filter;
        }
        
        protected async Task<T[]> GetAsync<T>(QuerySpecification<T> specification, CancellationToken cancellation = default) where T : class
        {
            try
            {
                var resultData = await DashboardDataManager.GetAsync(specification, cancellation);
                return resultData?.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Gets the command that refreshes the dashboard asynchronously.
        /// </summary>
        /// <remarks>This command executes the <c>OnExecuteLoadCommand</c> method to perform
        /// the refresh operation. It can always be executed as the associated condition always returns <see
        /// langword="true"/>.</remarks>
        public AsyncDelegateCommand LoadCommand => _loadCommand ??= new AsyncDelegateCommand(OnExecuteLoadCommand, () => true);

        /// <summary>
        /// Executes the command to refresh the dashboard asynchronously.
        /// </summary>
        /// <remarks>This method performs the necessary steps to refresh the dashboard, including
        /// initialization if required, refreshing dashboard data, and preparing the data for display. It ensures that
        /// the application state is updated to reflect the loading process and resets the state upon
        /// completion.</remarks>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        protected virtual async Task OnExecuteLoadCommand(CancellationToken cancellationToken)
        {
            try
            {
                ShellViewModel.IsBusy = true;
                IsLoading = true;
                Mouse.SetCursor(Cursors.Wait);
                if (!IsInitialized)
                {
                    await OnLoadFilterDataAsync(cancellationToken);
                    await OnLoadDashboardDataAsync(cancellationToken);
                    IsInitialized = true;
                }
                await OnPrepareDashboardDataAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ShellViewModel.IsBusy = false;
                IsLoading = false;
                Mouse.SetCursor(null);
            }
        }

        /// <summary>
        /// Initializing the dashboard filter data
        /// </summary>
        /// <returns></returns>
        public virtual async Task OnLoadFilterDataAsync(CancellationToken cancellationToken = default)
        {
            await GetRegionsAsync(cancellationToken);
            await GetCategoriesAsync(cancellationToken);
            await GetEmployeesAsync(cancellationToken);
            await GetCustomersAsync(cancellationToken);
            await GetShippersAsync(cancellationToken); 
            await GetSuppliersAsync(cancellationToken); 
            await GetDatesAsync(7, cancellationToken);
        }

        /// <summary>
        /// Refresh the dashboard data
        /// </summary>
        /// <returns></returns>
        public virtual async Task OnLoadDashboardDataAsync(CancellationToken cancellationToken = default)
        {
            var query = new QuerySpecification<SalesReportData>(Filter: CreateFilter());
            Data = [.. (await GetAsync(query, cancellationToken))];
        }

        /// <summary>
        /// Prepeares the dashboard for various data visualizations
        /// </summary>
        /// <returns></returns>
        public abstract Task OnPrepareDashboardDataAsync(CancellationToken cancellationToken = default);

    }

    
}
