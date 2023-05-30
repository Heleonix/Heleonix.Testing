// <copyright file="FixtureAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit
{
    using System;
    using System.Collections.Generic;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Interfaces;
    using global::NUnit.Framework.Internal;
    using global::NUnit.Framework.Internal.Builders;
    using static System.Net.Mime.MediaTypeNames;

    /// <summary>
    /// Represents the base class for test fixture attributes.
    /// </summary>
    /// <seealso cref="BaseAttribute" />
    /// <seealso cref="IFixtureBuilder" />
    /// <seealso cref="IApplyToTest" />
    public abstract class FixtureAttribute : BaseAttribute, IFixtureBuilder2
    {
        private readonly NUnitTestFixtureBuilder builder = new NUnitTestFixtureBuilder();

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
            throw new NotImplementedException("This method is replaced with the one from IFixtureBuilder2.");

        /// <summary>
        /// Builds any number of test fixtures from the specified type.
        /// </summary>
        /// <param name="typeInfo">The type info of the fixture to be used.</param>
        /// <param name="filter">PreFilter to be used to select methods.</param>
        /// <returns>A list of test suites.</returns>
        public IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo, IPreFilter filter)
        {
            var data = new TestFixtureData { TestName = this.TestName };

            foreach (var prop in this.Properties)
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                data.Properties.Set(prop.Key, prop.Value);
#pragma warning restore CA1062 // Validate arguments of public methods
            }

            var suite = this.builder.BuildFrom(typeInfo, filter, data);

            return new[] { suite };
        }
    }
}
