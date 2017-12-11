// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto.Tests
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class CallGraphTests
    {
#if DEBUG
        private const string Configuration = "Debug";
#else
        private const string Configuration = "Release";
#endif
        private const string Framework = "netstandard2.0";
        private const string AssetsPath = "../../../../assets/{0}/bin/{1}/{2}/{0}.dll";

        private string assemblyPath;
        private MemoryStream assemblyStream;

        public CallGraphTests(string assemblyPath)
        {
            this.assemblyPath = assemblyPath;
        }

        public string AssetPath
        {
            get
            {
                return string.Format(AssetsPath, this.assemblyPath, Configuration, Framework);
            }
        }

        public Stream GetAssemblyStream()
        {
            // FIXME should remove this?
            if (this.assemblyStream == null)
            {
                // Create a cached stream
                this.assemblyStream = new MemoryStream();
                var asset = this.AssetPath;
                using (var s = File.OpenRead(asset))
                {
                    s.CopyTo(this.assemblyStream);
                }
            }

            var stream = new MemoryStream();
            this.assemblyStream.CopyTo(stream);
            return stream;
        }
    }
}
