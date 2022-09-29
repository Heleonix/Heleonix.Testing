// <copyright file="TestHost.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using global::NUnit.Framework;

    /// <summary>
    /// Represents a base test host to be implemented by implementations of different tests patterns.
    /// </summary>
    internal abstract class TestHost
    {
        private readonly SpecNode root = new SpecNode(SpecNodeType.Root, null, null);
        private readonly Stack<SpecNode> specExecutionStack = new Stack<SpecNode>();

        /// <summary>
        /// Gets the current instance of the <see cref="TestHost"/>.
        /// </summary>
        /// <value>
        /// The current instance of the <see cref="TestHost"/>.
        /// </value>
        public static TestHost Current => TestProperties.GetTestHost(TestContext.CurrentContext.Test.Properties);

        /// <summary>
        /// Gets the spec structure rules.
        /// </summary>
        /// <value>
        /// The spec structure rules.
        /// </value>
        protected abstract IDictionary<SpecNodeType, SpecStructureRule> SpecStructureRules { get; }

        /// <summary>
        /// Removes the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        public static void Remove(SpecNode node)
        {
            node.Parent.Remove(node);
        }

        /// <summary>
        /// Adds the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void Add(SpecNode node)
        {
            this.ValidateAdding(node);

            var parent = this.specExecutionStack.Count > 0 ? this.specExecutionStack.Peek() : this.root;

            parent.Add(node);
        }

        /// <summary>
        /// Executes the specified node.
        /// </summary>
        /// <param name="node">The node.</param>
        public void Execute(SpecNode node)
        {
            var properties = TestContext.CurrentContext.Test.Properties;

            if (!TestProperties.IsOutputWritten(properties))
            {
                foreach (var prop in TestProperties.GetOutput(properties))
                {
                    TestContext.WriteLine(prop);
                }

                TestProperties.SetIsOutputWritten(properties);
            }

            if (!string.IsNullOrEmpty(node.Description))
            {
                TestContext.WriteLine($"{new string(' ', node.NestingLevel * 4)}{node.Description}");
            }

            this.specExecutionStack.Push(node);

            try
            {
                node.Action();
            }
            finally
            {
                this.specExecutionStack.Pop();
            }
        }

        private void ValidateAdding(SpecNode child)
        {
            if (!this.SpecStructureRules.ContainsKey(child.Type))
            {
                throw new InvalidOperationException($"The spec '{child.Type}' is not valid for the current test pattern");
            }

            var rule = this.SpecStructureRules[child.Type];

            if (rule.SpecExecutionStackRule != null
                && !Regex.IsMatch(string.Join(",", this.specExecutionStack), rule.SpecExecutionStackRule))
            {
                throw new InvalidOperationException($"Invalid test structure: cannot place the '{child.Type}' " +
                    $"into the '{string.Join("->", this.specExecutionStack.Reverse())}'");
            }

            if (rule.PredecessorsRule != null)
            {
                var parent = this.specExecutionStack.Count > 0 ? this.specExecutionStack.Peek() : this.root;

                if (!Regex.IsMatch(string.Join(",", parent.Children.Reverse()), rule.PredecessorsRule))
                {
                    throw new InvalidOperationException($"Invalid test structure: cannot place the '{child.Type}' " +
                    $"after the '{string.Join("->", parent.Children)}'");
                }
            }
        }
    }
}
