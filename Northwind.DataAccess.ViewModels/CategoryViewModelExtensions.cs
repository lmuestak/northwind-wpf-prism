using Northwind.Mvvm;

namespace Northwind.Data
{
    public static class CategoryViewModelExtensions
    {
        public static CategoryViewModel ToViewModel(this Category model, IContainerExtension container)
        {

            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(container);

            return new CategoryViewModel(container)
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                Description = model.Description,
                Picture = model.Picture,
                Icon17 = model.Icon17,
                Icon25 = model.Icon25,
                State = EntityState.Unchanged
            };

        }

        public static Category ToEntity(this CategoryViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Category
            {
                CategoryId = viewModel.CategoryId,
                CategoryName = viewModel.CategoryName,
                Description = viewModel.Description,
                Picture = viewModel.Picture,
                Icon17 = viewModel.Icon17,
                Icon25 = viewModel.Icon25
            };
        }
    }
}
