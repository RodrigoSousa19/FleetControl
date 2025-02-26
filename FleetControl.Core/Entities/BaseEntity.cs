namespace FleetControl.Core.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDeleted = false;
            Enabled = true;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool Enabled { get; set; }

        public void SetAsDeleted() => IsDeleted = true;

        public void Disable() => Enabled = false;
        public void Enable() => Enabled = true;
    }
}
