namespace FleetControl.Core.Entities
{
    public class Project : BaseEntity
    {
        public Project(string name, int idCostCenter, int idCustomer)
        {
            Name = name;
            IdCostCenter = idCostCenter;
            IdCustomer = idCustomer;

            Enabled = true;
        }

        public string Name { get; private set; }
        public int IdCostCenter { get; private set; }
        public CostCenter CostCenter { get; set; }
        public int IdCustomer { get; private set; }
        public Customer Customer { get; set; }

        public void Update(string nome, int idCostCenter, int idCustomer)
        {
            Name = nome;
            IdCostCenter = idCostCenter;
            IdCustomer = idCustomer;

            UpdatedAt = DateTime.Now;
        }
    }
}
