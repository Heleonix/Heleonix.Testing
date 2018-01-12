// <copyright file="AaaSpec.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa
{
    using System;
    using System.Collections.Generic;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Represents the specification for the AAA tests pattern.
    /// </summary>
    public static class AaaSpec
    {
        /// <summary>
        /// Builds the 'Act' step of the test.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Act(Action action)
        {
            var node = new SpecNode(SpecNodeType.Act, null, action);

            TestHost.Current.Add(node);
        }

        /// <summary>
        /// Builds the 'And' step of the test.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="action">The action.</param>
        public static void And(string description, Action action)
        {
            var node = new SpecNode(SpecNodeType.And, $"And {description}", action);

            TestHost.Current.Add(node);

            TestHost.Current.Execute(node);
        }

        /// <summary>
        /// Builds the 'Arrange' step of the test.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Arrange(Action action)
        {
            var node = new SpecNode(SpecNodeType.Arrange, null, action);

            TestHost.Current.Add(node);
        }

        /// <summary>
        /// Builds the 'Should' step of the test.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="action">The action.</param>
        public static void Should(string description, Action action)
        {
            var node = new SpecNode(SpecNodeType.Should, $"Should {description}", action);

            TestHost.Current.Add(node);

            var parents = new Stack<SpecNode>();
            var currentParent = node.Parent;

            // Include the Root
            while (currentParent != null)
            {
                parents.Push(currentParent);

                currentParent = currentParent.Parent;
            }

            foreach (var parent in parents)
            {
                foreach (var child in parent.Children)
                {
                    if (child.Type == SpecNodeType.Arrange)
                    {
                        TestHost.Current.Execute(child);
                    }
                }
            }

            foreach (var parent in parents)
            {
                foreach (var child in parent.Children)
                {
                    if (child.Type == SpecNodeType.Act)
                    {
                        TestHost.Current.Execute(child);
                    }
                }
            }

            TestHost.Current.Execute(node);

            foreach (var parent in parents)
            {
                foreach (var child in parent.Children)
                {
                    if (child.Type == SpecNodeType.Teardown)
                    {
                        TestHost.Current.Execute(child);
                    }
                }
            }
        }

        /// <summary>
        /// Builds the 'Teardown' step of the test.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void Teardown(Action action)
        {
            var node = new SpecNode(SpecNodeType.Teardown, null, action);

            TestHost.Current.Add(node);
        }

        /// <summary>
        /// Builds the 'When' step of the test.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="action">The action.</param>
        public static void When(string description, Action action)
        {
            var node = new SpecNode(SpecNodeType.When, $"When {description}", action);

            TestHost.Current.Add(node);

            TestHost.Current.Execute(node);
        }
    }
}
