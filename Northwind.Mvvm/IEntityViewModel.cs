namespace Northwind.Mvvm
{
    public interface IEntityViewModel<T>
    {
        EntityState State { get; set; }
    }

}
