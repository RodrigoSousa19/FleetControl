using FleetControl.Application.Models;
using FleetControl.Application.Models.Customers;
using MediatR;

namespace FleetControl.Application.Queries.Customers.GetAll
{
    public class GetAllCustomersQuery : IRequest<ResultViewModel<IList<CustomerViewModel>>>
    {
    }
}
