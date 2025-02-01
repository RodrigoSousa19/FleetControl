using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Customers.EnableCustomer
{
    public class EnableCustomerCommandHandler : IRequestHandler<EnableCustomerCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Customer> _repository;

        public EnableCustomerCommandHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(EnableCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetById(request.Id);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente informado.");

            if (customer.Enabled)
                return ResultViewModel.Error("O cliente informado já se encontra ativo.");

            customer.Enable();

            await _repository.Update(customer);

            return ResultViewModel.Success();
        }
    }
}
