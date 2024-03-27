# Heleonix.Testing

[![Release: .NET / NuGet](https://github.com/Heleonix/Heleonix.Testing/actions/workflows/release-net-nuget.yml/badge.svg)](https://github.com/Heleonix/Heleonix.Testing/actions/workflows/release-net-nuget.yml)

The library for writing tests in BDD and AAA styles

## Install
https://www.nuget.org/packages/Heleonix.Testing.NUnit

## AAA: Arrange Act Assert

### Structure
```csharp
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
                Assert.Fail();
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
```

### Tests Output

![AAA](https://raw.githubusercontent.com/Heleonix/docs/master/Heleonix.Testing/images/AAA.png)

## BDD: Behavior Driven Development

### Structure
```csharp
using global::NUnit.Framework;
using Heleonix.Testing.NUnit.Bdd;
using static Heleonix.Testing.NUnit.Bdd.BddSpec;

/// <summary>
/// Tests the TheCoolStory.
/// </summary>
[Feature(Name = "The Cool Feature")]
OR
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

                    Then("the result #2 happens", () => { Assert.Fail(); });
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
```

### Tests Output

![BDD](https://raw.githubusercontent.com/Heleonix/docs/master/Heleonix.Testing/images/BDD.png)

## Contribution Guideline

1. [Create a fork](https://github.com/Heleonix/Heleonix.Testing/fork) from the main repository
2. Implement whatever is needed
3. [Create a Pull Request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/creating-a-pull-request-from-a-fork).
   Make sure the assigned [Checks](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/collaborating-on-repositories-with-code-quality-features/about-status-checks#checks) pass successfully.
   You can watch the progress in the [PR: .NET](https://github.com/Heleonix/Heleonix.Testing/actions/workflows/pr-net.yml) GitHub workflows
4. [Request review](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/requesting-a-pull-request-review) from the code owner
5. Once approved, merge your Pull Request via [Squash and merge](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/incorporating-changes-from-a-pull-request/about-pull-request-merges#squash-and-merge-your-commits)

   > **IMPORTANT**  
   > While merging, enter a [Conventional Commits](https://www.conventionalcommits.org/) commit message.
   > This commit message will be used in automatically generated [Github Release Notes](https://github.com/Heleonix/Heleonix.Testing/releases)
   > and [NuGet Release Notes](https://www.nuget.org/packages/Heleonix.Testing/#releasenotes-body-tab)

6. Monitor the [Release: .NET / NuGet](https://github.com/Heleonix/Heleonix.Testing/actions/workflows/release-net-nuget.yml)
   GitHub workflow to make sure your changes are delivered successfully
7. In case of any issues, please contact [heleonix.sln@gmail.com](mailto:heleonix.sln@gmail.com)
