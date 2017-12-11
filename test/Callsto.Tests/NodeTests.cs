// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NodeTests
    {
        [TestMethod]
        public void ConstructorCreatesANode()
        {
            var node = new Node<string>("dummy");

            Assert.AreEqual("dummy", node.Element);
        }

        [TestMethod]
        public void ChildrenShouldBeEmpty()
        {
            var node = new Node<string>("dummy");

            Assert.AreEqual(0, node.Children.Count);
        }
    }
}
