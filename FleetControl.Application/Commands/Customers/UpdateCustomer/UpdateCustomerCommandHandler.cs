using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Customer> _repository;

        public UpdateCustomerCommandHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetById(request.IdCustomer);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente informado.");

            customer.Update(request.Name, request.Address, request.Contact, request.Cnpj, request.Email);

            await _repository.Update(customer);

            return ResultViewModel.Success();
        }
    }
}
