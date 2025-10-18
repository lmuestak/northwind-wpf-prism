using Northwind.Services.Interfaces;
using System;
using System.Configuration;
using System.Linq;

namespace Northwind.Services
{
    public class ConfigurationService : IConfigurationService
    {

        private readonly Configuration _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
        private SettingsConfigurationSection _userSettings;
        private static readonly string[] themes = ["dark", "light"];
        private static readonly string[] languages = ["en", "de", "fr", "it", "es"];
        public string GetTheme()
        {
            var theme = Settings.Theme;
            if (themes.Contains(theme, System.StringComparer.OrdinalIgnoreCase))
            {
                return theme;
            }
            else
            {
                Settings.Theme = "Light"; // Default to Light if not set or invalid
            }
            return Settings.Theme;
        }

        public string GetLanguage()
        {
            var language = Settings.Language;
            if (languages.Contains(language, System.StringComparer.OrdinalIgnoreCase))
            {
                return language;
            }
            else
            {
                Settings.Language = "us"; // Default to Light if not set or invalid
            }
            return Settings.Language;
        }

        public string GetDbProvider()
        {
            var dbProvider = Settings.DbProvider;
            if (!string.IsNullOrWhiteSpace(dbProvider))
            {
                return dbProvider;
            }
            else
            {
                Settings.DbProvider = "sqlite"; // sqlite/mssql
            }
            return Settings.DbProvider;
        }

        public string GetConnectionString()
        {
            var connectionString = Settings.ConnectionString;
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
#if DEBUG
                return "Filename=Northwind.debug.sqlite";
#else
                return connectionString;
#endif
            }
            else
            {
                var dbFile = "Northwind.sqlite";
#if DEBUG
                dbFile = "Northwind.debug.sqlite";
#endif
                Settings.ConnectionString = $"Filename={dbFile}"; //"Server=.;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True"; // Default to Light if not set or invalid
            }
            return Settings.ConnectionString;
        }

        public SettingsConfigurationSection Settings
        {
            get
            {
                if (_userSettings == null)
                {
                    _userSettings = _configuration.Sections["userSettings"] as SettingsConfigurationSection;
                    if (_userSettings == null)
                    {
                        _userSettings = SettingsConfigurationSection.GetSettings();
                        _configuration.Sections.Add("userSettings", _userSettings);
                        _configuration.Save(ConfigurationSaveMode.Modified);
                    }
                }
                return _userSettings;
            }
        }

        public void Save()
        {
            _configuration.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("userSettings");
        }
    }


}
