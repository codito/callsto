// Copyright (c) Arun Mahapatra. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Callsto
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Primitives to export to console.
    /// </summary>
    public static class ConsoleOutput
    {
        /// <summary>
        /// Prints the graphs on a console.
        /// </summary>
        /// <param name="roots">Root nodes of graphs in the pseudoforest.</param>
        /// <typeparam name="T">Base type for the graph.</typeparam>
        public static void ToConsole<T>(IEnumerable<Node<T>> roots)
        {
            // TODO refactor the console configuration
            foreach (var root in roots)
            {
                var visitedNodes = new HashSet<string>();
                var stack = new Stack<Tuple<Node<T>, int>>();
                var indentLevel = 0;
                stack.Push(new Tuple<Node<T>, int>(root, indentLevel));
                var newline = false;

                // Traverse to the deepest node
                while (stack.TryPop(out Tuple<Node<T>, int> item))
                {
                    var top = System.Console.CursorTop;
                    var left = System.Console.CursorLeft;

                    var node = item.Item1;
                    var level = item.Item2;

                    var format = "->{0}";
                    if (newline)
                    {
                        newline = false;
                    }

                    System.Console.SetCursorPosition(left + level, top);
                    System.Console.Write(format, node.Element.ToString());
                    System.Console.SetCursorPosition(left, top);
                    if (!visitedNodes.Contains(node.Element.ToString()))
                    {
                        visitedNodes.Add(node.Element.ToString());
                        level += node.Element.ToString().Length + format.Length;

                        // Push children onto stack
                        if (node.Children.Count != 0)
                        {
                            node.Children.ForEach(c => stack.Push(new Tuple<Node<T>, int>(c, level)));
                            continue;
                        }
                    }

                    // Struck a leaf node, rollback to previous level
                    newline = newline ? false : true;
                    System.Console.Write("\n");
                }
            }
        }
    }
}
