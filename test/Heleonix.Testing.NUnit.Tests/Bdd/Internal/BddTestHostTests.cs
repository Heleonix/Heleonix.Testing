// <copyright file="BddTestHostTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Bdd.Internal;

using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using global::NUnit.Framework;
using Heleonix.Testing.NUnit.Bdd.Internal;
using Heleonix.Testing.NUnit.Internal;

/// <summary>
/// Tests the <see cref="BddTestHost"/>.
/// </summary>
public static class BddTestHostTests
{
    /// <summary>
    /// Tests the <see cref="BddTestHost.SpecStructureRules"/>.
    /// </summary>
    [Test(Description = "Should return the BDD spec structure rules")]
    public static void SpecStructureRules()
    {
        // Arrange
        var host = new BddTestHost(0);

        // Act
        var rules = typeof(BddTestHost).InvokeMember(
            nameof(SpecStructureRules),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty,
            null,
            host,
            null,
            CultureInfo.InvariantCulture)
            as IDictionary<SpecNodeType, SpecStructureRule>;

        // Assert
        Assert.That(rules[SpecNodeType.Given].SpecExecutionStackRule, Is.EqualTo("^$"));
        Assert.That(rules[SpecNodeType.Given].PredecessorsRule, Is.EqualTo("^$"));

        Assert.That(rules[SpecNodeType.When].SpecExecutionStackRule, Is.EqualTo("^(Given)|^(And)"));
        Assert.That(rules[SpecNodeType.When].PredecessorsRule, Is.EqualTo("^(BeforeEach)|^(AfterEach)|^(Then)|^(And)|^$"));

        Assert.That(rules[SpecNodeType.Then].SpecExecutionStackRule, Is.EqualTo("^(When)|^(And)"));
        Assert.That(rules[SpecNodeType.Then].PredecessorsRule, Is.EqualTo("^(BeforeEach)|^(AfterEach)|^$"));

        Assert.That(rules[SpecNodeType.And].SpecExecutionStackRule, Is.EqualTo("^(And,)*(Given)|(When)"));
        Assert.That(rules[SpecNodeType.And].PredecessorsRule, Is.EqualTo("^(BeforeEach)|^(AfterEach)|^(When)|^(Then)|^(And)|^$"));

        Assert.That(rules[SpecNodeType.BeforeEach].SpecExecutionStackRule, Is.EqualTo("^(Given)|^(When)|^(And)"));
        Assert.That(rules[SpecNodeType.BeforeEach].PredecessorsRule, Is.EqualTo("^$"));

        Assert.That(rules[SpecNodeType.AfterEach].SpecExecutionStackRule, Is.EqualTo("^(Given)|^(When)|^(And)"));
        Assert.That(rules[SpecNodeType.AfterEach].PredecessorsRule, Is.EqualTo("^(BeforeEach)|^$"));
    }
}
