using Sandra.SimpleValidator;
using Sandra.SimpleValidator.Rules;
using Xunit;

namespace Sandra.SimpleValidation.Tests.Rules
{
    public class RequiredTests
    {
        [Fact]
        public void Given_Valid_Model_Should_Return_IsValid_As_True()
        {
            var validate = new ValidationService();
            var model = new TestClass
            {
                Name = "Hello World",
                Thing = "Thing!"
            };

            var result = validate.This(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_InValid_Model_Should_Return_IsValid_As_False()
        {
            var validate = new ValidationService();
            var model = new TestClass
            {
                Name = string.Empty,
                Thing = "Thing!"
            };

            var result = validate.This(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_InValid_Model_Should_Return_Custom_Message()
        {
            var validate = new ValidationService();
            var model = new TestClass
            {
                Name = "Hello World",
                Thing = string.Empty // Configured with custom message
            };

            var result = validate.This(model);

            Assert.Equal("Thing is required!", result.Messages[0].Message);
        }

        [Fact]
        public void Given_InValid_Model_Should_Return_Default_Message()
        {
            var validate = new ValidationService();
            var model = new TestClass
            {
                Name = string.Empty, // Configured with Default message
                Thing = "Thing!"
            };

            var result = validate.This(model);

            Assert.Equal("Field is required", result.Messages[0].Message);
        }

        public class TestClass
        {
            public string Name { get; set; }
            public string Thing { get; set; }
        }

        public class TestClassValidator : ValidateBase<TestClass>
        {
            public TestClassValidator()
            {
                For(x => x.Name)
                    .Ensure(new Required());

                For(x => x.Thing)
                    .Ensure(new Required().WithMessage("Thing is required!"));
            }
        }
    }
}
