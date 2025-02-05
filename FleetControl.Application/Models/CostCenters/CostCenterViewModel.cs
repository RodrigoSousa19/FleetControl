using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.CostCenters
{
    public class CostCenterViewModel
    {
        public CostCenterViewModel(int id, string description, bool enabled)
        {
            Id = id;
            Description = description;
            Enabled = enabled;
        }

        public int Id { get; private set; }
        public string Description { get; private set; }
        public bool Enabled { get; private set; }

        public static CostCenterViewModel FromEntity(CostCenter entity) => new(entity.Id, entity.Description, entity.Enabled);
    }
}
