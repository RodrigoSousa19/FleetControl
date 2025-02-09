namespace FleetControl.Application.Validations.CustomValidators.Interfaces
{
    public interface ICustomValidator
    {
        object Target { get; }
        public bool Validate();
    }
}
