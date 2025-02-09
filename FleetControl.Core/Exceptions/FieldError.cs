using FleetControl.Core.Exceptions.Interfaces;

namespace FleetControl.Core.Exceptions
{
    public class FieldError : IFieldError
    {
        public FieldError()
        {

        }
        public FieldError(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Field { get; set; }
        public string Message { get; set; }
    }
}
