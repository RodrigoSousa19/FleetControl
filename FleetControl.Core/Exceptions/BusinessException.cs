using FleetControl.Core.Exceptions.Interfaces;

namespace FleetControl.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public List<FieldError> Errors { get; private set; }

        public BusinessException()
        {
            Errors = [];
        }

        public BusinessException(IFieldError error)
        {
            Errors =
            [
                new FieldError(error.Field,error.Message)
            ];
        }

        public void Verify(Func<bool> validation, IFieldError error)
        {
            try
            {
                if (!validation())
                    Errors.Add(new FieldError(error.Field, error.Message));
            }
            catch
            {
                Errors.Add(new FieldError(error.Field, error.Message));
            }
        }

        public void TryThrow()
        {
            if (HasErrors)
                throw this;
        }

        public bool HasErrors => Errors.Count != 0;

        public override string ToString()
        {
            return string.Join(" ", Errors.Select(e => $"Campo: {e.Field} - Erro: {e.Message}"));
        }
    }
}
