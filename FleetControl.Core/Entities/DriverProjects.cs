namespace FleetControl.Core.Entities
{
    public class DriverProjects : BaseEntity
    {
        public DriverProjects(int idDriver, int idProject)
        {
            IdDriver = idDriver;
            IdProject = idProject;
        }

        public int IdDriver { get; private set; }
        public Driver Driver { get; set; }
        public int IdProject { get; private set; }
        public Project Project { get; set; }

        public void Update(int idDriver, int idProject)
        {
            IdDriver = idDriver;
            IdProject = idProject;

            UpdatedAt = DateTime.Now;
        }
    }
}
