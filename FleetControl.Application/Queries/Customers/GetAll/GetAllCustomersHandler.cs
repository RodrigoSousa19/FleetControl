using FleetControl.Application.Models;
using FleetControl.Application.Models.Customers;
using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;
using MediatR;

namespace FleetControl.Application.Queries.Customers.GetAll
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, ResultViewModel<IList<CustomerViewModel>>>
    {
        private readonly IGenericRepository<Customer> _repository;

        public GetAllCustomersHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IList<CustomerViewModel>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAll();

            var model = customers.Select(CustomerViewModel.FromEntity).ToList();

            return ResultViewModel<IList<CustomerViewModel>>.Success(model);
        }
    }
}
