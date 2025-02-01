using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Customers.EnableCustomer
{
    public class EnableCustomerCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public EnableCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
