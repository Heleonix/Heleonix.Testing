// <copyright file="TestAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit
{
    using global::NUnit.Framework.Interfaces;
    using global::NUnit.Framework.Internal;
    using global::NUnit.Framework.Internal.Builders;

    /// <summary>
    /// Represents the base class test attributes.
    /// </summary>
    /// <seealso cref="BaseAttribute" />
    /// <seealso cref="ISimpleTestBuilder" />
    public abstract class TestAttribute : BaseAttribute, ISimpleTestBuilder
    {
        private readonly NUnitTestCaseBuilder builder = new NUnitTestCaseBuilder();

        /// <summary>
        /// Build a TestMethod from the provided MethodInfo.
        /// </summary>
        /// <param name="method">The method to be used as a test.</param>
        /// <param name="suite">The TestSuite to which the method will be added.</param>
        /// <returns>
        /// A TestMethod object.
        /// </returns>
        public virtual TestMethod BuildFrom(IMethodInfo method, Test suite)
        {
            var testMethod = this.builder.BuildTestMethod(
                method,
                suite,
                new TestCaseParameters { TestName = this.TestName });

#pragma warning disable CA1062 // Validate arguments of public methods
            foreach (var key in suite.Properties.Keys)
#pragma warning restore CA1062 // Validate arguments of public methods
            {
                foreach (var value in suite.Properties[key])
                {
                    testMethod.Properties.Add(key, value);
                }
            }

            foreach (var prop in this.Properties)
            {
                testMethod.Properties.Add(prop.Key, prop.Value);
            }

            return testMethod;
        }
    }
}
