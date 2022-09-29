// <copyright file="StoryAttributeTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa
{
    using System.Collections.Generic;
    using System.Reflection;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Internal;
    using Heleonix.Testing.NUnit.Bdd;
    using Heleonix.Testing.NUnit.Tests.Common;

    /// <summary>
    /// Tests the <see cref="StoryAttribute"/>.
    /// </summary>
    public static class StoryAttributeTests
    {
        /// <summary>
        /// Tests the <see cref="StoryAttribute.TestName"/>.
        /// </summary>
        [Test(Description = "When an id and summary are provided Should return the test name")]
        public static void TestName()
        {
            // Arrange
            var componentTestAttribute = new StoryAttribute { Id = "ID-123", Summary = "Some summary" };

            // Act
            var testName = componentTestAttribute.GetType().GetProperty(
                nameof(TestName),
                BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as string;

            // Assert
            Assert.That(testName, Is.EqualTo("ID-123: Some summary"));
        }

        /// <summary>
        /// Tests the <see cref="StoryAttribute.Properties"/>.
        /// </summary>
        [Test(Description = "When an AsA, IWant, SoThat are provided Should return properties with those statements")]
        public static void Properties()
        {
            // Arrange
            var componentTestAttribute = new StoryAttribute { AsA = "PO", IWant = "Everything", SoThat = "I'm ok" };

            // Act
            var properties = componentTestAttribute.GetType().GetProperty(
                nameof(Properties),
                BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute)
                    as IDictionary<string, object>;

            // Assert
            Assert.That(properties.Count, Is.EqualTo(3));
            Assert.That(properties["Heleonix.Testing.NUnit.Internal.Output." + nameof(StoryAttribute.AsA)], Is.EqualTo("As A PO"));
            Assert.That(properties["Heleonix.Testing.NUnit.Internal.Output." + nameof(StoryAttribute.IWant)], Is.EqualTo("I Want Everything"));
            Assert.That(properties["Heleonix.Testing.NUnit.Internal.Output." + nameof(StoryAttribute.SoThat)], Is.EqualTo("So That I'm ok"));
        }

        /// <summary>
        /// Tests the <see cref="FixtureAttribute.ApplyToTest"/>.
        /// </summary>
        [Test(Description = "When the attribute has some properties Should add properties to the passedd test")]
        public static void ApplyToTest()
        {
            // Arrange
            var attribute = new StoryAttribute { AsA = "PO", IWant = "Everything", SoThat = "I'm ok" };
            var test = new DummyTest("test");

            // Act
            attribute.ApplyToTest(test);

            // Assert
            Assert.That(test.Properties.Keys.Count, Is.EqualTo(3));
            Assert.That(test.Properties["Heleonix.Testing.NUnit.Internal.Output." + nameof(StoryAttribute.AsA)][0], Is.EqualTo("As A PO"));
            Assert.That(test.Properties["Heleonix.Testing.NUnit.Internal.Output." + nameof(StoryAttribute.IWant)][0], Is.EqualTo("I Want Everything"));
            Assert.That(test.Properties["Heleonix.Testing.NUnit.Internal.Output." + nameof(StoryAttribute.SoThat)][0], Is.EqualTo("So That I'm ok"));
        }
    }
}
