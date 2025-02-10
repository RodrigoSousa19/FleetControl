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

        public Expressions(Func<bool> expression, IFieldError errorContent)
        {
            Expression = expression;
            ErrorContent = errorContent;
        }

        public Func<bool> Expression { get; set; }
        public IFieldError ErrorContent { get; set; }
    }

    public class Validator
    {
        private IList<Expressions> _expressions = [];

        public Validator()
        {
        }

        private Validator AddExpression(Func<bool> expression, IFieldError error)
        {
            _expressions.Add(new Expressions(expression, error));
            return this;
        }

        public Validator ProveCustomValidation(ICustomValidator customValidator, IFieldError error)
        {
            return AddExpression(() => customValidator.Validate(), error);
        }

        public Validator ValidateStringWithRegex(object target, string compareRegex, IFieldError error)
        {
            return AddExpression(() =>
            {
                Regex regex = new Regex(compareRegex);
                Match match = regex.Match(target.ToString() ?? "");
                return match.Success;
            }, error);
        }

        public Validator IsGreaterThanZero(object target, IFieldError error)
        {
            return AddExpression(() =>
            {
                if (target == null) return false;

                return target switch
                {
                    sbyte value when value > 0 => true,
                    byte value when value > 0 => true,
                    short value when value > 0 => true,
                    ushort value when value > 0 => true,
                    int value when value > 0 => true,
                    uint value when value > 0 => true,
                    long value when value > 0 => true,
                    ulong value when value > 0 => true,
                    float value when value > 0 => true,
                    double value when value > 0 => true,
                    decimal value when value > 0 => true,
                    string str when decimal.TryParse(str, out var decimalValue) && decimalValue > 0 => true,
                    _ => false
                };
            }, error);
        }

        public Validator IsGreaterThanOrEqualTo(object target, object minValue, IFieldError error)
        {
            return AddExpression(() =>
            {
                if (target == null || minValue == null) return false;

                return (target, minValue) switch
                {
                    (sbyte value, sbyte min) => value >= min,
                    (byte value, byte min) => value >= min,
                    (short value, short min) => value >= min,
                    (ushort value, ushort min) => value >= min,
                    (int value, int min) => value >= min,
                    (uint value, uint min) => value >= min,
                    (long value, long min) => value >= min,
                    (ulong value, ulong min) => value >= min,
                    (float value, float min) => value >= min,
                    (double value, double min) => value >= min,
                    (decimal value, decimal min) => value >= min,
                    (string str, _) when decimal.TryParse(str, out var decValue) && Convert.ToDecimal(minValue) is var minDec => decValue >= minDec,
                    _ => false
                };
            }, error);
        }

        public Validator IsLessThanOrEqualTo(object target, object maxValue, IFieldError error)
        {
            return AddExpression(() =>
            {
                if (target == null || maxValue == null) return false;

                return (target, maxValue) switch
                {
                    (sbyte value, sbyte max) => value <= max,
                    (byte value, byte max) => value <= max,
                    (short value, short max) => value <= max,
                    (ushort value, ushort max) => value <= max,
                    (int value, int max) => value <= max,
                    (uint value, uint max) => value <= max,
                    (long value, long max) => value <= max,
                    (ulong value, ulong max) => value <= max,
                    (float value, float max) => value <= max,
                    (double value, double max) => value <= max,
                    (decimal value, decimal max) => value <= max,
                    (string str, _) when decimal.TryParse(str, out var decValue) && Convert.ToDecimal(maxValue) is var maxDec => decValue <= maxDec,
                    _ => false
                };
            }, error);
        }

        public Validator IsValidDate(object target, IFieldError error)
        {
            return AddExpression(() => DateTime.TryParse(target?.ToString(), out _), error);
        }

        public Validator IsDateWithinRange(object target, DateTime startDate, DateTime endDate, IFieldError error)
        {
            return AddExpression(() =>
            {
                if (DateTime.TryParse(target?.ToString(), out DateTime date))
                    return date >= startDate && date <= endDate;
                return false;
            }, error);
        }

        public Validator IsValidDateRange(object startDate, object endDate, IFieldError error)
        {
            return AddExpression(() =>
            {
                if(DateTime.TryParse(startDate.ToString(),out DateTime start) && DateTime.TryParse(endDate.ToString(),out DateTime end))
                {
                    var startDateAfterEndDate = start > end;
                    var endDateBeforeStartDate = end < start;

                    return startDateAfterEndDate || endDateBeforeStartDate;
                }
                return false;

            }, error);
        }

        public Validator IsNotNullOrEmpty(object target, IFieldError error)
        {
            return AddExpression(() => !string.IsNullOrWhiteSpace(target?.ToString()), error);
        }

        public Validator IsPositiveInteger(object target, IFieldError error)
        {
            return AddExpression(() => int.TryParse(target?.ToString(), out int value) && value > 0, error);
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
