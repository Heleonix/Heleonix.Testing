// <copyright file="ComponentTestAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa;

using System;
using Heleonix.Testing.NUnit.Internal;

/// <summary>
/// Marks a test fixture as a component tests.
/// </summary>
/// <seealso cref="FixtureAttribute" />
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ComponentTestAttribute : FixtureAttribute
{
    /// <summary>
    /// Gets or sets the type of the component to be tested.
    /// </summary>
    /// <value>
    /// The type of the component to be tested.
    /// </value>
    public Type Type { get; set; }

    /// <summary>
    /// Gets the name of the test.
    /// </summary>
    /// <value>
    /// The name of the test.
    /// </value>
    protected override string TestName
    {
        get
        {
            return this.Type?.Name;
        }
    }

    /// <summary>
    /// Gets the properties.
    /// </summary>
    /// <value>
    /// The properties.
    /// </value>
    protected override IDictionary<string, object> Properties => new Dictionary<string, object>
    {
        { TestPropertiesHelper.OutputPropertyName(nameof(this.Type)), this.Type?.Name },
    };
}
