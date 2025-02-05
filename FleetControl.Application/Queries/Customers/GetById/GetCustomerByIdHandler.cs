using FleetControl.Application.Models;
using FleetControl.Application.Models.Customers;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Customers.GetById
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, ResultViewModel<CustomerViewModel>>
    {

        private readonly IGenericRepository<Customer> _repository;

        public GetCustomerByIdHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<CustomerViewModel>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetById(request.Id);

            if (customer is null)
                return ResultViewModel<CustomerViewModel>.Error("Não foi possível localizar o cliente informado.");

            var model = CustomerViewModel.FromEntity(customer);

            return ResultViewModel<CustomerViewModel>.Success(model);
        }
    }
}
