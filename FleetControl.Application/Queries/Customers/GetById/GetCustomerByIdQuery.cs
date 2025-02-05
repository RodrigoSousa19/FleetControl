using FleetControl.Application.Models;
using FleetControl.Application.Models.Customers;
using MediatR;

namespace FleetControl.Application.Queries.Customers.GetById
{
    public class GetCustomerByIdQuery : IRequest<ResultViewModel<CustomerViewModel>>
    {
        public int Id { get; private set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
