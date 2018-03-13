// <copyright file="AaaTestHostTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Aaa.Internal
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using global::NUnit.Framework;
    using Heleonix.Testing.NUnit.Aaa.Internal;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Tests the <see cref="AaaTestHost"/>
    /// </summary>
    public static class AaaTestHostTests
    {
        /// <summary>
        /// Tests the <see cref="AaaTestHost.SpecStructureRules"/>.
        /// </summary>
        [Test(Description = "Should return the AAA spec structure rules")]
        public static void SpecStructureRules()
        {
            // Arrange
            var host = new AaaTestHost();

            // Act
            var rules = typeof(AaaTestHost).InvokeMember(
                nameof(SpecStructureRules),
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty,
                null,
                host,
                null,
                CultureInfo.InvariantCulture)
                as IDictionary<SpecNodeType, SpecStructureRule>;

            // Assert
            Assert.That(rules[SpecNodeType.When].SpecExecutionStackRule, Is.EqualTo("^$"));
            Assert.That(rules[SpecNodeType.When].PredecessorsRule, Is.EqualTo("^(Arrange)|^(Act)|^(Teardown)|^$"));

            Assert.That(rules[SpecNodeType.And].SpecExecutionStackRule, Is.EqualTo("^(And,)*(When)"));
            Assert.That(rules[SpecNodeType.And].PredecessorsRule, Is.EqualTo("^(Arrange)|^(Act)|^(Should)|^(Teardown)|^(And)|^$"));

            Assert.That(rules[SpecNodeType.Should].SpecExecutionStackRule, Is.EqualTo("^(When)|^(And)"));
            Assert.That(rules[SpecNodeType.Should].PredecessorsRule, Is.EqualTo("^(Arrange)|^(Act)|^(Teardown)|^$"));

            Assert.That(rules[SpecNodeType.Arrange].SpecExecutionStackRule, Is.EqualTo("^(When)|^(And)|^$"));
            Assert.That(rules[SpecNodeType.Arrange].PredecessorsRule, Is.EqualTo("^$"));

            Assert.That(rules[SpecNodeType.Act].SpecExecutionStackRule, Is.EqualTo("^(When)|^(And)|^$"));
            Assert.That(rules[SpecNodeType.Act].PredecessorsRule, Is.EqualTo("^(Arrange)|^$"));

            Assert.That(rules[SpecNodeType.Teardown].SpecExecutionStackRule, Is.EqualTo("^(When)|^(And)|^$"));
            Assert.That(rules[SpecNodeType.Teardown].PredecessorsRule, Is.EqualTo("^(Arrange)|^(Act)|^$"));
        }
    }
}
