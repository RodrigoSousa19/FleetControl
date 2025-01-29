namespace FleetControl.Core.Entities
{
    public class Project : BaseEntity
    {
        public Project(string nome, int idCostCenter, int idCustomer)
        {
            Nome = nome;
            IdCostCenter = idCostCenter;
            IdCustomer = idCustomer;
        }

        public string Nome { get; private set; }
        public int IdCostCenter { get; private set; }
        public CostCenter CostCenter { get; set; }
        public int IdCustomer { get; private set; }
        public Customer Customer { get; set; }
        
        public void Update(string nome, int idCostCenter, int idCustomer)
        {
            Nome = nome;
            IdCostCenter = idCostCenter;
            IdCustomer = idCustomer;
        }
    }
}
