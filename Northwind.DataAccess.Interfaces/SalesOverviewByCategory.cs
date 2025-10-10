namespace Northwind.DataAccess
{
    public class SalesOverviewByCategory
    {
        public int OrderId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
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

    }
}
