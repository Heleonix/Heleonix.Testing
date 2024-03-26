﻿// <copyright file="TheCoolFeature.cs" company="Heleonix - Hennadii Lutsyshyn">
// Copyright (c) Heleonix - Hennadii Lutsyshyn. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the repository root for full license information.
// </copyright>

namespace Heleonix.Testing.NUnit.Tests.Bdd.Examples;

using Heleonix.Testing.NUnit.Bdd;
using static Heleonix.Testing.NUnit.Bdd.BddSpec;

/// <summary>
/// Tests the TheCoolStory.
/// </summary>
[Feature(Name = "The Cool Feature")]
public static class TheCoolFeature
{
    /// <summary>
    /// Tests the Scenario.
    /// </summary>
    [Scenario(Name = "Earn a lot of money in the feature")]
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
