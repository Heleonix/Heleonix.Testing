// <copyright file="TestPropertiesHelperTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
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
    /// Tests the <see cref="TestPropertiesHelper"/>.
    /// </summary>
    public static class TestPropertiesHelperTests
    {
        /// <summary>
        /// Tests the <see cref="TestPropertiesHelper.OutputPropertyName(string)"/>.
        /// </summary>
        [Test(Description = "Should return output property name")]
        public static void OutputPropertyName()
        {
            // Arrange
            var propertyName = "Prop";

            // Act
            var propertyFullName = TestPropertiesHelper.OutputPropertyName(propertyName);

            // Assert
            Assert.That(propertyFullName, Is.EqualTo($"{typeof(TestPropertiesHelper).Namespace + ".Output."}{propertyName}"));
        }

        /// <summary>
        /// Tests the <see cref="TestPropertiesHelper.GetOutput(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When there is one output property with two values and one custom property " +
            "Should two values of the output property")]
        public static void GetOutput()
        {
            // Arrange
            var propertyBag = new PropertyBag();

            propertyBag.Add(TestPropertiesHelper.OutputPropertyName("OutputProp"), 11111);
            propertyBag.Add(TestPropertiesHelper.OutputPropertyName("OutputProp"), 22222);
            propertyBag.Add("SomeOtherProp", 33333);

            // Act
            var output = TestPropertiesHelper.GetOutput(propertyBag);

            // Assert
            Assert.That(output.Length, Is.EqualTo(2));
            Assert.That(output[0], Is.EqualTo(11111));
            Assert.That(output[1], Is.EqualTo(22222));
        }

        /// <summary>
        /// Tests the <see cref="TestPropertiesHelper.GetTestHost(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When a test host exists Should return the test host instance")]
        public static void GetTestHost()
        {
            // Arrange
            var propertyBag = new PropertyBag();
            var testHostMockObject = new Mock<TestHost>(new object[] { 0 }).Object;

            propertyBag.Add($"{typeof(TestPropertiesHelper).Namespace}.{nameof(TestHost)}", testHostMockObject);

            // Act
            var testHost = TestPropertiesHelper.GetTestHost(propertyBag);

            // Assert
            Assert.That(testHost, Is.EqualTo(testHostMockObject));
        }

        /// <summary>
        /// Tests the <see cref="TestPropertiesHelper.GetTestHost(IPropertyBag)"/>.
        /// </summary>
        [Test(Description = "When the method is called Should set a test host instance")]
        public static void SetTestHost()
        {
            // Arrange
            var propertyBag = new PropertyBag();
            var testHostMockObject = new Mock<TestHost>(new object[] { 0 }).Object;

            // Act
            TestPropertiesHelper.SetTestHost(propertyBag, testHostMockObject);

            // Assert
            Assert.That(
                propertyBag[$"{typeof(TestPropertiesHelper).Namespace}.{nameof(TestHost)}"][0],
                Is.EqualTo(testHostMockObject));
        }
    }
}
