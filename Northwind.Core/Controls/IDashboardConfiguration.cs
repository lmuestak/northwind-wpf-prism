namespace Northwind.Core.Controls
{
    public interface IDashboardConfiguration
    {
        void DashboardConfigurationComplete(DashboardConfigurationType type, bool save, string newName);
        DashboardNameValidResponse DashboardNameValid(string name);

    }
}
