// <copyright file="TestHostTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using global::NUnit.Framework;
    using Heleonix.Testing.NUnit.Bdd;
    using Heleonix.Testing.NUnit.Bdd.Internal;
    using Heleonix.Testing.NUnit.Internal;
    using Moq;
    using Moq.Protected;

    /// <summary>
    /// Tests the <see cref="TestHost" />.
    /// </summary>
    public static class TestHostTests
    {
        /// <summary>
        /// Tests the <see cref="TestHost.Current"/>.
        /// </summary>
        [Test(Description = "Should provide the Current singleton instance")]
        public static void Current()
        {
            // Arrange
            var testHostMockObject = new Mock<TestHost>().Object;

            TestContext.CurrentContext.Test.Properties.Add(
                $"{typeof(TestProperties).Namespace}.{nameof(TestHost)}", testHostMockObject);

            // Assert
            Assert.That(TestHost.Current, Is.EqualTo(testHostMockObject));
        }

        /// <summary>
        /// Tests the <see cref="TestHost.Add"/>.
        /// </summary>
        [Test(Description = "When the execution stack is empty Should add a node to the Root node")]
        public static void Add1()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                { { SpecNodeType.Arrange, new SpecStructureRule(null, null) } });

            TestContext.CurrentContext.Test.Properties.Add(
                $"{typeof(TestProperties).Namespace}.{nameof(TestHost)}", testHostMock.Object);

            var node = new SpecNode(SpecNodeType.Arrange, "description", null);

            // Act
            TestHost.Current.Add(node);

            // Assert
            Assert.That(node.Parent.Type, Is.EqualTo(SpecNodeType.Root));
        }

        /// <summary>
        /// Tests the <see cref="TestHost.Add"/>.
        /// </summary>
        [Test(Description = "When the execution stack is not empty Should add a node to the executing node")]
        public static void Add2()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                {
                    { SpecNodeType.When, new SpecStructureRule(null, null) },
                    { SpecNodeType.Arrange, new SpecStructureRule(null, null) },
                });

            var node = new SpecNode(SpecNodeType.Arrange, "description", null);
            var parentNode = new SpecNode(SpecNodeType.When, "description", () =>
            {
                TestHost.Current.Add(node);
            });

            TestContext.CurrentContext.Test.Properties.Add(
                $"{typeof(TestProperties).Namespace}.{nameof(TestHost)}", testHostMock.Object);

            // Act
            TestHost.Current.Add(parentNode);

            TestHost.Current.Execute(parentNode);

            // Assert
            Assert.That(parentNode.Parent.Type, Is.EqualTo(SpecNodeType.Root));
            Assert.That(node.Parent.Type, Is.EqualTo(SpecNodeType.When));
        }

        /// <summary>
        /// Tests the validation of adding new nodes.
        /// </summary>
        [Test(Description = "When an absent node type is passed Should fail")]
        public static void ValidateAdding1()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                {
                    { SpecNodeType.When, new SpecStructureRule(null, null) },
                });

            // Act
            var methodInfo = testHostMock.Object.GetType().BaseType.GetMethod(
                "ValidateAdding", BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            Assert.Throws<TargetInvocationException>(() =>
            {
                methodInfo.Invoke(testHostMock.Object, new[] { new SpecNode(SpecNodeType.Arrange, null, null) });
            });
        }

        /// <summary>
        /// Tests the validation of adding new nodes.
        /// </summary>
        [Test(Description = "When there are no rules Should succeed")]
        public static void ValidateAdding2()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                {
                    { SpecNodeType.When, new SpecStructureRule(null, null) },
                });

            // Act
            var methodInfo = testHostMock.Object.GetType().BaseType.GetMethod(
                "ValidateAdding", BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            Assert.DoesNotThrow(() =>
            {
                methodInfo.Invoke(testHostMock.Object, new[] { new SpecNode(SpecNodeType.When, null, null) });
            });
        }

        /// <summary>
        /// Tests the validation of adding new nodes.
        /// </summary>
        [Test(Description = "When there is a passing specExecutionStackRule Should succeed")]
        public static void ValidateAdding3()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                {
                    { SpecNodeType.When, new SpecStructureRule("^$", null) },
                });

            // Act
            var methodInfo = testHostMock.Object.GetType().BaseType.GetMethod(
                "ValidateAdding", BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            Assert.DoesNotThrow(() =>
            {
                methodInfo.Invoke(testHostMock.Object, new[] { new SpecNode(SpecNodeType.When, null, null) });
            });
        }

        /// <summary>
        /// Tests the validation of adding new nodes.
        /// </summary>
        [Test(Description = "When there is a failing specExecutionStackRule Should throw the InvalidOperationException")]
        public static void ValidateAdding4()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                {
                    { SpecNodeType.When, new SpecStructureRule("^And", null) },
                });

            // Act
            var methodInfo = testHostMock.Object.GetType().BaseType.GetMethod(
                "ValidateAdding", BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                try
                {
                    methodInfo.Invoke(testHostMock.Object, new[] { new SpecNode(SpecNodeType.When, null, null) });
                }
                catch (TargetInvocationException e)
                {
                    throw e.InnerException;
                }
            });
        }

        /// <summary>
        /// Tests the validation of adding new nodes.
        /// </summary>
        [Test(Description = "When there is a passing predecessorsRule Should succeed")]
        public static void ValidateAdding5()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                {
                    { SpecNodeType.When, new SpecStructureRule(null, "^$") },
                });

            // Act
            var methodInfo = testHostMock.Object.GetType().BaseType.GetMethod(
                "ValidateAdding", BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            Assert.DoesNotThrow(() =>
            {
                methodInfo.Invoke(testHostMock.Object, new[] { new SpecNode(SpecNodeType.When, null, null) });
            });
        }

        /// <summary>
        /// Tests the validation of adding new nodes.
        /// </summary>
        [Test(Description = "When there is a failing predecessorsRule Should throw the InvalidOperationException")]
        public static void ValidateAdding6()
        {
            // Arrange
            var testHostMock = new Mock<TestHost>();

            testHostMock.Protected().SetupGet<IDictionary<SpecNodeType, SpecStructureRule>>("SpecStructureRules")
                .Returns(new Dictionary<SpecNodeType, SpecStructureRule>
                {
                    { SpecNodeType.When, new SpecStructureRule(null, "^And") },
                });

            // Act
            var methodInfo = testHostMock.Object.GetType().BaseType.GetMethod(
                "ValidateAdding", BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                try
                {
                    methodInfo.Invoke(testHostMock.Object, new[] { new SpecNode(SpecNodeType.When, null, null) });
                }
                catch (TargetInvocationException e)
                {
                    throw e.InnerException;
                }
            });
        }

        /// <summary>
        /// Tests the <see cref="TestHost.Execute(SpecNode)"/>.
        /// </summary>
        [Test(Description = "When properties are not written to output Should write properties to the output")]
        public static void Execute()
        {
            // Arrange
            var rootNode = new SpecNode(SpecNodeType.Given, "given", () => { });
            var childNode = new SpecNode(SpecNodeType.When, "when", () => { });
            rootNode.Add(childNode);
            TestContext.CurrentContext.Test.Properties.Add("Heleonix.Testing.NUnit.Internal.Output." + nameof(StoryAttribute.AsA), "As a PO");

            TestProperties.SetTestHost(TestContext.CurrentContext.Test.Properties, new BddTestHost());

            // Act
            TestHost.Current.Execute(childNode);

            // Assert
            Assert.That(TestContext.CurrentContext.Test.Properties["Heleonix.Testing.NUnit.Internal." + nameof(TestProperties.IsOutputWritten)][0], Is.True);
        }
    }
}
