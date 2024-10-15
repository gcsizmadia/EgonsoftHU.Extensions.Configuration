// Copyright © 2022-2024 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using System.Configuration;
using System.Linq;

using Microsoft.Extensions.Configuration;

using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace EgonsoftHU.Extensions.Configuration
{
    /// <summary>
    /// App.config/Web.config configuration provider implementation for Microsoft.Extensions.Configuration.
    /// </summary>
    /// <remarks>Uses <see cref="ConfigurationManager" /> to load AppSettings and ConnectionStrings.</remarks>
    public class ConfigurationManagerConfigurationProvider : ConfigurationProvider, IConfigurationSource
    {
        /// <inheritdoc />
        public override void Load()
        {
            foreach (ConnectionStringSettings setting in ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>())
            {
                Data.Add($"{nameof(ConfigurationManager.ConnectionStrings)}:{setting.Name}", setting.ConnectionString);
            }

            foreach (string? key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key is null)
                {
                    continue;
                }

                Data.Add(key, ConfigurationManager.AppSettings[key]);
            }
        }

        /// <inheritdoc />
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return this;
        }
    }
}
