using System.ComponentModel.Composition;

namespace Sandra.SimpleValidator
{
    [InheritedExport(typeof(IModelValidator))]
    public interface IModelValidator
    {
        /// <summary>
        /// Validates all rules on a given object
        /// </summary>
        /// <param name="modelToValidate">The model to validate</param>
        /// <param name="validateAllRules">Specifies if all rules should be validated for a given property, when false will short circuit on first error</param>
        /// <returns></returns>
        ValidationResult Validate(object modelToValidate, bool validateAllRules = false);
    }
}