using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Application.Validations.CustomValidators;
using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories;
using MediatR;

namespace FleetControl.Application.Commands.Customers.InsertCustomer
{
    public class InsertCustomerHandler : IRequestHandler<InsertCustomerCommand, ResultViewModel<Customer>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public InsertCustomerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultViewModel<Customer>> Handle(InsertCustomerCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .ProveCustomValidation(new CnpjValidator(request.Cnpj), ErrorsList.InvalidCnpj)
                .IsEmailValid(request.Email, ErrorsList.InvalidEmail)
                .Validate();

            var customer = await _unitOfWork.CustomerRepository.Create(request.ToEntity());

            await _unitOfWork.SaveChangesAsync();

            return ResultViewModel<Customer>.Success(customer);
        }
    }
}
