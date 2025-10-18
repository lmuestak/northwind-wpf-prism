namespace Northwind.Data
{
    public class SalesReportData
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int EmployeeId { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public int ShipperId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string? EmployeeTerritoryId { get; set; } = string.Empty;
        public int RegionId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string ShipperName { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string TerritoryName { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public DateTime OrderAt { get; set; }
        public int OrderAtId { get; set; }
        public int OrderYear { get; set; }
        public int OrderMonth { get; set; }
        public int OrderQuarter { get; set; }
        public int OrderDay { get; set; }
        public DateTime? RequiredAt { get; set; }
        public int? RequiredAtId { get; set; }
        public int? RequiredYear { get; set; }
        public int? RequiredMonth { get; set; }
        public int? RequiredQuarter { get; set; }
        public int? RequiredDay { get; set; }
        public DateTime? ShippedAt { get; set; }
        public int? ShippedAtId { get; set; }
        public int? ShippedYear { get; set; }
        public int? ShippedMonth { get; set; }
        public int? ShippedQuarter { get; set; }
        public int? ShippedDay { get; set; }
        public decimal? UnitPrice { get; set; } = 0;
        public short? Quantity { get; set; } = 0;
        public float? Discount { get; set; } = 0;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Region { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;

    }
}