// <copyright file="FeatureAttributeTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa
{
    using System.Reflection;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Internal;
    using Heleonix.Testing.NUnit.Bdd;

    /// <summary>
    /// Tests the <see cref="FeatureAttribute"/>.
    /// </summary>
    public static class FeatureAttributeTests
    {
        /// <summary>
        /// Tests the <see cref="FeatureAttribute.TestName"/>.
        /// </summary>
        [Test(Description = "When a name is provided Should return the test name")]
        public static void TestName()
        {
            // Arrange
            var componentTestAttribute = new FeatureAttribute { Name = "FeatureName" };

            // Act
            var testName = componentTestAttribute.GetType().GetProperty(
                nameof(TestName),
                BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as string;

            // Assert
            Assert.That(testName, Is.EqualTo("Feature: FeatureName"));
        }
    }
}
