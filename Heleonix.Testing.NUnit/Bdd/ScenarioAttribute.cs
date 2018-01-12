// <copyright file="ScenarioAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Bdd
{
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
    public sealed class ScenarioAttribute : TestAttribute
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
        /// Build a TestMethod from the provided MethodInfo.
        /// </summary>
        /// <param name="method">The method to be used as a test</param>
        /// <param name="suite">The TestSuite to which the method will be added</param>
        /// <returns>
        /// A TestMethod object
        /// </returns>
        public override TestMethod BuildFrom(IMethodInfo method, Test suite)
        {
            var testMethod = base.BuildFrom(method, suite);

            TestProperties.SetTestHost(testMethod.Properties, new BddTestHost());

            return testMethod;
        }
    }
}
