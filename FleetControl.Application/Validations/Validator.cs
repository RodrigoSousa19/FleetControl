using FleetControl.Application.Validations.CustomValidators.Interfaces;
using FleetControl.Core.Exceptions;
using FleetControl.Core.Exceptions.Interfaces;
using System.Text.RegularExpressions;

namespace FleetControl.Application.Validations
{
    public class Expressions
    {
        public Expressions()
        {

        }

        public Expressions(Func<bool> expression, IFieldError errorContent, object value)
        {
            Expression = expression;
            ErrorContent = errorContent;
            Value = value;
        }

        public Func<bool> Expression { get; set; }
        public IFieldError ErrorContent { get; set; }
        public object Value { get; set; }
    }

    public class Validator
    {
        private IList<Expressions> _expressions = [];

        public Validator()
        {
        }

        private Validator AddExpression(Func<bool> expression, IFieldError error, object value)
        {
            _expressions.Add(new Expressions(expression, error, value));
            return this;
        }

        public Validator ProveCustomValidation(ICustomValidator customValidator, IFieldError error)
        {
            return AddExpression(() => customValidator.Validate(), error, customValidator.Target);
        }

        public Validator ValidateStringWithRegex(object target, string compareRegex, IFieldError error)
        {
            return AddExpression(() =>
            {
                Regex regex = new Regex(compareRegex);
                Match match = regex.Match(target.ToString() ?? "");
                return match.Success;
            }, error, target);
        }

        public Validator IsGreaterThanZero(object target, IFieldError error)
        {
            return AddExpression(() => { _ = decimal.TryParse(target.ToString(), out decimal value); return value > 0; }, error, target);
        }

        public Validator IsGreaterThanOrEqualTo(object target, decimal minValue, IFieldError error)
        {
            return AddExpression(() => { _ = decimal.TryParse(target.ToString(), out decimal value); return value >= minValue; }, error, target);
        }

        public Validator IsValidDate(object target, IFieldError error)
        {
            return AddExpression(() => DateTime.TryParse(target?.ToString(), out _), error, target);
        }

        public Validator IsDateWithinRange(object target, DateTime startDate, DateTime endDate, IFieldError error)
        {
            return AddExpression(() =>
            {
                if (DateTime.TryParse(target?.ToString(), out DateTime date))
                    return date >= startDate && date <= endDate;
                return false;
            }, error, target);
        }

        public Validator IsNotNullOrEmpty(object target, IFieldError error)
        {
            return AddExpression(() => !string.IsNullOrWhiteSpace(target?.ToString()), error, target);
        }

        public Validator IsPositiveInteger(object target, IFieldError error)
        {
            return AddExpression(() => int.TryParse(target?.ToString(), out int value) && value > 0, error, target);
        }

        public Validator IsEmailValid(object target, IFieldError error)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return ValidateStringWithRegex(target, emailPattern, error);
        }

        public Validator IsLicensePlateValid(object target, IFieldError error)
        {
            string licensePlatePattern = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";
            return ValidateStringWithRegex(target, licensePlatePattern, error);
        }


        public void Validate()
        {
            var businessException = new BusinessException();

            foreach (var expression in _expressions)
                businessException.Verify(expression.Expression, expression.ErrorContent);

            businessException.TryThrow();
        }
    }
}
