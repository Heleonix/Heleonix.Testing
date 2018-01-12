// <copyright file="FeatureAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Bdd
{
    using System;

    /// <summary>
    /// Marks a test fixture as a feature tests.
    /// </summary>
    /// <seealso cref="FixtureAttribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class FeatureAttribute : FixtureAttribute
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
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
                return $"Feature: {this.Name}";
            }
        }
    }
}
