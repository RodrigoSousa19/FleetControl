using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Customers.DisableCustomer
{
    public class DisableCustomerCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DisableCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
