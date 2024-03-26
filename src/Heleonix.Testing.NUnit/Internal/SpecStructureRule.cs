// <copyright file="SpecStructureRule.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Internal;

/// <summary>
/// Represents a rule of a spec structure.
/// </summary>
internal class SpecStructureRule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SpecStructureRule"/> class.
    /// </summary>
    /// <param name="specExecutionStackRule">The spec execution stack rule.</param>
    /// <param name="predecessorsRule">The predecessors rule.</param>
    public SpecStructureRule(string specExecutionStackRule, string predecessorsRule)
    {
        this.SpecExecutionStackRule = specExecutionStackRule;
        this.PredecessorsRule = predecessorsRule;
    }

    /// <summary>
    /// Gets the rule for predecessors.
    /// </summary>
    /// <value>
    /// The predecessors rule.
    /// </value>
    public string PredecessorsRule { get; private set; }

    /// <summary>
    /// Gets the spec execution stack rule.
    /// </summary>
    /// <value>
    /// The spec execution stack rule.
    /// </value>
    public string SpecExecutionStackRule { get; private set; }
}
