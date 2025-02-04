using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Customers.DisableCustomer
{
    public class DisableCustomerHandler : IRequestHandler<DisableCustomerCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Customer> _repository;

        public DisableCustomerHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetById(request.Id);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente informado.");

            if (!customer.Enabled)
                return ResultViewModel.Error("O cliente informado já se encontra inativo.");

            customer.Disable();

            await _repository.Update(customer);

            return ResultViewModel.Success();
        }
    }
}
