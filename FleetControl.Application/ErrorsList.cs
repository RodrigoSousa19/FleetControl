using FleetControl.Core.Exceptions;
using FleetControl.Core.Exceptions.Interfaces;

namespace FleetControl.Application
{
    public static class ErrorsList
    {
        public static IFieldError EmptyDescription = new FieldError("Description", "A Descrição não pode ser nula ou vazia.");

        public static IFieldError InvalidCnh = new FieldError("Cnh", "A CNH informada é inválida.");
        public static IFieldError InvalidCpf = new FieldError("Cpf", "O CPF informado é inválido.");
        public static IFieldError InvalidCnpj = new FieldError("Cnpj", "O CNPJ informado é inválido.");

        public static IFieldError InvalidEmail = new FieldError("Email", "Formato de email inválido.");
        public static IFieldError EmptyEmail = new FieldError("Email", "O email não pode ser vazio ou nulo.");

        public static IFieldError InvalidDateRange = new FieldError("StartDate and EndDate", "A data inicial deve ser menor que a data final.");
        public static IFieldError EmptyComment = new FieldError("Content", "O comentário não pode ser nulo ou vazio.");
        public static IFieldError InvalidUserName = new FieldError("Name", "O campo 'Nome' não pode ser vazio ou nulo.");

        public static IFieldError EmptyCarBrand = new FieldError("Brand", "O campo 'Marca' do veículo não pode ser vazio ou nulo.");
        public static IFieldError EmptyCarModel = new FieldError("Model", "O campo 'Modelo' não pode ser vazio ou nulo.");
        public static IFieldError EmptyCarFuelType = new FieldError("FuelType", "O campo 'Tipo de combustível' não pode ser vazio ou nulo.");
        public static IFieldError EmptyCarColor = new FieldError("Color", "O campo 'Cor' do veículo não pode ser vazio ou nulo.");
        public static IFieldError InvalidLicensePlate = new FieldError("LicensePlate", "A placa informada para o veículo não é válida.");
        public static IFieldError InvalidCarMileAge = new FieldError("MileAge", "A quilometragem do veículo deve ser igual ou maior que zero.");

        public static IFieldError InvalidTotalCostValue = new FieldError("TotalCost", "O valor total da manutenção deve ser igual ou maior que zero.");
    }
}
