// <copyright file="StoryAttributeTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa;

using System.Collections.Generic;
using System.Reflection;
using global::NUnit.Framework;
using global::NUnit.Framework.Internal;
using Heleonix.Testing.NUnit.Bdd;

/// <summary>
/// Tests the <see cref="StoryAttribute"/>.
/// </summary>
public static class StoryAttributeTests
{
    /// <summary>
    /// Tests the <see cref="StoryAttribute.TestName"/>.
    /// </summary>
    [Test(Description = "When an id and summary are provided Should return the test name and corresponding properties")]
    public static void TestName()
    {
        // Arrange
        var storyTestAttribute = new StoryAttribute { Id = "ID-123", Summary = "Some summary" };

        // Act
        var testName = storyTestAttribute.GetType().GetProperty(
            nameof(TestName),
            BindingFlags.Instance | BindingFlags.NonPublic).GetValue(storyTestAttribute) as string;

        var properties = storyTestAttribute.GetType().GetProperty(
            "Properties",
            BindingFlags.Instance | BindingFlags.NonPublic).GetValue(storyTestAttribute) as IDictionary<string, object>;

        // Assert
        Assert.That(testName, Is.EqualTo("ID-123: Some summary"));
        Assert.That(properties["Heleonix.Testing.NUnit.Internal.Output.TestName"], Is.EqualTo("ID-123: Some summary"));
    }
}
