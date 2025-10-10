namespace Northwind.Data
{
    public interface ICategory
    {
        int CategoryId { get; set; }
        string CategoryName { get; set; }
        string Description { get; set; }
        byte[]? Picture { get; set; }
        byte[]? Icon17 { get; set; }
        byte[]? Icon25 { get; set; }
    }
}
