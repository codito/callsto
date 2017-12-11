// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    /// <summary>
    /// An edge of between nodes.
    /// </summary>
    /// <typeparam name="T">Type of tree node.</typeparam>
    public class Edge<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Edge{T}"/> class.
        /// </summary>
        /// <param name="start">Start node for the edge.</param>
        /// <param name="end">End node for the edge.</param>
        public Edge(T start, T end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Gets the destination node reference.
        /// </summary>
        public T End
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the source node reference.
        /// </summary>
        public T Start
        {
            get;
            private set;
        }
    }
}
