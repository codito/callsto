// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Generic;
    using Mono.Cecil;

    /// <summary>
    /// Abstracts a net assembly module.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Gets the types defined in this module.
        /// </summary>
        IEnumerable<TypeDefinition> Types
        {
            get;
        }
    }
}
