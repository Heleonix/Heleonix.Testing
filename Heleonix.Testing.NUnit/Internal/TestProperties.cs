// <copyright file="TestProperties.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Internal
{
    using System;
    using System.Linq;
    using global::NUnit.Framework.Interfaces;

    /// <summary>
    /// Represents the properties of the test and methods to work with the <see cref="IPropertyBag"/>.
    /// </summary>
    internal static class TestProperties
    {
        private static readonly string Prefix = typeof(TestProperties).Namespace + ".";

        private static readonly string OutputPrefix = Prefix + "Output.";

        private static readonly string TestHostName = PropertyName(nameof(TestHost));

        private static readonly string IsOutputWrittenName = PropertyName(nameof(IsOutputWritten));

        /// <summary>
        /// Builds a name of an output property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A name of an output property.</returns>
        public static string OutputPropertyName(string name) => OutputPrefix + name;

        /// <summary>
        /// Gets the properties to be written to the output.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>The properties to be written to the output.</returns>
        public static object[] GetOutput(IPropertyBag properties) =>
            properties.Keys.Where(key => key.StartsWith(OutputPrefix, StringComparison.Ordinal))
                .SelectMany(key => properties[key].OfType<object>()).ToArray();

        /// <summary>
        /// Determines whether output is written.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>
        ///   <c>true</c> if output has been written; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOutputWritten(IPropertyBag properties) => properties[IsOutputWrittenName].Contains(true);

        /// <summary>
        /// Sets <c>true</c> if output has been written; otherwise <c>false</c>.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public static void SetIsOutputWritten(IPropertyBag properties) => properties.Set(IsOutputWrittenName, true);

        /// <summary>
        /// Gets the test host.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>The test host.</returns>
        public static TestHost GetTestHost(IPropertyBag properties) => properties[TestHostName][0] as TestHost;

        /// <summary>
        /// Sets the test host.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="host">The host.</param>
        public static void SetTestHost(IPropertyBag properties, TestHost host) => properties.Set(TestHostName, host);

        private static string PropertyName(string name) => Prefix + name;
    }
}
