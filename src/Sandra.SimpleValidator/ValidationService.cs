using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandra.SimpleValidator
{
    public class ValidationService
    {
        private readonly bool validateAllRules;

        public ValidationService()
        {
            validateAllRules = false;
        }

        public ValidationService(bool validateAllRules)
        {
            this.validateAllRules = validateAllRules;
        }

        private static IDictionary<Type, IModelValidator> ModelValidators = new Dictionary<Type, IModelValidator>();

        public virtual ValidationResult This<T>(T model)
        {
            var modelType = typeof (T);
            var validator = ModelValidators[modelType];

            return validator.Validate(model, validateAllRules);
        }

        public static void RegisterAllFrom<T>()
        {
            var found = typeof(T).Assembly
                .DefinedTypes
                .Where(t => typeof(IModelValidator).IsAssignableFrom(t));
            
            foreach (var modelValidator in found)
            {
                // Create an instance of this rule.
                var modelRuleInstance = Activator.CreateInstance(modelValidator) as IModelValidator;

                if (modelValidator.BaseType == null)
                {
                    continue;
                }

                var baseType = modelValidator.BaseType.GetGenericArguments()[0];

                if (ModelValidators.ContainsKey(baseType))
                {
                    continue;
                }
                
                ModelValidators.Add(baseType, modelRuleInstance);
            }
        }
    }
}