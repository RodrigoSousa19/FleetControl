using FleetControl.Core.Enums.User;

namespace FleetControl.Core.Entities
{
    public class Driver : BaseEntity
    {
        public Driver(int idUser, string documentNumber, DocumentType documentType)
        {
            IdUser = idUser;
            DocumentNumber = documentNumber;
            DocumentType = documentType;

            Enabled = true;
        }
        public int IdUser { get; private set; }
        public User User { get; set; }
        public string DocumentNumber { get; private set; }
        public DocumentType DocumentType { get; private set; }

        public void Update(string documentNumber, DocumentType documentType)
        {
            DocumentNumber = documentNumber;
            DocumentType = documentType;

            UpdatedAt = DateTime.Now;
        }

        public string GetDocumentDescription()
        {
            return DocumentType switch
            {
                DocumentType.CPF => "CPF",
                DocumentType.DriversLicense => "CNH",
                _ => "Desconhecido"
            };
        }
    }
}
