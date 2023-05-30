// <copyright file="BddSpecTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Aaa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Internal;
    using Heleonix.Testing.NUnit.Bdd;
    using Heleonix.Testing.NUnit.Bdd.Internal;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Tests the <see cref="BddSpec"/>.
    /// </summary>
    public static class BddSpecTests
    {
        /// <summary>
        /// Tests the <see cref="BddSpec.AfterEach(Action)"/>.
        /// </summary>
        [Test(Description = "Should add the spec to the parent node")]
        public static void AfterEach()
        {
            // Arrange
            var host = new BddTestHost(-1);
            Action action = () => { };
            var parentNode = new SpecNode(SpecNodeType.Given, null, () =>
            {
                // Act
                BddSpec.AfterEach(action);
            });

            TestPropertiesHelper.SetTestHost(TestExecutionContext.CurrentContext.CurrentTest.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.AfterEach));
            Assert.That(node.Description, Is.Null);
            Assert.That(node.NestingLevel, Is.EqualTo(1));
            Assert.That(node.Parent, Is.EqualTo(parentNode));
        }

        /// <summary>
        /// Tests the <see cref="BddSpec.And(string, Action)"/>.
        /// </summary>
        [Test(Description = "Should add and execute the spec to the parent node")]
        public static void And()
        {
            // Arrange
            var host = new BddTestHost(-1);
            var actionExecuted = false;
            Action action = () =>
            {
                actionExecuted = true;
            };
            var parentNode = new SpecNode(SpecNodeType.Given, null, () =>
            {
                // Act
                BddSpec.And("description", action);
            });

            TestPropertiesHelper.SetTestHost(TestExecutionContext.CurrentContext.CurrentTest.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(actionExecuted, Is.True);
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.And));
            Assert.That(node.Description, Is.EqualTo("And description"));
            Assert.That(node.NestingLevel, Is.EqualTo(1));
            Assert.That(node.Parent, Is.EqualTo(parentNode));
        }

        /// <summary>
        /// Tests the <see cref="BddSpec.BeforeEach(Action)"/>.
        /// </summary>
        [Test(Description = "Should add the spec to the parent node")]
        public static void BeforeEach()
        {
            // Arrange
            var host = new BddTestHost(-1);
            Action action = () => { };
            var parentNode = new SpecNode(SpecNodeType.Given, null, () =>
            {
                // Act
                BddSpec.BeforeEach(action);
            });

            TestPropertiesHelper.SetTestHost(TestExecutionContext.CurrentContext.CurrentTest.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.BeforeEach));
            Assert.That(node.Description, Is.Null);
            Assert.That(node.NestingLevel, Is.EqualTo(1));
            Assert.That(node.Parent, Is.EqualTo(parentNode));
        }

        /// <summary>
        /// Tests the <see cref="BddSpec.Given(string, Action)"/>.
        /// </summary>
        [Test(Description = "Should add and execute the spec to the parent node")]
        public static void Given()
        {
            // Arrange
            var host = new BddTestHost(-1);
            SpecNode rootNode = null;
            var actionExecuted = false;
            Action action = () =>
            {
                actionExecuted = true;
            };
            var siblingNode = new SpecNode(SpecNodeType.Given, null, () => { });

            TestPropertiesHelper.SetTestHost(TestExecutionContext.CurrentContext.CurrentTest.Properties, host);

            host.Add(siblingNode);

            rootNode = siblingNode.Parent;

            TestHost.Remove(siblingNode);

            // Act
            BddSpec.Given("description", action);

            // Assert
            var node = rootNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(actionExecuted, Is.True);
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.Given));
            Assert.That(node.Description, Is.EqualTo("Given description"));
            Assert.That(node.NestingLevel, Is.EqualTo(0));
            Assert.That(node.Parent, Is.EqualTo(rootNode));
        }

        /// <summary>
        /// Tests the <see cref="BddSpec.Then(string, Action)"/>.
        /// </summary>
        [Test(Description = "Should add and execute the spec to the parent node")]
        public static void Then()
        {
            // Arrange
            var host = new BddTestHost(-1);
            var actionExecuted = false;
            var executionStack = new Stack<SpecNodeType>();
            Action action = () =>
            {
                actionExecuted = true;
            };

            var parentNode = new SpecNode(SpecNodeType.Given, null, () =>
            {
                // Arrange
                BddSpec.When(null, () =>
                {
                    executionStack.Push(SpecNodeType.When);

                    BddSpec.BeforeEach(() => { executionStack.Push(SpecNodeType.BeforeEach); });

                    BddSpec.AfterEach(() => { executionStack.Push(SpecNodeType.AfterEach); });

                    BddSpec.And(null, () =>
                    {
                        executionStack.Push(SpecNodeType.And);

                        BddSpec.BeforeEach(() => { executionStack.Push(SpecNodeType.BeforeEach); });

                        BddSpec.AfterEach(() => { executionStack.Push(SpecNodeType.AfterEach); });

                        // Act
                        BddSpec.Then("description", action);
                    });
                });
            });

            TestPropertiesHelper.SetTestHost(TestExecutionContext.CurrentContext.CurrentTest.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.Single().Children.Last().Children.Last();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(actionExecuted, Is.True);
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.Then));
            Assert.That(node.Description, Is.EqualTo("Then description"));
            Assert.That(node.NestingLevel, Is.EqualTo(3));
            Assert.That(node.Parent.Type, Is.EqualTo(SpecNodeType.And));
            Assert.That(node.Parent.Parent.Parent, Is.EqualTo(parentNode));

            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.AfterEach));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.AfterEach));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.BeforeEach));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.BeforeEach));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.And));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.When));
        }

        /// <summary>
        /// Tests the <see cref="BddSpec.When(string, Action)"/>.
        /// </summary>
        [Test(Description = "Should add and execute the spec to the parent node")]
        public static void When()
        {
            // Arrange
            var host = new BddTestHost(-1);
            Action action = () => { };
            var parentNode = new SpecNode(SpecNodeType.Given, null, () =>
            {
                // Act
                BddSpec.When("description", action);
            });

            TestPropertiesHelper.SetTestHost(TestExecutionContext.CurrentContext.CurrentTest.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.When));
            Assert.That(node.Description, Is.EqualTo("When description"));
            Assert.That(node.NestingLevel, Is.EqualTo(1));
            Assert.That(node.Parent, Is.EqualTo(parentNode));
        }

        /// <summary>
        /// Tests integration of specs.
        /// </summary>
        [Scenario(Name = nameof(Example))]
        public static void Example()
        {
            BddSpec.Given("Given 1", () =>
            {
                BddSpec.BeforeEach(() => { });

                BddSpec.AfterEach(() => { });

                BddSpec.When("When 1", () =>
                {
                    BddSpec.BeforeEach(() => { });

                    BddSpec.AfterEach(() => { });

                    BddSpec.Then("Then 1", () => { });

                    BddSpec.And("And 1", () =>
                    {
                        BddSpec.BeforeEach(() => { });

                        BddSpec.AfterEach(() => { });

                        BddSpec.Then("Then 2", () => { });
                    });
                });

                BddSpec.And("And 2", () =>
                {
                    BddSpec.BeforeEach(() => { });

                    BddSpec.AfterEach(() => { });

                    BddSpec.When("When 2", () =>
                    {
                        BddSpec.BeforeEach(() => { });

                        BddSpec.AfterEach(() => { });

                        BddSpec.Then("Then 3", () => { });

                        BddSpec.And("And 3", () =>
                        {
                            BddSpec.BeforeEach(() => { });

                            BddSpec.AfterEach(() => { });

                            BddSpec.Then("Then 4", () => { });
                        });
                    });
                });
            });
        }
    }
}
