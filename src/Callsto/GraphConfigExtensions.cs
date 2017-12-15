// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Immutable;
    using System.Linq;

    /// <summary>
    /// Extensions for a configuration instance.
    /// </summary>
    public static class GraphConfigExtensions
    {
        /// <summary>
        /// Gets an immutable list of modules for this configuration.
        /// </summary>
        /// <param name="config">Configuration instance.</param>
        /// <returns>List of modules.</returns>
        public static ImmutableList<Module> Modules(this GraphConfig config)
        {
            return config.Assemblies
                         .Select(a => new Module(a))
                         .ToImmutableList<Module>();
        }
    }
}
