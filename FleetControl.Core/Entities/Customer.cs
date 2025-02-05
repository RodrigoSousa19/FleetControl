namespace FleetControl.Core.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(string name, string address, string contact, string cnpj, string email)
        {
            Name = name;
            Address = address;
            Contact = contact;
            Cnpj = cnpj;
            Email = email;

            Enabled = true;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Contact { get; private set; }
        public string Cnpj { get; private set; }
        public string Email { get; private set; }

        public void Update(string name, string address, string contact, string cnpj, string email)
        {
            Name = name;
            Address = address;
            Contact = contact;
            Cnpj = cnpj;
            Email = email;

            UpdatedAt = DateTime.Now;
        }
    }
}
