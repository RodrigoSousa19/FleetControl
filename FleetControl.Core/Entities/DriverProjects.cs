namespace FleetControl.Core.Entities
{
    public class DriverProjects : BaseEntity
    {
        public int IdDriver { get; private set; }
        public Driver Driver { get; set; }
        public int IdProject { get; private set; }
        public Project Project { get; set; }
    }
}
