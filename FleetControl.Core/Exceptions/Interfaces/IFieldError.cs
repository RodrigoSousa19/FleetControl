namespace FleetControl.Core.Exceptions.Interfaces
{
    public interface IFieldError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
