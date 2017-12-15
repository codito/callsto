// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Operations on edges.
    /// </summary>
    public static class EdgeExtensions
    {
        /// <summary>
        /// Builds a graph.
        /// </summary>
        /// <param name="edges">Set of edges.</param>
        /// <returns>Returns a set of graphs from the edges.</returns>
        /// <typeparam name="T">Type of tree node.</typeparam>
        public static IEnumerable<Node<T>> Graph<T>(this IEnumerable<Edge<T>> edges)
        {
            var nodes = new Dictionary<T, Node<T>>();
            var roots = new HashSet<T>();
            foreach (var edge in edges)
            {
                roots.Add(edge.Start);
                roots.Add(edge.End);

                if (!nodes.TryGetValue(edge.Start, out Node<T> parentNode))
                {
                    parentNode = new Node<T>(edge.Start);
                    nodes.Add(edge.Start, parentNode);
                }

                if (!nodes.TryGetValue(edge.End, out Node<T> childNode))
                {
                    childNode = new Node<T>(edge.End);
                    nodes.Add(edge.End, childNode);
                    roots.Remove(edge.End);
                }

                parentNode.Children.Add(childNode);
            }

            return roots.Select(r => nodes[r]);
        }
    }
}
