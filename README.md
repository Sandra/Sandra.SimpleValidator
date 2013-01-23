# Sandra.SimpleValidator

Sandra.SimpleValidator is designed to be... a simple validator, nothing more. It has no ability to inject repositories and query the database to validate stuff. It has no ability to generate JavaScript validation. 

It's purely used to validate a Model/ViewModel or DTO, or something that requires super simple validation such as Required, Minimum Length, etc.

To use simply create a validator:

```csharp
public class UserValidator : ValidateThis<User>
{
    public UserValidator()
    {
        For(x => x.UserName)
            .Ensure(new Required());

        For(x => x.Email)
            .Ensure(new Required())
            .Ensure(new Email());

        For(x => x.Locale)
            .Ensure(new Required());
    }
}
```

Then you can inject the `ValidationService` into your `Controller` or `NancyModule`

```csharp
public class HomeModule : NancyModule
{
    public HomeModule(ValidationService validate)
    {
        Get["/"] = _ =>
        {
            var user = this.Bind<User>();
            var validationResult = validate.This(user);

            if (validationResult.IsInvalid)
            {
                //Handle errors with
                //validationResult.Messages

                return "Validation has fails :(";
            }

            return "Validation was successful :)";
        };
    }
}
```

And that's it. The `ValidationService` method is virtual so you can mock and test it, and unit testing your Validators is easy too, simply create an instance of the validator and call validate on the model you want to test.

```csharp
[Fact]
public void Given_Valid_Model_Should_Return_IsValid_As_True()
{
    var validator = new TestClassValidator();
    var model = new TestClass
    {
        Name = "Hello World",
        Thing = "Thing!"
    };

    var result = validator.Validate(model);

    Assert.True(result.IsValid);
}
```