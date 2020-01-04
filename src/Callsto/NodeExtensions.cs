// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Operations on a <see cref="Node{T}"/>.
    /// </summary>
    public static class NodeExtensions
    {
        /// <summary>
        /// Walks a forest of nodes in depth first order. Applies a tee function to each node.
        /// </summary>
        /// <param name="nodes">Set of nodes in the forest.</param>
        /// <param name="print">A print function to apply for each node.</param>
        /// <param name="leaf">A print function to apply for each leaf node.</param>
        /// <returns>Returns a set of nodes in the forest.</returns>
        /// <typeparam name="T">Type of tree node.</typeparam>
        public static IEnumerable<Node<T>> DepthFirstWalk<T>(
                this IEnumerable<Node<T>> nodes,
                Action<Node<T>> print,
                Action<Node<T>> leaf)
        {
            var stack = new Stack<Node<T>>();
            foreach (var root in nodes)
            {
                stack.Push(root);
                var visited = new HashSet<T>();

                while (stack.TryPop(out Node<T> node))
                {
                    var hasChildren = false;
                    if (visited.Add(node.Element))
                    {
                        hasChildren = node.Children.Any();
                        foreach (var child in node.Children)
                        {
                            stack.Push(child);
                        }
                    }

                    print(node);
                    if (!hasChildren)
                    {
                        leaf(node);
                    }
                }
            }

            return nodes;
        }
    }
}
