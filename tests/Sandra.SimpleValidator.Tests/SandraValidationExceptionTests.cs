using Sandra.SimpleValidator.Rules;
using Xunit;

namespace Sandra.SimpleValidator.Tests
{
    public class SandraValidationExceptionTests
    {
        [Fact]
        public void Given_Service_Throwing_SandraValidationException_Should_Contain_Results()
        {
            ValidationService.RegisterAllFrom<UserValidator>();

            var validationService = new ValidationService();
            var dummyService = new DummyService(validationService);

            var testUser = new User();

            Assert.Throws<SandraValidationException>(() => dummyService.DoSomething(testUser));
        }

        [Fact]
        public void Given_Service_Throwing_SandraValidationException_Should_Contain_One_Message()
        {
            ValidationService.RegisterAllFrom<UserValidator>();
            
            var validationService = new ValidationService();
            var dummyService = new DummyService(validationService);

            var testUser = new User();

            try
            {
                dummyService.DoSomething(testUser);
            }
            catch (SandraValidationException sve)
            {
                Assert.Equal(1, sve.Result.Messages.Count);
            }
        }

        public class DummyService
        {
            private readonly ValidationService _validate;

            public DummyService(ValidationService validate)
            {
                _validate = validate;
            }

            public void DoSomething(User user)
            {
                var result = _validate.This(user);

                if (result.IsInvalid)
                {
                    throw new SandraValidationException(result);
                }

                //Do nothing
            }
        }

        public class User
        {
            public string Name { get; set; }
        }

        public class UserValidator : ValidateThis<User>
        {
            public UserValidator()
            {
                For(x => x.Name)
                    .Ensure(new Required());
            }
        }
    }
}