namespace Northwind.Data
{
    public static class OrderViewModelExtensions
    {
        public static OrderViewModel ToViewModel(this Order order, IContainerExtension container)
        {

            ArgumentNullException.ThrowIfNull(order);
            ArgumentNullException.ThrowIfNull(container);

            return new OrderViewModel(container)
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipperId,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry
            };

        }
        public static Order ToModel(this OrderViewModel viewModel)
        {

            ArgumentNullException.ThrowIfNull(viewModel);

            return new Order
            {
                OrderId = viewModel.OrderId,
                CustomerId = viewModel.CustomerId ?? string.Empty,
                EmployeeId = viewModel.EmployeeId,
                OrderDate = viewModel.OrderDate.GetValueOrDefault(),
                RequiredDate = viewModel.RequiredDate,
                ShippedDate = viewModel.ShippedDate,
                ShipperId = viewModel.ShipVia,
                Freight = viewModel.Freight,
                ShipName = viewModel.ShipName ?? string.Empty,
                ShipAddress = viewModel.ShipAddress ?? string.Empty,
                ShipCity = viewModel.ShipCity ?? string.Empty,
                ShipRegion = viewModel.ShipRegion ?? string.Empty,
                ShipPostalCode = viewModel.ShipPostalCode ?? string.Empty,
                ShipCountry = viewModel.ShipCountry ?? string.Empty
            };

        }
    }
}