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
    public sealed class MemberTestAttribute : TestAttribute
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
        /// Builds a test method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="suite">The suite.</param>
        /// <returns>The test method.</returns>
        public override TestMethod BuildFrom(IMethodInfo method, Test suite)
        {
            var testMethod = base.BuildFrom(method, suite);

            TestPropertiesHelper.SetTestHost(testMethod.Properties, new AaaTestHost());

            return testMethod;
        }
    }
}
