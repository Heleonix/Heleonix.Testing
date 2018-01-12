// <copyright file="SpecNodeType.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) 2018-present Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Internal
{
    /// <summary>
    /// Provides types of spec nodes for tests patterns.
    /// </summary>
    internal enum SpecNodeType
    {
        /// <summary>
        /// The root node.
        /// </summary>
        Root,

        /// <summary>
        /// The Given.
        /// </summary>
        Given,

        /// <summary>
        /// The When.
        /// </summary>
        When,

        /// <summary>
        /// The Should.
        /// </summary>
        Should,

        /// <summary>
        /// The And.
        /// </summary>
        And,

        /// <summary>
        /// The Then.
        /// </summary>
        Then,

        /// <summary>
        /// The SetupEach.
        /// </summary>
        SetupEach,

        /// <summary>
        /// The BeforeEach.
        /// </summary>
        BeforeEach,

        /// <summary>
        /// The CleanupEach.
        /// </summary>
        CleanupEach,

        /// <summary>
        /// The AfterEach.
        /// </summary>
        AfterEach,

        /// <summary>
        /// The Arrange.
        /// </summary>
        Arrange,

        /// <summary>
        /// The Act.
        /// </summary>
        Act,

        /// <summary>
        /// The Teardown.
        /// </summary>
        Teardown
    }
}
