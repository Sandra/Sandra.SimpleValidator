using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandra.SimpleValidator
{
    public class ValidationService
    {
        static ValidationService()
        {
            ModelValidators = new Dictionary<Type, IModelValidator>();

            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                                 .SelectMany(s => s.GetTypes())
                                 .Where(x => x.BaseType != null &&
                                             x.BaseType.GetInterfaces().Any(y => y == typeof (IModelValidator)) &&
                                             !x.IsAbstract && x.IsClass);

            foreach (var modelValidator in types)
            {
                if (modelValidator.BaseType == null)
                    continue;

                var baseType = modelValidator.BaseType.GetGenericArguments()[0];

                ModelValidators.Add(baseType, Activator.CreateInstance(modelValidator) as IModelValidator);
            }
        }

        private static IDictionary<Type, IModelValidator> ModelValidators { get; set; }

        public virtual ValidationResult This<T>(T model)
        {
            var validator = ModelValidators[typeof (T)];

            return validator.Validate(model);
        }
    }
}