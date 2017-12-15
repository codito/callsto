// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto.Tests
{
    using Callsto;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GraphConfigTests
    {
        [TestMethod]
        public void GraphConfigAssemblyAddsAndAssemblyToConfig()
        {
            var config = GraphConfig.Default.Assembly("foo.dll");

            CollectionAssert.AreEqual(new[] { "foo.dll" }, config.Assemblies);
        }

        [TestMethod]
        public void GraphConfigAssemblyCanAddMultiplePaths()
        {
            var config = GraphConfig.Default.Assembly("a.dll").Assembly("b.dll");

            CollectionAssert.AreEqual(new[] { "a.dll", "b.dll" }, config.Assemblies);
        }

        [TestMethod]
        public void GraphConfigModulesReturnsAnImmutableModulesList()
        {
            var config = GraphConfig.Default.Assembly("a.dll").Assembly("b.dll");

            Assert.AreEqual(2, config.Modules().Count);
        }
    }
}
