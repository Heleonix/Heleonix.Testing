// <copyright file="TestPropertiesTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Internal
{
    using System.Linq;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Interfaces;
    using global::NUnit.Framework.Internal;
    using Heleonix.Testing.NUnit.Internal;
    using Moq;

    /// <summary>
    /// Tests the <see cref="TestProperties"/>.
    /// </summary>
    public static class TestPropertiesTests
    {
        /// <summary>
        /// Tests the <see cref="TestProperties.OutputPropertyName(string)"/>.
        /// </summary>
        [Test(Description = "Should return output property name")]
        public static void OutputPropertyName()
        {
            // Arrange
            var propertyName = "Prop";

            // Act
            var propertyFullName = TestProperties.OutputPropertyName(propertyName);

            // Assert
            Assert.That(propertyFullName, Is.EqualTo($"{typeof(TestProperties).Namespace + ".Output."}{propertyName}"));
        }

        /// <summary>
        /// Tests the <see cref="TestProperties.GetOutput(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When there is one output property with two values and one custom property " +
            "Should two values of the output property")]
        public static void GetOutput()
        {
            // Arrange
            var propertyBag = new PropertyBag();

            propertyBag.Add(TestProperties.OutputPropertyName("OutputProp"), 11111);
            propertyBag.Add(TestProperties.OutputPropertyName("OutputProp"), 22222);
            propertyBag.Add("SomeOtherProp", 33333);

            // Act
            var output = TestProperties.GetOutput(propertyBag);

            // Assert
            Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output[0], Is.EqualTo(11111));
            Assert.That(output[1], Is.EqualTo(22222));
        }

        /// <summary>
        /// Tests the <see cref="TestProperties.IsOutputWritten(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When output is written in the first call of a spec method " +
            "Should return false for the second call")]
        public static void IsOutputWritten()
        {
            // Arrange
            var propertyBag = new PropertyBag();

            propertyBag.Add($"{typeof(TestProperties).Namespace}.{nameof(IsOutputWritten)}", true);

            // Act
            var isOutputWritten = TestProperties.IsOutputWritten(propertyBag);

            // Assert
            Assert.That(isOutputWritten, Is.True);
        }

        /// <summary>
        /// Tests the <see cref="TestProperties.IsOutputWritten(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When the method is called SHould set the flag to true")]
        public static void SetIsOutputWritten()
        {
            // Arrange
            var propertyBag = new PropertyBag();

            // Act
            TestProperties.SetIsOutputWritten(propertyBag);

            // Assert
            Assert.That(propertyBag[$"{typeof(TestProperties).Namespace}.{nameof(IsOutputWritten)}"], Contains.Item(true));
        }

        /// <summary>
        /// Tests the <see cref="TestProperties.GetTestHost(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When a test host exists Should return the test host instance")]
        public static void GetTestHost()
        {
            // Arrange
            var propertyBag = new PropertyBag();
            var testHostMockObject = new Mock<TestHost>().Object;

            propertyBag.Add($"{typeof(TestProperties).Namespace}.{nameof(TestHost)}", testHostMockObject);

            // Act
            var testHost = TestProperties.GetTestHost(propertyBag);

            // Assert
            Assert.That(testHost, Is.EqualTo(testHostMockObject));
        }

        /// <summary>
        /// Tests the <see cref="TestProperties.GetTestHost(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When the method is called Should set a test host instance")]
        public static void SetTestHost()
        {
            // Arrange
            var propertyBag = new PropertyBag();
            var testHostMockObject = new Mock<TestHost>().Object;

            // Act
            TestProperties.SetTestHost(propertyBag, testHostMockObject);

            // Assert
            Assert.That(
                propertyBag[$"{typeof(TestProperties).Namespace}.{nameof(TestHost)}"][0],
                Is.EqualTo(testHostMockObject));
        }
    }
}
