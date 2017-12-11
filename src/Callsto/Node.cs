// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Generic;

    /// <summary>
    /// A node of a tree.
    /// </summary>
    /// <typeparam name="T">Type of tree node.</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="element">Data for the node.</param>
        public Node(T element)
        {
            this.Element = element;
            this.Children = new List<Node<T>>();
        }

        /// <summary>
        /// Gets the element for this node.
        /// </summary>
        public T Element
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the set of outgoing edges.
        /// </summary>
        public List<Node<T>> Children
        {
            get;
            private set;
        }
    }
}
