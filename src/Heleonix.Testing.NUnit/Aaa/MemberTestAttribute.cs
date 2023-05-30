// <copyright file="MemberTestAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Aaa
{
    using System;
    using global::NUnit.Framework.Interfaces;
    using global::NUnit.Framework.Internal;
    using Heleonix.Testing.NUnit.Aaa.Internal;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Represents the attribute for testing of compononts' members, like methods, constructors, properties.
    /// </summary>
    /// <seealso cref="TestAttribute" />
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class MemberTestAttribute : TestAttribute, IApplyToContext
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the test name.
        /// </summary>
        /// <value>
        /// The test name.
        /// </value>
        protected override string TestName => this.Name;

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        protected override IDictionary<string, object> Properties => new Dictionary<string, object>
        {
            { TestPropertiesHelper.OutputPropertyName(nameof(this.Name)), this.Name },
        };

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

        /// <summary>
        /// Builds a test method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="suite">The suite.</param>
        /// <returns>The test method.</returns>
        public override TestMethod BuildFrom(IMethodInfo method, Test suite)
        {
            var testMethod = base.BuildFrom(method, suite);

            TestPropertiesHelper.SetTestHost(testMethod.Properties, new AaaTestHost(1));

            return testMethod;
        }
    }
}
