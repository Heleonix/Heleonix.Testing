// <copyright file="FixtureAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit
{
    using System.Collections.Generic;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Interfaces;
    using global::NUnit.Framework.Internal;
    using global::NUnit.Framework.Internal.Builders;

    /// <summary>
    /// Represents the base class for test fixture attributes.
    /// </summary>
    /// <seealso cref="BaseAttribute" />
    /// <seealso cref="IFixtureBuilder" />
    /// <seealso cref="IApplyToTest" />
    public abstract class FixtureAttribute : BaseAttribute, IFixtureBuilder, IApplyToTest
    {
        private readonly NUnitTestFixtureBuilder builder = new NUnitTestFixtureBuilder();

        /// <summary>
        /// Modifies a test as defined for the specific attribute.
        /// </summary>
        /// <param name="test">The test to modify</param>
        public virtual void ApplyToTest(Test test)
        {
            foreach (var prop in this.Properties)
            {
                test.Properties.Add(prop.Key, prop.Value);
            }
        }

        /// <summary>
        /// Build one or more TestFixtures from type provided. At least one
        /// non-null TestSuite must always be returned, since the method is
        /// generally called because the user has marked the target class as
        /// a fixture. If something prevents the fixture from being used, it
        /// will be returned nonetheless, labelled as non-runnable.
        /// </summary>
        /// <param name="typeInfo">The type info of the fixture to be used.</param>
        /// <returns>
        /// A TestSuite object or one derived from TestSuite.
        /// </returns>
        public virtual IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo) =>
            new[] { this.builder.BuildFrom(typeInfo, new TestFixtureData { TestName = this.TestName }) };
    }
}
