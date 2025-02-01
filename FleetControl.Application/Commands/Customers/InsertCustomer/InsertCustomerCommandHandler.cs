using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Commands.Customers.InsertCustomer
{
    public class InsertCustomerCommandHandler : IRequestHandler<InsertCustomerCommand, ResultViewModel>
    {
        private readonly IGenericRepository<Customer> _repository;

        public InsertCustomerCommandHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Create(request.ToEntity());

            return ResultViewModel<Customer>.Success(customer);
        }
    }
}
