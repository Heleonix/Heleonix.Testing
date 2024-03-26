// <copyright file="ComponentTestAttributeTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa;

using System.Collections.Generic;
using System.Reflection;
using global::NUnit.Framework;
using global::NUnit.Framework.Internal;

/// <summary>
/// Tests the <see cref="ComponentTestAttribute"/>.
/// </summary>
public static class ComponentTestAttributeTests
{
    /// <summary>
    /// Tests the <see cref="ComponentTestAttribute.TestName"/>.
    /// </summary>
    [Test(Description = "When a component type is provided Should return the test name and corresponding properties")]
    public static void TestName1()
    {
        // Arrange
        var componentTestAttribute = new ComponentTestAttribute { Type = typeof(int) };

        // Act
        var testName = componentTestAttribute.GetType().GetProperty(
            "TestName",
            BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as string;

        var properties = componentTestAttribute.GetType().GetProperty(
            "Properties",
            BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as IDictionary<string, object>;

        // Assert
        Assert.That(testName, Is.EqualTo("Int32"));
        Assert.That(properties["Heleonix.Testing.NUnit.Internal.Output.Type"], Is.EqualTo("Int32"));
    }

    /// <summary>
    /// Tests the <see cref="ComponentTestAttribute.TestName"/>.
    /// </summary>
    [Test(Description = "When a component type is not provided Should return null and corresponding properties")]
    public static void TestName2()
    {
        // Arrange
        var componentTestAttribute = new ComponentTestAttribute();

        // Act
        var testName = componentTestAttribute.GetType().GetProperty(
            "TestName",
            BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as string;

        var properties = componentTestAttribute.GetType().GetProperty(
           "Properties",
           BindingFlags.Instance | BindingFlags.NonPublic).GetValue(componentTestAttribute) as IDictionary<string, object>;

        // Assert
        Assert.That(testName, Is.Null);
        Assert.That(properties["Heleonix.Testing.NUnit.Internal.Output.Type"], Is.Null);
    }
}
