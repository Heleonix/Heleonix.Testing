// <copyright file="BddSpec.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Bdd
{
    using System;
    using System.Collections.Generic;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Represents the specification for the BDD tests pattern.
    /// </summary>
    public static class BddSpec
    {
        /// <summary>
        /// Builds the 'AfterEach' step of the test.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void AfterEach(Action action)
        {
            var node = new SpecNode(SpecNodeType.AfterEach, null, action);

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
        /// Builds the 'BeforeEach' step of the test.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void BeforeEach(Action action)
        {
            var node = new SpecNode(SpecNodeType.BeforeEach, null, action);

            TestHost.Current.Add(node);
        }

        /// <summary>
        /// Builds the 'CleanupEach' step of the test.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void CleanupEach(Action action)
        {
            var node = new SpecNode(SpecNodeType.CleanupEach, null, action);

            TestHost.Current.Add(node);
        }

        /// <summary>
        /// Builds the 'Given' step of the test.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="action">The action.</param>
        public static void Given(string description, Action action)
        {
            var node = new SpecNode(SpecNodeType.Given, $"Given {description}", action);

            TestHost.Current.Add(node);

            TestHost.Current.Execute(node);
        }

        /// <summary>
        /// Builds the 'SetupEach' step of the test.
        /// </summary>
        /// <param name="action">The action.</param>
        public static void SetupEach(Action action)
        {
            var node = new SpecNode(SpecNodeType.SetupEach, null, action);

            TestHost.Current.Add(node);
        }

        /// <summary>
        /// Builds the 'Then' step of the test.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="action">The action.</param>
        public static void Then(string description, Action action)
        {
            var node = new SpecNode(SpecNodeType.Then, $"Then {description}", action);

            TestHost.Current.Add(node);

            var parents = new Stack<SpecNode>();
            var currentParent = node.Parent;

            // Exclude the Root
            while (currentParent.Type != SpecNodeType.Root)
            {
                parents.Push(currentParent);

                currentParent = currentParent.Parent;
            }

            foreach (var parent in parents)
            {
                foreach (var child in parent.Children)
                {
                    if (child.Type == SpecNodeType.SetupEach)
                    {
                        TestHost.Current.Execute(child);
                    }
                }
            }

            foreach (var parent in parents)
            {
                foreach (var child in parent.Children)
                {
                    if (child.Type == SpecNodeType.BeforeEach)
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
                    if (child.Type == SpecNodeType.AfterEach)
                    {
                        TestHost.Current.Execute(child);
                    }
                }
            }

            foreach (var parent in parents)
            {
                foreach (var child in parent.Children)
                {
                    if (child.Type == SpecNodeType.CleanupEach)
                    {
                        TestHost.Current.Execute(child);
                    }
                }
            }
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
