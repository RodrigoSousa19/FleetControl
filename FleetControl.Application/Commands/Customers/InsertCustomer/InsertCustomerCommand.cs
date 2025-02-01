using FleetControl.Application.Models;
using FleetControl.Core.Entities;
using MediatR;

namespace FleetControl.Application.Commands.Customers.InsertCustomer
{
    public class InsertCustomerCommand : IRequest<ResultViewModel<Customer>>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }

        public Customer ToEntity() => new(Name, Address, Contact, Cnpj, Email);
    }
}
