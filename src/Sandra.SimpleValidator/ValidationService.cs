using System;
using System.Collections.Generic;

namespace Sandra.SimpleValidator
{
    public class ValidationService
    {
        private readonly bool _validateAllRules;

        static ValidationService()
        {
            ModelValidators = new Dictionary<Type, IModelValidator>();

            var discoveredModelValidators = MefHelpers.GetExportedTypes<IModelValidator>();
            if (discoveredModelValidators == null)
            {
                return;
            }

            foreach (var modelValidator in discoveredModelValidators)
            {
                // Create an instance of this rule.
                var modelRuleInstance = Activator.CreateInstance(modelValidator) as IModelValidator;

                if (modelValidator.BaseType == null)
                    continue;

                var baseType = modelValidator.BaseType.GetGenericArguments()[0];

                ModelValidators.Add(baseType, modelRuleInstance);
            }
        }

        public ValidationService()
        {
            _validateAllRules = false;
        }

        public ValidationService(bool validateAllRules)
        {
            _validateAllRules = validateAllRules;
        }

        private static IDictionary<Type, IModelValidator> ModelValidators { get; set; }

        public virtual ValidationResult This<T>(T model)
        {
            var modelType = typeof (T);
            var validator = ModelValidators[modelType];

            return validator.Validate(model, _validateAllRules);
        }
    }
}