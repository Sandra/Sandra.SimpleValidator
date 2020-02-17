using Sandra.SimpleValidator.Rules;
using Xunit;

namespace Sandra.SimpleValidator.Tests.Rules
{
    public class MaximumLengthTests
    {
        [Fact]
        public void Given_Valid_Model_Should_Return_IsValid_As_True()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Name = "0123456789",
                Thing = "0123456789"
            };

            var result = validator.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Valid_Model_With_Null_String_Should_Return_IsValid_As_True()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Name = null,
                Thing = null
            };

            var result = validator.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_InValid_Model_Should_Return_IsValid_As_False()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Name = "0123456789!!!!!",
                Thing = "01234"
            };

            var result = validator.Validate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_InValid_Model_Should_Return_Custom_Message()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Name = "0123456789",
                Thing = "0123456789!!!!!!!!" // Configured with custom message
            };

            var result = validator.Validate(model);

            Assert.Equal("Should be length of 10!", result.Messages[0].Message);
        }

        [Fact]
        public void Given_InValid_Model_Should_Return_Default_Message()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Name = "0123456789!!!!!!!!!!!", // Configured with Default message
                Thing = "0123456789"
            };

            var result = validator.Validate(model);

            Assert.Equal("Field needs to be Maximum length of 10", result.Messages[0].Message);
        }

        public class TestClass
        {
            public string Name { get; set; }
            public string Thing { get; set; }
        }

        public class TestClassValidator : ValidateThis<TestClass>
        {
            public TestClassValidator()
            {
                For(x => x.Name)
                    .Ensure(new MaximumLength(10));

                For(x => x.Thing)
                    .Ensure(new MaximumLength(10).WithMessage("Should be length of 10!"));
            }
        }
    }
}