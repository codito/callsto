// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using CommandLine;
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
        public static void Main(string[] args)
        {
            var loggerConfig = new LoggerConfiguration();
            var parser = new Parser(with => with.EnableDashDash = true);
            var result = parser.ParseArguments<CallstoOptions>(args)
                .WithParsed(option =>
                        {
                            if (option.Debug)
                            {
                                loggerConfig = loggerConfig.MinimumLevel.Debug();
                            }
                        });

            Log.Logger = loggerConfig.WriteTo.Console().CreateLogger();
            var callgraph = new CallGraph(new[] { args[0] });
            var graph = callgraph.Build();
            ConsoleOutput.ToConsole(graph);
        }

        private class CallstoOptions
        {
            [Option('d')]
            public bool Debug { get; set; }
        }
    }
}
