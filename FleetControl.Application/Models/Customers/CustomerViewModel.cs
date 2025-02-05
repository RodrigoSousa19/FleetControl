using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.Customers
{
    public class CustomerViewModel
    {
        public CustomerViewModel(int id, string name, string address, string contact, string cnpj, string email, bool enabled)
        {
            Id = id;
            Name = name;
            Address = address;
            Contact = contact;
            Cnpj = cnpj;
            Email = email;
            Enabled = enabled;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Contact { get; private set; }
        public string Cnpj { get; private set; }
        public string Email { get; private set; }
        public bool Enabled { get; private set; }

        public static CustomerViewModel FromEntity(Customer entity) => new(entity.Id, entity.Name, entity.Address, entity.Contact, entity.Cnpj, entity.Email, entity.Enabled);
    }
}
