namespace FleetControl.Core.Entities
{
    public class Driver : BaseEntity
    {
        public Driver(int idUser, string documentNumber, string documentType)
        {
            IdUser = idUser;
            DocumentNumber = documentNumber;
            DocumentType = documentType;

            DriverProjects = [];
        }
        public int IdUser { get; private set; }
        public User User { get; set; }
        public string DocumentNumber { get; private set; }
        public string DocumentType { get; private set; }
        public ICollection<DriverProjects> DriverProjects { get; set; }

        public void Update(string documentNumber, string documentType)
        {
            DocumentNumber = documentNumber;
            DocumentType = documentType;
        }
    }
}
