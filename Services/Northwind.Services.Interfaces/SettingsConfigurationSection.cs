using System.Configuration;

namespace Northwind.Services.Interfaces
{
    public class SettingsConfigurationSection : ConfigurationSection
    {
        private const string SectionName = "userSettings";
        public static SettingsConfigurationSection GetSettings()
        {
            return (SettingsConfigurationSection)ConfigurationManager.GetSection(SectionName) ?? new SettingsConfigurationSection();
        }

        [ConfigurationProperty("dbProvider", IsRequired = true, DefaultValue = "sqlite")]
        public string DbProvider
        {
            get { return (string)this["dbProvider"]; }
            set { this["dbProvider"] = value; }
        }

        [ConfigurationProperty("connectionString", IsRequired = true, DefaultValue = "Data Source=Northwind.sqlite")]
        public string ConnectionString
        {
            get { return (string)this["connectionString"]; }
            set { this["connectionString"] = value; }
        }

        [ConfigurationProperty("apiBaseUrl", IsRequired = false, DefaultValue = "https://api.northwind.com")]
        public string ApiBaseUrl
        {
            get { return (string)this["apiBaseUrl"]; }
            set { this["apiBaseUrl"] = value; }
        }

        [ConfigurationProperty("theme", IsRequired = true, DefaultValue = "light")]
        public string Theme
        {
            get { return (string)this["theme"]; }
            set { this["theme"] = value; }
        }

        [ConfigurationProperty("language", DefaultValue = "en")]
        public string Language
        {
            get { return (string)this["language"]; }
            set { this["language"] = value; }
        }
    }
}
