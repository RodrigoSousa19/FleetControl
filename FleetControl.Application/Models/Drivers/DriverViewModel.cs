using FleetControl.Core.Entities;

namespace FleetControl.Application.Models.Drivers
{
    public class DriverViewModel
    {
        public DriverViewModel(int id, string fullName, string documentNumber, string documentType, bool enabled, IReadOnlyList<string> projects)
        {
            Id = id;
            FullName = fullName;
            DocumentNumber = documentNumber;
            DocumentType = documentType;
            Projects = projects;
            Enabled = enabled;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string DocumentNumber { get; private set; }
        public string DocumentType { get; private set; }
        public bool Enabled { get; private set; }
        public IReadOnlyList<string> Projects { get; private set; }

        public static DriverViewModel FromEntity(Driver entity)
        {
            var driverProjects = entity.DriverProjects.Select(x => x.Project.Name).ToList();

            return new DriverViewModel(entity.Id, entity.User?.Name, entity.DocumentNumber, entity.GetDocumentDescription(), entity.Enabled, driverProjects);
        }
    }
}
