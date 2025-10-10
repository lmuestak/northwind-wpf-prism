using Northwind.Mvvm;

namespace Northwind.Data
{
    public class OrderDetailViewModel(IContainerExtension container) : EntityViewModel<OrderDetail>(container)
    {
        private int _orderId;
        private int _productId;
        private decimal _unitPrice;
        private short _quantity;
        private float _discount;
        public int OrderId
        {
            get => _orderId;
            set => SetProperty(ref _orderId, value);
        }
        public int ProductId
        {
            get => _productId;
            set => SetProperty(ref _productId, value);
        }
        public decimal UnitPrice
        {
            get => _unitPrice;
            set => SetProperty(ref _unitPrice, value);
        }
        public short Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        public float Discount
        {
            get => _discount;
            set => SetProperty(ref _discount, value);
        }
    }
}
