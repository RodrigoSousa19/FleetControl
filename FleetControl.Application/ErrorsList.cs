using FleetControl.Core.Exceptions;
using FleetControl.Core.Exceptions.Interfaces;

namespace FleetControl.Application
{
    public static class ErrorsList
    {
        public static IFieldError InvalidDescription = new FieldError("Description", "Descrição inválida");
        public static IFieldError InvalidCpf = new FieldError("Cpf", "CPF inválido");
        public static IFieldError InvalidCnpj = new FieldError("Cnpj", "Cnpj inválido");
        public static IFieldError FieldShouldNotBeNullOrEmpty = new FieldError("{0}", "O campo {0} não pode estar em branco!");
        public static IFieldError LicensePlateInvalid = new FieldError("LicensePlate", "Placa do veículo inválida");
    }
}
