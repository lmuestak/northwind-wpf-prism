using Northwind.Data;
using Northwind.Mvvm;

namespace Northwind.ViewModels
{
    public static class OrderDetailViewModelExtensions
    {
        public static OrderDetailViewModel ToViewModel(this OrderDetail orderDetail)
        {
            ArgumentNullException.ThrowIfNull(orderDetail);
            return new OrderDetailViewModel()
            {
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                UnitPrice = orderDetail.UnitPrice,
                Quantity = orderDetail.Quantity,
                Discount = orderDetail.Discount,
                State = EntityState.Unchanged
            };
        }
        public static OrderDetail ToEntity(this OrderDetailViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new OrderDetail
            {
                OrderId = viewModel.OrderId,
                ProductId = viewModel.ProductId,
                UnitPrice = viewModel.UnitPrice,
                Quantity = viewModel.Quantity,
                Discount = viewModel.Discount
            };
        }
    }
}
