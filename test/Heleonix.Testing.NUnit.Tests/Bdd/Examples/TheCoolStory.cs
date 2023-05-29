// <copyright file="TheCoolStory.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Bdd.Examples
{
    using global::NUnit.Framework;
    using Heleonix.Testing.NUnit.Bdd;
    using static Heleonix.Testing.NUnit.Bdd.BddSpec;

    /// <summary>
    /// Tests the TheCoolStory.
    /// </summary>
    [Story(
        Id = "111",
        Summary = "The cool story",
        AsA = "Product owner",
        IWant = "a cool story",
        SoThat = "I earn a lot of money")]
    public static class TheCoolStory
    {
        /// <summary>
        /// Tests the Scenario.
        /// </summary>
        [Scenario(Name = "Earn a lot of money in the story")]
        public static void Scenario()
        {
            Given("the precondition #1", () =>
            {
                BeforeEach(() => { });

                AfterEach(() => { });

                When("the action #1 is executed", () =>
                {
                    BeforeEach(() => { });

                    AfterEach(() => { });

                    Then("the result #1 happens", () => { });

                    And("the condition #1 is true", () =>
                    {
                        BeforeEach(() => { });

                        AfterEach(() => { });

                        Then("the result #2 happens", () => { });
                    });
                });

                And("condition #2 is true", () =>
                {
                    BeforeEach(() => { });

                    AfterEach(() => { });

                    When("the action #2 is executed", () =>
                    {
                        BeforeEach(() => { });

                        AfterEach(() => { });

                        Then("the result #3 happens", () => { });

                        And("the condition #3 is true", () =>
                        {
                            BeforeEach(() => { });

                            AfterEach(() => { });

                            Then("the result #4 happens", () => { });
                        });
                    });
                });
            });
        }
    }
}
