// <copyright file="TestAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
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
            var parameters = new TestCaseParameters { TestName = this.TestName };

            foreach (var prop in this.Properties)
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                parameters.Properties.Set(prop.Key, prop.Value);
#pragma warning restore CA1062 // Validate arguments of public methods
            }

            return this.builder.BuildTestMethod(method, suite, parameters);
        }
    }
}
