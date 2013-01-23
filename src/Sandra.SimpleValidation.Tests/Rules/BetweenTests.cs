using Sandra.SimpleValidator;
using Sandra.SimpleValidator.Rules;
using Xunit;

namespace Sandra.SimpleValidation.Tests.Rules
{
    public class BetweenTests
    {
        [Fact]
        public void Given_Valid_Model_Should_Return_IsValid_As_True()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Field1 = 7,
                Field2 = 7,
                Field3 = 7,
                Field4 = 7,
                Field11 = 7,
                Field12 = 7,
                Field13 = 7,
                Field14 = 7,
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
                Field1 = 7,
                Field2 = 7,
                Field3 = 7,
                Field4 = 17,
                Field11 = 7,
                Field12 = 7,
                Field13 = 7,
                Field14 = 7,
            };

            var result = validator.Validate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_Inclusive_False_Fields_Should_Be_Invalid_Minimum()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Field1 = 5,
                Field2 = 5,
                Field3 = 5,
                Field4 = 5,
                Field11 = 5,
                Field12 = 5,
                Field13 = 5,
                Field14 = 5,
            };

            var result = validator.Validate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_Inclusive_False_Fields_Should_Be_Valid_Minimum()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Field1 = 6,
                Field2 = 6,
                Field3 = 6,
                Field4 = 6,
                Field11 = 5,
                Field12 = 5,
                Field13 = 5,
                Field14 = 5,
            };

            var result = validator.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Inclusive_False_Fields_Should_Be_Invalid_Maximum()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Field1 = 10,
                Field2 = 10,
                Field3 = 10,
                Field4 = 10,
                Field11 = 10,
                Field12 = 10,
                Field13 = 10,
                Field14 = 10,
            };

            var result = validator.Validate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Given_Inclusive_False_Fields_Should_Be_Valid_Maximum()
        {
            var validator = new TestClassValidator();
            var model = new TestClass
            {
                Field1 = 9,
                Field2 = 9,
                Field3 = 9,
                Field4 = 9,
                Field11 = 10,
                Field12 = 10,
                Field13 = 10,
                Field14 = 10,
            };

            var result = validator.Validate(model);

            Assert.True(result.IsValid);
        }

        public class TestClass
        {
            public int Field1 { get; set; }
            public decimal Field2 { get; set; }
            public double Field3 { get; set; }
            public float Field4 { get; set; }

            public int Field11 { get; set; }
            public decimal Field12 { get; set; }
            public double Field13 { get; set; }
            public float Field14 { get; set; }
        }

        public class TestClassValidator : ValidateThis<TestClass>
        {
            public TestClassValidator()
            {
                For(x => x.Field1)
                    .Ensure(new Between(5, 10));
                For(x => x.Field11)
                    .Ensure(new Between(5, 10).IsInclusive(true));
                For(x => x.Field2)
                    .Ensure(new Between(5m, 10m));
                For(x => x.Field12)
                    .Ensure(new Between(5m, 10m).IsInclusive(true));
                For(x => x.Field3)
                    .Ensure(new Between(5d, 10d));
                For(x => x.Field13)
                    .Ensure(new Between(5d, 10d).IsInclusive(true));
                For(x => x.Field4)
                    .Ensure(new Between(5f, 10f));
                For(x => x.Field14)
                    .Ensure(new Between(5f, 10f).IsInclusive(true));
            }
        }
    }
}
