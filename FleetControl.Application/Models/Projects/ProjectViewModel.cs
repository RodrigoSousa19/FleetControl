using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.Projects
{
    public class ProjectViewModel
    {
        public ProjectViewModel(int id, string name, string customerName, string costCenter, bool enabled)
        {
            Id = id;
            Name = name;
            CustomerName = customerName;
            CostCenter = costCenter;
            Enabled = enabled;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string CustomerName { get; private set; }
        public string CostCenter { get; private set; }
        public bool Enabled { get; private set; }

        public static ProjectViewModel FromEntity(Project entity) => new(entity.Id, entity.Name, entity.Customer.Name, entity.CostCenter.Description, entity.Enabled);
    }
}
