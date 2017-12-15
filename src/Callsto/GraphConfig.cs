// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Generic;

    /// <summary>
    /// Configuration for a graph.
    /// </summary>
    public sealed class GraphConfig
    {
        private List<string> assemblies;

        private GraphConfig()
        {
            // Do not allow direct instantiation
            this.assemblies = new List<string>();
        }

        /// <summary>
        /// Gets a default instance of the configuration.
        /// </summary>
        public static GraphConfig Default => new GraphConfig();

        /// <summary>
        /// Gets the set of assemblies configured for exploration.
        /// </summary>
        public string[] Assemblies => this.assemblies.ToArray();

        /// <summary>
        /// Adds an assembly into configuration.
        /// </summary>
        /// <param name="path">Path of an assembly to explore.</param>
        /// <returns>Configuration instance.</returns>
        public GraphConfig Assembly(string path)
        {
            this.assemblies.Add(path);
            return this;
        }
    }
}
