namespace Northwind.Data
{
    public class SalesOverviewByEmployeeView
    {
        public int OrderId { get; set; }
        public string Country { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Discount percentage (e.g., 10 = 10%).
        /// </summary>
        public decimal DiscountPercent { get; set; }

        public decimal PriceTotal => UnitPrice * Quantity;
        public decimal DiscountTotal => (PriceTotal * DiscountPercent) / 100m;
        public decimal SalesPrice => PriceTotal - DiscountTotal;
        public string FullName { get; set; } = string.Empty;

    }
}