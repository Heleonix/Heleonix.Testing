// <copyright file="MyComponentTests.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Aaa.Examples
{
    using global::NUnit.Framework;
    using Heleonix.Testing.NUnit.Aaa;
    using static Heleonix.Testing.NUnit.Aaa.AaaSpec;

    /// <summary>
    /// Tests the <see cref="MyComponent"/>.
    /// </summary>
    [ComponentTest(Type = typeof(MyComponent))]
    public static class MyComponentTests
    {
        /// <summary>
        /// Tests the <see cref="MyComponent.Member1"/>.
        /// </summary>
        [MemberTest(Name = nameof(MyComponent.Member1))]
        public static void Member1()
        {
            Arrange(() =>
            {
            });

            Act(() =>
            {
            });

            Teardown(() =>
            {
            });

            When("the condition #1 is true", () =>
            {
                Arrange(() =>
                {
                });

                Act(() =>
                {
                });

                Teardown(() =>
                {
                });

                Should("lead to the result #1", () =>
                {
                });
            });

            When("the condition #2 is true", () =>
            {
                Arrange(() =>
                {
                });

                Act(() =>
                {
                });

                Teardown(() =>
                {
                });

                Should("lead to the result #2", () =>
                {
                });
            });
        }

        /// <summary>
        /// Tests the <see cref="MyComponent.Member2"/>.
        /// </summary>
        [MemberTest(Name = nameof(MyComponent.Member2))]
        public static void Member2()
        {
            Arrange(() =>
            {
            });

            Act(() =>
            {
            });

            Teardown(() =>
            {
            });

            When("the action #1 is executed", () =>
            {
                Arrange(() =>
                {
                });

                Act(() =>
                {
                });

                Teardown(() =>
                {
                });

                Should("lead to the result #1", () =>
                {
                });

                And("the condition #1 is true", () =>
                {
                    Arrange(() =>
                    {
                    });

                    Act(() =>
                    {
                    });

                    Teardown(() =>
                    {
                    });

                    Should("lead to the result #2", () =>
                    {
                    });
                });
            });
        }
    }
}
