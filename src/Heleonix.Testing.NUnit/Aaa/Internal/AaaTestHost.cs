// <copyright file="AaaTestHost.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa.Internal
{
    using System.Collections.Generic;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Represents the test host for the AAA tests pattern.
    /// </summary>
    /// <seealso cref="TestHost" />
    internal class AaaTestHost : TestHost
    {
        /// <summary>
        /// Gets the spec structure rules.
        /// </summary>
        /// <value>
        /// The spec structure rules.
        /// </value>
        protected override IDictionary<SpecNodeType, SpecStructureRule> SpecStructureRules { get; } =
            new Dictionary<SpecNodeType, SpecStructureRule>
            {
                { SpecNodeType.When, new SpecStructureRule("^$", "^(Arrange)|^(Act)|^(Teardown)|^(When)|^$") },
                {
                    SpecNodeType.And, new SpecStructureRule(
                        "^(And,)*(When)",
                        "^(Arrange)|^(Act)|^(Should)|^(Teardown)|^(And)|^$")
                },
                {
                    SpecNodeType.Should, new SpecStructureRule(
                        "^(When)|^(And)",
                        "^(Arrange)|^(Act)|^(Teardown)|^$")
                },
                { SpecNodeType.Arrange, new SpecStructureRule("^(When)|^(And)|^$", "^$") },
                { SpecNodeType.Act, new SpecStructureRule("^(When)|^(And)|^$", "^(Arrange)|^$") },
                { SpecNodeType.Teardown, new SpecStructureRule("^(When)|^(And)|^$", "^(Arrange)|^(Act)|^$") },
            };
    }
}
