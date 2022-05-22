using Microsoft.Extensions.Configuration;

namespace ConfigurationService.JSON
{
    public class ConfigurationModulService
    {
        //Interface
        private IConfiguration config;

        /// <summary>
        /// Holt den Configurationseintrag aus dem JSON
        /// </summary>
        /// <param name="selConfigFile">Name der Konfigurationsdatei</param>
        /// <param name="selValue">Name des Eintrags</param>
        /// <returns></returns>
        public string GetConfigurationStringValue(string selConfigFile, string selValue)
        {
            config = new ConfigurationBuilder()
           .AddJsonFile(selConfigFile, optional: true, reloadOnChange: true)
           .Build();

            return config.GetValue<string>(selValue);
        }
    }
}
