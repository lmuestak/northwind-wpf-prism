using Northwind.Mvvm;

namespace Northwind.Data
{
    public static class OrderDetailViewModelExtensions
    {
        public static OrderDetailViewModel ToViewModel(this OrderDetail orderDetail, IContainerExtension container)
        {
            ArgumentNullException.ThrowIfNull(orderDetail);
            ArgumentNullException.ThrowIfNull(container);
            return new OrderDetailViewModel(container)
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
