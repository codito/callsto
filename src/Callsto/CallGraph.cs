// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using Serilog;

    /// <summary>
    /// Provides a call graph for all types in a module.
    /// </summary>
    public class CallGraph
    {
        private IEnumerable<string> assemblies;
        private Dictionary<string, Node<MethodReference>> methods;
        private Queue<MethodReference> methodQueue;
        private HashSet<string> rootNodes;
        private HashSet<string> visitedNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallGraph"/> class.
        /// </summary>
        /// <param name="assemblies">Set of assemblies</param>
        public CallGraph(IEnumerable<string> assemblies)
        {
            this.assemblies = assemblies;
            this.methods = new Dictionary<string, Node<MethodReference>>();
            this.methodQueue = new Queue<MethodReference>();
            this.rootNodes = new HashSet<string>();
            this.visitedNodes = new HashSet<string>();
        }

        /// <summary>
        /// Builds a graph of methods in the assemblies.
        /// </summary>
        /// <returns>Set of root nodes in the forest</returns>
        public IEnumerable<Node<MethodReference>> Build()
        {
            var module = ModuleDefinition.ReadModule(this.assemblies.First());

            // Seed the method list with initial set of known methods
            foreach (var type in module.Types)
            {
                foreach (var m in type.Methods)
                {
                    this.methods.Add(m.FullName, new Node<MethodReference>(m));
                    this.methodQueue.Enqueue(m);
                    this.rootNodes.Add(m.FullName);
                }
            }

            while (this.methodQueue.TryDequeue(out MethodReference method))
            {
                // Skip if this node is already visited
                if (this.visitedNodes.Contains(method.FullName))
                {
                    continue;
                }

                Log.Debug("Build: New method '{0}'", method.FullName);
                var methodNode = this.methods[method.FullName];
                if (!method.IsDefinition)
                {
                    // Skip exploration of external methods
                    continue;
                }

                var methodDef = method.Resolve();
                foreach (var instr in methodDef.Body.Instructions)
                {
                    if (instr.OpCode.Equals(OpCodes.Call) || instr.OpCode.Equals(OpCodes.Callvirt))
                    {
                        // Get method information
                        var methodref = instr.Operand as MethodReference;

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
                    }
                }

                this.visitedNodes.Add(method.FullName);
            }

            return this.rootNodes.Select(r => this.methods[r]);
        }
    }
}
