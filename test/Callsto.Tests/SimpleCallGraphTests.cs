// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto.Tests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SimpleCallGraphTests : CallGraphTests
    {
        private CallGraph graph;

        public SimpleCallGraphTests()
            : base("SimpleTest")
        {
            this.graph = new CallGraph(new[] { this.AssetPath });
        }

        [TestMethod]
        public void CallGraphBuildShouldCreateAMethodGraph()
        {
            var trees = this.graph.Build().ToList();

            Assert.AreEqual(6, trees.Count);
        }
    }
}
