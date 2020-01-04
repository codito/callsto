// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NodeExtensionsTests
    {
        private readonly List<List<int>> nodes;

        public NodeExtensionsTests()
        {
            this.nodes = new List<List<int>>();
            this.nodes.Add(new List<int>());
        }

        [TestMethod]
        public void DepthFirstWalkShouldWalkSingleNode()
        {
            var t = new Dictionary<int, List<int>> { { 1, new List<int>() } };
            var tree = this.CreateTree(t);

            tree.DepthFirstWalk(n => this.nodes.Last().Add(n.Element), _ => this.nodes.Add(new List<int>()));

            CollectionAssert.AreEqual(new[] { 1 }, this.nodes[0]);
        }

        [TestMethod]
        public void DepthFirstWalkShouldWalkMultipleNodes()
        {
            var t = new Dictionary<int, List<int>> { { 1, new List<int> { 2, 3, 4 } } };
            var tree = this.CreateTree(t);

            tree.DepthFirstWalk(n => this.nodes.Last().Add(n.Element), _ => this.nodes.Add(new List<int>()));

            CollectionAssert.AreEqual(new[] { 1, 4 }, this.nodes[0]);
            CollectionAssert.AreEqual(new[] { 3 }, this.nodes[1]);
            CollectionAssert.AreEqual(new[] { 2 }, this.nodes[2]);
        }

        [TestMethod]
        public void DepthFirstWalkShouldSkipCycles()
        {
            var t = new Dictionary<int, List<int>> { { 1, new List<int> { 2, 1 } } };
            var tree = this.CreateTree(t);

            tree.DepthFirstWalk(n => this.nodes.Last().Add(n.Element), _ => this.nodes.Add(new List<int>()));

            CollectionAssert.AreEqual(new[] { 1, 1 }, this.nodes[0]);
            CollectionAssert.AreEqual(new[] { 2 }, this.nodes[1]);
        }

        private IEnumerable<Node<int>> CreateTree(Dictionary<int, List<int>> nodes)
        {
            foreach (var node in nodes)
            {
                var root = new Node<int>(node.Key);
                foreach (var c in node.Value)
                {
                    root.Children.Add(new Node<int>(c));
                }

                yield return root;
            }
        }
    }
}
