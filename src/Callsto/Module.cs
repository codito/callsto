// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Immutable;
    using Mono.Cecil;

    /// <summary>
    /// Abstracts a net assembly module.
    /// </summary>
    public class Module
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

        /// <summary>
        /// Gets the types defined in this module.
        /// </summary>
        public ImmutableList<TypeDefinition> Types
        {
            get
            {
                return ModuleDefinition.ReadModule(this.assemblyPath).Types.ToImmutableList();
            }
        }
    }
}
