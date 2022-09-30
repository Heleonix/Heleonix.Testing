// <copyright file="FixtureAttributeTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests
{
    using System;
    using global::NUnit.Framework;
    using global::NUnit.Framework.Interfaces;
    using Heleonix.Testing.NUnit;
    using Moq;

    /// <summary>
    /// Tests the <see cref="FixtureAttribute.BuildFrom(ITypeInfo)"/>.
    /// </summary>
    public static class FixtureAttributeTests
    {
        /// <summary>
        /// Tests the <see cref="FixtureAttribute.BuildFrom(ITypeInfo)"/>.
        /// </summary>
        [global::NUnit.Framework.Test(Description = "Should throw the NotImplementedException")]
        public static void BuildFrom()
        {
            // Arrange
            var fixtureMockObject = new Mock<FixtureAttribute> { CallBase = true }.Object;

            // Act
            // Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                fixtureMockObject.BuildFrom(null);
            });
        }
    }
}