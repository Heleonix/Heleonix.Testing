// <copyright file="BddTestHost.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Bdd.Internal
{
    using System.Collections.Generic;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Represents the test host for the BDD tests pattern.
    /// </summary>
    /// <seealso cref="TestHost" />
    internal class BddTestHost : TestHost
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
                { SpecNodeType.Given, new SpecStructureRule("^$", "^$") },
                {
                    SpecNodeType.When, new SpecStructureRule(
                        "^(Given)|^(And)",
                        "^(BeforeEach)|^(AfterEach)|^(Then)|^(And)|^$")
                },
                {
                    SpecNodeType.Then, new SpecStructureRule(
                        "^(When)|^(And)",
                        "^(BeforeEach)|^(AfterEach)|^$")
                },
                {
                    SpecNodeType.And, new SpecStructureRule(
                        "^(And,)*(Given)|(When)",
                        "^(BeforeEach)|^(AfterEach)|^(When)|^(Then)|^(And)|^$")
                },
                { SpecNodeType.BeforeEach, new SpecStructureRule("^(Given)|^(When)|^(And)", "^$") },
                { SpecNodeType.AfterEach, new SpecStructureRule("^(Given)|^(When)|^(And)", "^(BeforeEach)|^$") },
            };
    }
}
