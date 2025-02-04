using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Queries.Customers.GetAll
{
    public class GetAllCustomersQuery : IRequest<ResultViewModel<IList<Customer>>>
    {
    }
}
