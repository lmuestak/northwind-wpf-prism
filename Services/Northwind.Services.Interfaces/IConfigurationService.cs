namespace Northwind.Services.Interfaces
{
    public interface IConfigurationService
    {
        string GetConnectionString();
        string GetDbProvider();
        string GetTheme();
        string GetLanguage();
        void Save();
    }
}
