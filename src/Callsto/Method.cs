// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    /// <summary>
    /// Abstractions of a Method in CIL.
    /// </summary>
    public static class Method
    {
        /// <summary>
        /// Gets the edges between a method and it's children.
        /// </summary>
        /// <param name="methodref">Method reference.</param>
        /// <returns>Edges between the method and it's children.</returns>
        public static IEnumerable<Edge<MethodReference>> Edges(MethodReference methodref)
        {
            var children = new List<MethodReference>();
            if (!methodref.IsDefinition)
            {
                // Skip exploration of external methods
                Enumerable.Empty<Edge<MethodReference>>();
            }

            var methodDef = methodref.Resolve();
            foreach (var instr in methodDef.Body.Instructions)
            {
                if (instr.OpCode.Equals(OpCodes.Call) || instr.OpCode.Equals(OpCodes.Callvirt))
                {
                    // Get method information
                    var child = instr.Operand as MethodReference;
                    if (child != null)
                    {
                        yield return new Edge<MethodReference>(methodref, child);
                    }

                    children.Add(child);

#if ENABLE
                    // Queue it up if local method
                    if (!this.methods.TryGetValue(methodref.FullName, out Node<MethodReference> childNode))
                    {
                        Log.Debug("Build: Create node for '{0}'", methodref.FullName);
                        childNode = new Node<MethodReference>(methodref);
                        this.methods.Add(methodref.FullName, childNode);
                    }

                    // Add the child node to parent
                    methodNode.Children.Add(childNode);
                    this.methodQueue.Enqueue(methodref);

                    // Child node is no longer a root
                    if (this.rootNodes.Contains(methodref.FullName))
                    {
                        // Recursive method, don't remove the root
                        if (!method.FullName.Equals(methodref.FullName))
                        {
                            this.rootNodes.Remove(methodref.FullName);
                        }
                    }
#endif
                }
            }

            children.ForEach(c => Edges(c));

            ////this.visitedNodes.Add(method.FullName);
        }
    }
}
