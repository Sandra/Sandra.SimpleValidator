using Sandra.SimpleValidator.Rules;
using Xunit;

namespace Sandra.SimpleValidator.Tests.Rules
{
    public class AreSameTests
    {
        [Fact]
        public void Given_Valid_Model_Should_Return_IsValid_As_True()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Password = "abc",
                PasswordConfirm = "abc"
            };

            var result = validator.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Invalid_Model_Should_Return_IsValid_As_False()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Password = "abc",
                PasswordConfirm = "afwe"
            };

            var result = validator.Validate(model);

            Assert.False(result.IsValid);
        }

        public class TestClass
        {
            public string Password { get; set; }
            public string PasswordConfirm { get; set; }
        }

        public class TestClassValidator : ValidateThis<TestClass>
        {
            public TestClassValidator()
            {
                For(x => x.Password)
                    .Ensure(new Required());

                Compare(x => x.Password)
                    .With(x => x.PasswordConfirm)
                    .Ensure(new AreSame());
            }
        }
    }
}