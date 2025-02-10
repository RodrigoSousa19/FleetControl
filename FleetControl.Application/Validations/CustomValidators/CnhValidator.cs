using FleetControl.Application.Validations.CustomValidators.Interfaces;

namespace FleetControl.Application.Validations.CustomValidators
{
    public class CnhValidator : ICustomValidator
    {
        public object Target { get; }

        public CnhValidator(object target)
        {
            Target = target;
        }

        public bool Validate()
        {
            return IsValidCNH();
        }

        private bool IsValidCNH()
        {
            var cnh = Target.ToString();

            if (string.IsNullOrEmpty(cnh))
                return false;

            if (cnh.Length != 11)
                return false;

            if (cnh.Distinct().Count() == 1)
                return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (cnh[i] - '0') * (9 - i);
            }
            int primeiroDV = soma % 11;
            primeiroDV = (primeiroDV >= 10) ? 0 : primeiroDV;

            soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (cnh[i] - '0') * (1 + i);
            }
            soma += primeiroDV * 9;

            int segundoDV = soma % 11;
            segundoDV = (segundoDV >= 10) ? 0 : segundoDV;

            return (cnh[9] - '0' == primeiroDV) && (cnh[10] - '0' == segundoDV);
        }
    }
}
