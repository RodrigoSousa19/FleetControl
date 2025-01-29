namespace FleetControl.Core.Entities
{
    public class CostCenter : BaseEntity
    {
        public CostCenter(string description)
        {
            Description = description;
            Enabled = true;
        }

        public string Description { get; private set; }

        public void Update(string description)
        {
            Description = description;
        }
    }
}
