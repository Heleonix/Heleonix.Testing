// <copyright file="StoryAttribute.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Bdd
{
    using System;
    using System.Collections.Generic;
    using Heleonix.Testing.NUnit.Internal;

    /// <summary>
    /// Marks test fixture as a story tests.
    /// </summary>
    /// <seealso cref="FixtureAttribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class StoryAttribute : FixtureAttribute
    {
        /// <summary>
        /// Gets or sets an 'As a...' description.
        /// </summary>
        /// <value>
        /// The 'As a...' description.
        /// </value>
        public string AsA { get; set; }

        /// <summary>
        /// Gets or sets an id of the story.
        /// </summary>
        /// <value>
        /// The id of the story.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets an 'I want...' description.
        /// </summary>
        /// <value>
        /// The 'I want..' description.
        /// </value>
        public string IWant { get; set; }

        /// <summary>
        /// Gets or sets an 'So that...' description.
        /// </summary>
        /// <value>
        /// The 'So that...' description.
        /// </value>
        public string SoThat { get; set; }

        /// <summary>
        /// Gets or sets a summary of a story.
        /// </summary>
        /// <value>
        /// A summary of a story.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        protected override IDictionary<string, object> Properties => new Dictionary<string, object>
        {
            { TestPropertiesHelper.OutputPropertyName(nameof(this.TestName)), $"{this.TestName}" },
        };

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
                return $"{this.Id}: {this.Summary}";
            }
        }
    }
}
