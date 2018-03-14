# Heleonix.Testing
The library for writing tests in BDD and AAA style

## Install
https://www.nuget.org/packages/Heleonix.Testing.NUnit

## AAA: Arrange Act Assert

#### Structure
```csharp
using Heleonix.Testing.NUnit.Aaa;
using static Heleonix.Testing.NUnit.Aaa.AaaSpec;

[ComponentTest(Type = typeof(Component))]
public static class ComponentTests
{
    [MemberTest(Name = nameof(Component.Member))]
    public static void Member()
    {
        Arrange(() => { });

        Act(() => { });

        Teardown(() => { });

        When("the action #1 is executed", () =>
        {
            Arrange(() => { });

            Act(() => { });

            Teardown(() => { });

            Should("lead to the result #1", () => { });

            And("the condition #1 is true", () =>
            {
                Arrange(() => { });

                Act(() => { });

                Teardown(() => { });

                Should("lead to the result #2", () => { });
            });
        });
    }
}
```

#### Tests Output
![AAA](https://github.com/Heleonix/heleonix.github.io/blob/master/images/AAA.png)

## BDD: Behavior Driven Development

#### Structure
```csharp
using Heleonix.Testing.NUnit.Bdd;
using static Heleonix.Testing.NUnit.Bdd.BddSpec;

[Feature(Name = "Feature")]
OR
[Scenario(Name = "Some scenarion")]
OR
[Story(
    Id = "111",
    Summary = "The cool story",
    AsA = "Product owner",
    IWant = "a cool story",
    SoThat = "I earn a lot of money")]
public static class TheCoolStory
{
    [Scenario(Name = "Earn a lot of money")]
    public static void Scenario()
    {
        Given("the precondition #1", () =>
        {
            SetupEach(() => { });

            BeforeEach(() => { });

            AfterEach(() => { });

            CleanupEach(() => { });

            When("the action #1 is executed", () =>
            {
                SetupEach(() => { });

                BeforeEach(() => { });

                AfterEach(() => { });

                CleanupEach(() => { });

                Then("the result #1 happens", () => { });

                And("the condition #1 is true", () =>
                {
                    SetupEach(() => { });

                    BeforeEach(() => { });

                    AfterEach(() => { });

                    CleanupEach(() => { });

                    Then("the result #2 happens", () => { });
                });
            });

            And("condition #2 is true", () =>
            {
                SetupEach(() => { });

                BeforeEach(() => { });

                AfterEach(() => { });

                CleanupEach(() => { });

                When("the action #2 is executed", () =>
                {
                    SetupEach(() => { });

                    BeforeEach(() => { });

                    AfterEach(() => { });

                    CleanupEach(() => { });

                    Then("the result #3 happens", () => { });

                    And("the condition #3 is true", () =>
                    {
                        SetupEach(() => { });

                        BeforeEach(() => { });

                        AfterEach(() => { });

                        CleanupEach(() => { });

                        Then("the result #4 happens", () => { });
                    });
                });
            });
        });
    }
}
```

#### Tests Output
![BDD](https://github.com/Heleonix/heleonix.github.io/blob/master/images/BDD.png)
