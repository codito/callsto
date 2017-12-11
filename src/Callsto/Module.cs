// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Generic;
    using Mono.Cecil;

    /// <inheritdoc/>
    public class Module : IModule
    {
        private string assemblyPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Module"/> class.
        /// </summary>
        /// <param name="assemblyPath">Full path to an assembly.</param>
        public Module(string assemblyPath)
        {
            this.assemblyPath = assemblyPath;
        }

        /// <inheritdoc/>
        public IEnumerable<TypeDefinition> Types
        {
            get;
        }
    }
}
