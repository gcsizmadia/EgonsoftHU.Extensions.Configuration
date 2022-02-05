// Copyright © 2022 Gabor Csizmadia
// This code is licensed under MIT license (see LICENSE for details)

using EgonsoftHU.Extensions.Configuration;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Contains an extension method for <see cref="IConfigurationBuilder"/>.
    /// </summary>
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds the ConfigurationManager configuration provider implementation.
        /// </summary>
        /// <param name="builder">The application configuration builder.</param>
        /// <returns>the same application configuration builder after adding the configuration provider to it.</returns>
        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder)
        {
            return builder.Add(new ConfigurationManagerConfigurationProvider());
        }
    }
}
