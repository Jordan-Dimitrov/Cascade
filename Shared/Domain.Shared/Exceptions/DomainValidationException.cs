using System.Runtime.Serialization;

namespace Domain.Shared.Exceptions
{
    [Serializable]
    public sealed class DomainValidationException : Exception
    {
        public DomainValidationException(string message)
            : base(message)
        {
        }

        public DomainValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private DomainValidationException()
        {
        }

        private DomainValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
