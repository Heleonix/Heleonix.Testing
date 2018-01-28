# Heleonix.Testing
The library for writing tests in BDD and AAA style

## BDD: Behavior Driven Development
```csharp
using static Heleonix.Testing.NUnit.Aaa.AaaSpec;

[ComponentTest(Type = typeof(Component))]
public static class ComponentTests
{
    [MemberTest(Name = nameof(Member))]
    public static void Member()
    {
        Arrange(() => { });

        Act(() => { });

        Teardown(() => { });

        When("When 1", () =>
        {
            Arrange(() => { });

            Act(() => { });

            Teardown(() => { });

            Should("Should 1", () => { });

            And("And 1", () =>
            {
                Arrange(() => { });

                Act(() => { });

                Teardown(() => { });

                Should("Should 2", () => { });
            });
        });
    }
}
```

## AAA: Arrange Act Assert
```csharp
using static Heleonix.Testing.NUnit.Bdd.BddSpec;

[Feature(Name = "Feature")]
OR
[Story(
    Id = "111",
    Summary = "The cool story",
    AsA = "Product owner",
    IWant = "a cool story",
    SoThat = "I earn a lot of money")]
public static class ExampleTests
{
    [Scenario(Name = nameof(Example))]
    public static void Example()
    {
        Given("Given 1", () =>
        {
            SetupEach(() => { });

            BeforeEach(() => { });

            AfterEach(() => { });

            CleanupEach(() => { });

            When("When 1", () =>
            {
                SetupEach(() => { });

                BeforeEach(() => { });

                AfterEach(() => { });

                CleanupEach(() => { });

                Then("Then 1", () => { });

                And("And 1", () =>
                {
                    SetupEach(() => { });

                    BeforeEach(() => { });

                    AfterEach(() => { });

                    CleanupEach(() => { });

                    Then("Then 2", () => { });
                });
            });

            And("And 2", () =>
            {
                SetupEach(() => { });

                BeforeEach(() => { });

                AfterEach(() => { });

                CleanupEach(() => { });

                When("When 2", () =>
                {
                    SetupEach(() => { });

                    BeforeEach(() => { });

                    AfterEach(() => { });

                    CleanupEach(() => { });

                    Then("Then 3", () => { });

                    And("And 3", () =>
                    {
                        SetupEach(() => { });

                        BeforeEach(() => { });

                        AfterEach(() => { });

                        CleanupEach(() => { });

                        Then("Then 4", () => { });
                    });
                });
            });
        });
    }
}
```
