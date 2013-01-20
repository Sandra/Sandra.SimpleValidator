using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Sandra.SimpleValidator
{
    public abstract class ValidateBase<T> : IModelValidator
    {
        private readonly ICollection<PropertyRule> _rules = new Collection<PropertyRule>();

        public ValidationResult Validate(object modelToValidate)
        {
            var result = new ValidationResult();

            foreach (PropertyRule propertyRule in _rules)
            {
                dynamic value = propertyRule.Delegate.Invoke((T) modelToValidate);

                Tuple<bool, IEnumerable<ValidationError>> propertyResult = propertyRule.RunRulesWith(value);

                if (!propertyResult.Item1)
                {
                    result.Messages.AddRange(propertyResult.Item2);
                }
            }

            return result;
        }

        protected PropertyRule For(Expression<Func<T, object>> selector)
        {
            var rule = new PropertyRule(selector);

            _rules.Add(rule);

            return rule;
        }

        protected class PropertyRule
        {
            private readonly string _propertyName;
            private readonly ICollection<IRule> _rules = new List<IRule>();

            public PropertyRule(Expression<Func<T, dynamic>> property)
            {
                _propertyName = ((MemberExpression) property.Body).Member.Name;
                Delegate = property.Compile();
            }

            public Func<T, dynamic> Delegate { get; private set; }

            public PropertyRule Ensure(IRule rule)
            {
                _rules.Add(rule);

                return this;
            }

            public Tuple<bool, IEnumerable<ValidationError>> RunRulesWith(dynamic value)
            {
                var isValid = true;
                var errors = new List<ValidationError>();

                foreach (var rule in _rules)
                {
                    if (!rule.IsValid(value))
                    {
                        isValid = false;
                        errors.Add(new ValidationError
                        {
                            PropertyName = _propertyName,
                            Message = rule.Message
                        });
                    }
                }

                return new Tuple<bool, IEnumerable<ValidationError>>(isValid, errors);
            }
        }
    }
}