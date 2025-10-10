using Northwind.Mvvm;

namespace Northwind.Data
{
    public class OrderDetailsExtendedViewModel(IContainerExtension container) : EntityViewModel<OrderDetailsExtended>(container)
    {
        private int _orderId;
        private string _productName = string.Empty;
        private decimal? _unitPrice;
        private short? _quantity;
        private float? _discount;
        private decimal? _extendedPrice;
        public int OrderId
        {
            get => _orderId;
            set => SetProperty(ref _orderId, value);
        }
        public string ProductName
        {
            get => _productName;
            set => SetProperty(ref _productName, value);
        }
        public decimal? UnitPrice
        {
            get => _unitPrice;
            set => SetProperty(ref _unitPrice, value);
        }
        public short? Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        public float? Discount
        {
            get => _discount;
            set => SetProperty(ref _discount, value);
        }
        public decimal? ExtendedPrice
        {
            get => _extendedPrice;
            set => SetProperty(ref _extendedPrice, value);
        }
    }

    public static class OrderDetailsExtendedViewModelExtensions
    {
        public static OrderDetailsExtendedViewModel ToViewModel(this OrderDetailsExtended orderDetailsExtended, IContainerExtension container)
        {
            ArgumentNullException.ThrowIfNull(orderDetailsExtended);
            ArgumentNullException.ThrowIfNull(container);
            return new OrderDetailsExtendedViewModel(container)
            {
                OrderId = orderDetailsExtended.OrderId,
                ProductName = orderDetailsExtended.ProductName ?? string.Empty,
                UnitPrice = orderDetailsExtended.UnitPrice,
                Quantity = orderDetailsExtended.Quantity,
                Discount = orderDetailsExtended.Discount,
                ExtendedPrice = orderDetailsExtended.ExtendedPrice
            };
        }
        public static OrderDetailsExtended ToEntity(this OrderDetailsExtendedViewModel viewModel)
        {
            ArgumentNullException.ThrowIfNull(viewModel);
            return new OrderDetailsExtended
            {
                OrderId = viewModel.OrderId,
                ProductName = viewModel.ProductName ?? string.Empty,
                UnitPrice = viewModel.UnitPrice,
                Quantity = viewModel.Quantity,
                Discount = viewModel.Discount,
                ExtendedPrice = viewModel.ExtendedPrice
            };
        }
    }
}