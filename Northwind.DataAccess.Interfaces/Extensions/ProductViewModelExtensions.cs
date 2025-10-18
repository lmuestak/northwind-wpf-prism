using Northwind.Data;
using Prism.Ioc;
using System;
using System.ComponentModel;

namespace Northwind.ViewModels
{
    public static class ProductViewModelExtensions
    {
        public static ProductViewModel ToViewModel(this Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            return new ProductViewModel()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                ReorderLevel = product.ReorderLevel,
                Active = !product.Discontinued
            };
        }
        public static Product ToEntity(this ProductViewModel viewModel)
        {
            
            ArgumentNullException.ThrowIfNull(viewModel);
            return new Product
            {
                ProductId = viewModel.ProductId.GetValueOrDefault(),
                ProductName = viewModel.ProductName,
                SupplierId = viewModel.SupplierId,
                CategoryId = viewModel.CategoryId,
                QuantityPerUnit = viewModel.QuantityPerUnit,
                UnitPrice = viewModel.UnitPrice,
                UnitsInStock = viewModel.UnitsInStock,
                UnitsOnOrder = viewModel.UnitsOnOrder,
                ReorderLevel = viewModel.ReorderLevel,
                Discontinued = !viewModel.Active
            };
        }
    }
}
