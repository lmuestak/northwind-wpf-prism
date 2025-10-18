using Northwind.Data;
using Northwind.Mvvm;
using System.Collections.ObjectModel;

namespace Northwind.ViewModels
{
    public static class CategoryViewModelExtensions
    {
        public static CategoryViewModel ToViewModel(this Category category)
        {

            ArgumentNullException.ThrowIfNull(category);

            return new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description ?? string.Empty,
                Picture = category.Picture,
                Icon17 = category.Icon17,
                Icon25 = category.Icon25,
                Products = category.Products != null ? new ObservableCollection<ProductViewModel>(category.Products.Select(_=>_.ToViewModel())) : new ObservableCollection<ProductViewModel>([]),
                State = EntityState.Unchanged
            };

        }

        public static Category ToEntity(this CategoryViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Category
            {
                CategoryId = viewModel.CategoryId.GetValueOrDefault(),
                CategoryName = viewModel.CategoryName,
                Description = viewModel.Description,
                Picture = viewModel.Picture,
                Icon17 = viewModel.Icon17,
                Icon25 = viewModel.Icon25,
                Products = viewModel.Products?.Select(_ => _.ToEntity()).ToList() ?? new List<Product>(),
            };
        }
    }
}
