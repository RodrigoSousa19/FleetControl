using FleetControl.Application.Models;
using MediatR;

namespace FleetControl.Application.Commands.Customers.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<ResultViewModel>
    {
        public int IdCustomer { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
    }
}
