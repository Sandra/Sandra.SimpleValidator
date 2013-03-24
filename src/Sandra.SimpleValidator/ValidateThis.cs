using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Sandra.SimpleValidator
{
    public abstract class ValidateThis<T> : IModelValidator
    {
        private readonly ICollection<RuleBuilder> _rules = new Collection<RuleBuilder>();

        public ValidationResult Validate(object modelToValidate, bool validateAllRules = false)
        {
            var result = new ValidationResult();

            foreach (var propertyRule in _rules)
            {
                var value = propertyRule.Delegate.Invoke((T) modelToValidate);

                Tuple<bool, IEnumerable<ValidationError>> propertyResult;

                if (propertyRule is ComparePropertyRule)
                {
                    var compareRule = (ComparePropertyRule) propertyRule;
                    var valueToCompare = compareRule.Delegate2.Invoke((T) modelToValidate);
                    propertyResult = compareRule.RunRulesWith(value, valueToCompare, validateAllRules);
                }
                else
                {
                    propertyResult = ((PropertyRule)propertyRule).RunRulesWith(value, validateAllRules);
                }

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

        protected ComparePropertyRule Compare(Expression<Func<T, object>> selector)
        {
            var rule = new ComparePropertyRule(selector);

            _rules.Add(rule);

            return rule;
        }

        protected class ComparePropertyRule : RuleBuilder
        {
            protected string PropertyNameToCompareTo;
            protected ICollection<ICompareRule> Rules = new List<ICompareRule>();

            public ComparePropertyRule(Expression<Func<T, dynamic>> property) : base(property)
            {
            }

            public Func<T, dynamic> Delegate2 { get; private set; }

            public ComparePropertyRule With(Expression<Func<T, dynamic>> property)
            {
                PropertyNameToCompareTo = property.GetCorrectPropertyName();
                Delegate2 = property.Compile();

                return this;
            }

            public ComparePropertyRule Ensure(ICompareRule rule)
            {
                Rules.Add(rule);

                return this;
            }

            public Tuple<bool, IEnumerable<ValidationError>> RunRulesWith(dynamic value, dynamic valueToCompare, bool validateAllRules)
            {
                var isValid = true;
                var errors = new List<ValidationError>();

                foreach (var rule in Rules)
                {
                    if (!rule.IsValid(value, valueToCompare))
                    {
                        isValid = false;
                        errors.Add(new ValidationError
                        {
                            PropertyName = PropertyName,
                            Message = rule.Message
                        });

                        if (!validateAllRules)
                        {
                            break;
                        }
                    }
                }

                return new Tuple<bool, IEnumerable<ValidationError>>(isValid, errors);
            }
        }

        protected class PropertyRule : RuleBuilder
        {
            protected ICollection<IRule> Rules = new List<IRule>();

            public PropertyRule(Expression<Func<T, dynamic>> property) : base(property)
            {
            }

            public PropertyRule Ensure(IRule rule)
            {
                Rules.Add(rule);

                return this;
            }

            public Tuple<bool, IEnumerable<ValidationError>> RunRulesWith(dynamic value, bool validateAllRules)
            {
                var isValid = true;
                var errors = new List<ValidationError>();

                foreach (var rule in Rules)
                {
                    if (!rule.IsValid(value))
                    {
                        isValid = false;
                        errors.Add(new ValidationError
                        {
                            PropertyName = PropertyName,
                            Message = rule.Message
                        });
                    }

                    if (!validateAllRules)
                    {
                        break;
                    }
                }

                return new Tuple<bool, IEnumerable<ValidationError>>(isValid, errors);
            }
        }

        protected class RuleBuilder
        {
            protected string PropertyName;

            public RuleBuilder(Expression<Func<T, dynamic>> property)
            {
                PropertyName = property.GetCorrectPropertyName();
                Delegate = property.Compile();
            }

            public Func<T, dynamic> Delegate { get; private set; }
        }
    }
}