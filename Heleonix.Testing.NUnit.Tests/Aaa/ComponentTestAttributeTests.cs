// <copyright file="ComponentTestAttributeTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Internal;
    using Heleonix.Testing.NUnit.Aaa;
    using Heleonix.Testing.NUnit.Aaa.Internal;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Tests the <see cref="ComponentTestAttribute"/>.
    /// </summary>
    public static class ComponentTestAttributeTests
    {
        /// <summary>
        /// Tests the <see cref="ComponentTestAttribute.TestName"/>.
        /// </summary>
        [Test(Description = "When a component type is provided Should return the test name")]
        public static void TestName1()
        {
            // Arrange
            var componentTestAttribute = new ComponentTestAttribute { Type = typeof(int) };

            // Act
            var testName = componentTestAttribute.GetType().GetProperty(
                "TestName",
                BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as string;

            // Assert
            Assert.That(testName, Is.EqualTo("Int32"));
        }

        /// <summary>
        /// Tests the <see cref="ComponentTestAttribute.TestName"/>.
        /// </summary>
        [Test(Description = "When a component type is not provided Should return null")]
        public static void TestName2()
        {
            // Arrange
            var componentTestAttribute = new ComponentTestAttribute();

            // Act
            var testName = componentTestAttribute.GetType().GetProperty(
                "TestName",
                BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as string;

            // Assert
            Assert.That(testName, Is.Null);
        }
    }
}
