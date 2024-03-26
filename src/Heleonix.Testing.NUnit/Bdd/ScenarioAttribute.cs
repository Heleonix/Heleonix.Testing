// <copyright file="ScenarioAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Bdd;

using System;
using global::NUnit.Framework.Interfaces;
using global::NUnit.Framework.Internal;
using Heleonix.Testing.NUnit.Bdd.Internal;
using Heleonix.Testing.NUnit.Internal;

/// <summary>
/// Marks a test a a scenario test.
/// </summary>
/// <seealso cref="TestAttribute" />
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class ScenarioAttribute : TestAttribute, IApplyToContext
{
    /// <summary>
    /// Gets or sets the name of the scenario.
    /// </summary>
    /// <value>
    /// The name of the scenario.
    /// </value>
    public string Name { get; set; }

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
            return $"Scenario: {this.Name}";
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
        { TestPropertiesHelper.OutputPropertyName(nameof(this.TestName)), this.TestName },
    };

    /// <summary>
    /// Build a TestMethod from the provided MethodInfo.
    /// </summary>
    /// <param name="method">The method to be used as a test.</param>
    /// <param name="suite">The TestSuite to which the method will be added.</param>
    /// <returns>
    /// A TestMethod object.
    /// </returns>
    public override TestMethod BuildFrom(IMethodInfo method, Test suite)
    {
        var testMethod = base.BuildFrom(method, suite);

        TestPropertiesHelper.SetTestHost(testMethod.Properties, new BddTestHost(1));

        return testMethod;
    }

    /// <inheritdoc/>
    public void ApplyToContext(TestExecutionContext context)
    {
        var fixtureOutputProperties = TestPropertiesHelper.GetOutput(context.CurrentTest.Parent.Properties);

        foreach (var prop in fixtureOutputProperties)
        {
            context.OutWriter.WriteLine(prop);
        }

        var testOutputProperties = TestPropertiesHelper.GetOutput(context.CurrentTest.Properties);

        foreach (var prop in testOutputProperties)
        {
            context.OutWriter.WriteLine($"    {prop}");
        }
    }
}
