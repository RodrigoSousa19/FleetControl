using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Customers.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Customer> _repository;

        public DeleteCustomerCommandHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetById(request.Id);

            if (customer is null)
                return ResultViewModel.Error("Não foi possível encontrar o cliente informado.");

            customer.SetAsDeleted();

            await _repository.Update(customer);

            return ResultViewModel.Success();
        }
    }
}
