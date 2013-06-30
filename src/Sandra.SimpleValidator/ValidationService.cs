using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace Sandra.SimpleValidator
{
    public class ValidationService
    {
        private readonly bool _validateAllRules;

        static ValidationService()
        {
            ModelValidators = new Dictionary<Type, IModelValidator>();

            var catalog = new AggregateCatalog(
                new DirectoryCatalog(@".", @"*")
                );

            try
            {
                catalog.Catalogs.Add(new DirectoryCatalog(@".\bin", @"*"));
            }
            catch (Exception)
            {
            }

            var container = new CompositionContainer(catalog);
            var exportedValidators = container.GetExports<IModelValidator>();
            
            ModelValidators = new Dictionary<Type, IModelValidator>();

            foreach (var modelValidator in exportedValidators)
            {
                var modelValidatorInstance = modelValidator.Value;
                var modelValidatorType = modelValidatorInstance.GetType();

                if (modelValidatorType.BaseType == null)
                    continue;

                var baseType = modelValidatorType.BaseType.GetGenericArguments()[0];

                ModelValidators.Add(baseType, modelValidator.Value);
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