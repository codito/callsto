// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System.Collections.Generic;
    using System.Linq;

    using CommandLine;
    using CommandLine.Text;
    using Serilog;

    /// <summary>
    /// Callsto application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Program entrypoint.
        /// </summary>
        /// <param name="args">Set of command line arguments.</param>
        /// <returns>0 if successful</returns>
        public static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<CallstoOptions>(args)
                .MapResult(opt => Run(opt), err => 1);
        }

        /// <summary>
        /// Runs the application with parsed options.
        /// </summary>
        /// <param name="options">Parsed options.</param>
        /// <returns>0 if successful</returns>
        private static int Run(CallstoOptions options)
        {
            var loggerConfig = new LoggerConfiguration();
            if (options.Debug)
            {
                loggerConfig = loggerConfig.MinimumLevel.Debug();
            }

            Log.Logger = loggerConfig.WriteTo.Console().CreateLogger();

            // Module.Explore(files).Methods().
            var callgraph = new CallGraph(options.Files);
            var graph = callgraph.Build();
            ConsoleOutput.ToConsole(graph);

            GraphConfig.Default
                       .Assembly(options.Files.First())
                       .Modules()
                       .SelectMany(m => m.Types)
                       .SelectMany(t => t.Methods)
                       .SelectMany(m => Method.Edges(m))
                       .Graph()
                       .DepthFirstWalk(m => System.Console.Write("{0}->", m.Element.ToString()), n => System.Console.WriteLine("END"))
                       .ToList()
                       .ForEach(m => System.Console.Write("\nM: " + m.Element.ToString()));

            return 0;
        }

        private class CallstoOptions
        {
            [Usage(ApplicationAlias = "callsto")]
            public static IEnumerable<Example> Examples
            {
                get
                {
                    yield return new Example("Command", new CallstoOptions { Files = new[] { "[OPTION]...", "[FILE]..." } });
                    yield return new Example("Print call graph to console", new CallstoOptions { Files = new[] { "foo.dll", "/tmp/bar.dll" } });
                    yield return new Example("Show diagnostic messages", new CallstoOptions { Files = new[] { "foo.dll" }, Debug = true });
                }
            }

            [Option('d', "debug", HelpText = "Print diagnostic messages")]
            public bool Debug { get; set; }

            [Value(0, MetaName = "[FILE]...", HelpText = "List of assemblies to explore", Required = true)]
            public IEnumerable<string> Files { get; set; }
        }
    }
}
