// <copyright file="AaaSpecTests.cs" company="Heleonix - Hennadii Lutsyshyn">
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
    using Heleonix.Testing.NUnit.Aaa;
    using Heleonix.Testing.NUnit.Aaa.Internal;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Tests the <see cref="AaaSpec"/>.
    /// </summary>
    public static class AaaSpecTests
    {
        /// <summary>
        /// Tests the <see cref="AaaSpec.Act(Action)"/>.
        /// </summary>
        [Test(Description = "Should add the spec to the parent node")]
        public static void Act()
        {
            // Arrange
            var host = new AaaTestHost();
            Action action = () => { };
            var parentNode = new SpecNode(SpecNodeType.When, null, () =>
            {
                // Act
                AaaSpec.Act(action);
            });

            TestProperties.SetTestHost(TestContext.CurrentContext.Test.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.Act));
            Assert.That(node.Description, Is.Null);
            Assert.That(node.NestingLevel, Is.EqualTo(1));
            Assert.That(node.Parent, Is.EqualTo(parentNode));
        }

        /// <summary>
        /// Tests the <see cref="AaaSpec.And(string, Action)"/>.
        /// </summary>
        [Test(Description = "Should add and execute the spec to the parent node")]
        public static void And()
        {
            // Arrange
            var host = new AaaTestHost();
            var actionExecuted = false;
            Action action = () =>
            {
                actionExecuted = true;
            };
            var parentNode = new SpecNode(SpecNodeType.When, null, () =>
            {
                // Act
                AaaSpec.And("description", action);
            });

            TestProperties.SetTestHost(TestContext.CurrentContext.Test.Properties, host);

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
        /// Tests the <see cref="AaaSpec.Arrange(Action)"/>.
        /// </summary>
        [Test(Description = "Should add the spec to the parent node")]
        public static void Arrange()
        {
            // Arrange
            var host = new AaaTestHost();
            Action action = () => { };
            var parentNode = new SpecNode(SpecNodeType.When, null, () =>
            {
                // Act
                AaaSpec.Arrange(action);
            });

            TestProperties.SetTestHost(TestContext.CurrentContext.Test.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.Arrange));
            Assert.That(node.Description, Is.Null);
            Assert.That(node.NestingLevel, Is.EqualTo(1));
            Assert.That(node.Parent, Is.EqualTo(parentNode));
        }

        /// <summary>
        /// Tests the <see cref="AaaSpec.Should(string, Action)"/>.
        /// </summary>
        [Test(Description = "Should add and execute the spec to the parent node")]
        public static void Should()
        {
            // Arrange
            var host = new AaaTestHost();
            var actionExecuted = false;
            var executionStack = new Stack<SpecNodeType>();
            Action action = () =>
            {
                actionExecuted = true;
            };

            var parentNode = new SpecNode(SpecNodeType.When, null, () =>
            {
                // Arrange
                AaaSpec.Arrange(() => { executionStack.Push(SpecNodeType.Arrange); });

                AaaSpec.Act(() => { executionStack.Push(SpecNodeType.Act); });

                AaaSpec.Teardown(() => { executionStack.Push(SpecNodeType.Teardown); });

                AaaSpec.And(null, () =>
                {
                    AaaSpec.Arrange(() => { executionStack.Push(SpecNodeType.Arrange); });

                    AaaSpec.Act(() => { executionStack.Push(SpecNodeType.Act); });

                    AaaSpec.Teardown(() => { executionStack.Push(SpecNodeType.Teardown); });

                    // Act
                    AaaSpec.Should("description", action);
                });
            });

            TestProperties.SetTestHost(TestContext.CurrentContext.Test.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.Last().Children.Last();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(actionExecuted, Is.True);
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.Should));
            Assert.That(node.Description, Is.EqualTo("Should description"));
            Assert.That(node.NestingLevel, Is.EqualTo(2));
            Assert.That(node.Parent.Type, Is.EqualTo(SpecNodeType.And));
            Assert.That(node.Parent.Parent, Is.EqualTo(parentNode));

            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.Teardown));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.Teardown));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.Act));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.Act));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.Arrange));
            Assert.That(executionStack.Pop(), Is.EqualTo(SpecNodeType.Arrange));
        }

        /// <summary>
        /// Tests the <see cref="AaaSpec.Teardown(Action)"/>.
        /// </summary>
        [Test(Description = "Should add the spec to the parent node")]
        public static void Teardown()
        {
            // Arrange
            var host = new AaaTestHost();
            Action action = () => { };
            var parentNode = new SpecNode(SpecNodeType.When, null, () =>
            {
                // Act
                AaaSpec.Teardown(action);
            });

            TestProperties.SetTestHost(TestContext.CurrentContext.Test.Properties, host);

            host.Add(parentNode);

            // Act
            host.Execute(parentNode);

            // Assert
            var node = parentNode.Children.First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.Teardown));
            Assert.That(node.Description, Is.Null);
            Assert.That(node.NestingLevel, Is.EqualTo(1));
            Assert.That(node.Parent, Is.EqualTo(parentNode));
        }

        /// <summary>
        /// Tests the <see cref="AaaSpec.When(string, Action)"/>.
        /// </summary>
        [Test(Description = "Should add and execute the spec to the parent node")]
        public static void When()
        {
            // Arrange
            var host = new AaaTestHost();
            SpecNode rootNode = null;
            var actionExecuted = false;
            Action action = () =>
            {
                actionExecuted = true;
            };
            var siblingNode = new SpecNode(SpecNodeType.Arrange, null, () => { });

            TestProperties.SetTestHost(TestContext.CurrentContext.Test.Properties, host);

            host.Add(siblingNode);

            rootNode = siblingNode.Parent;

            // Act
            AaaSpec.When("description", action);

            // Assert
            var node = rootNode.Children.Skip(1).First();

            Assert.That(node.Action, Is.EqualTo(action));
            Assert.That(actionExecuted, Is.True);
            Assert.That(node.Type, Is.EqualTo(SpecNodeType.When));
            Assert.That(node.Description, Is.EqualTo("When description"));
            Assert.That(node.NestingLevel, Is.EqualTo(0));
            Assert.That(node.Parent, Is.EqualTo(rootNode));
        }
    }
}
