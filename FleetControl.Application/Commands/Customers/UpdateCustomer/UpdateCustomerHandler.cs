using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Application.Validations.CustomValidators;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ResultViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(request.IdCustomer);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente informado.");

            new Validator()
                .ProveCustomValidation(new CnpjValidator(request.Cnpj), ErrorsList.InvalidCnpj)
                .IsEmailValid(request.Email, ErrorsList.InvalidEmail)
                .Validate();

            customer.Update(request.Name, request.Address, request.Contact, request.Cnpj, request.Email);

            await _unitOfWork.CustomerRepository.Update(customer);

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
