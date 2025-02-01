using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Customers.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<ResultViewModel>
    {
        public int Id { get; private set; }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
