using FleetControl.Application.Models;
using FleetControl.Application.Validations;
using FleetControl.Application.Validations.CustomValidators;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Customers.InsertCustomer
{
    public class InsertCustomerHandler : IRequestHandler<InsertCustomerCommand, ResultViewModel<Customer>>
    {
        private readonly IGenericRepository<Customer> _repository;

        public InsertCustomerHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<Customer>> Handle(InsertCustomerCommand request, CancellationToken cancellationToken)
        {
            new Validator()
                .ProveCustomValidation(new CnpjValidator(request.Cnpj), ErrorsList.InvalidCnpj)
                .IsEmailValid(request.Email,ErrorsList.InvalidEmail)
                .Validate();

            var customer = await _repository.Create(request.ToEntity());

            return ResultViewModel<Customer>.Success(customer);
        }
    }
}
