using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System.Text.Json.Serialization;

namespace Music.Domain.ValueObjects
{
    public sealed class AlbumName : ValueObject
    {
        private const int _MinLength = 4;
        private const int _MaxLength = 16;
        private string _Value = null!;
        public AlbumName(string value)
        {
            Value = value;
        }

        [JsonConstructor]
        private AlbumName()
        {

        }

        public string Value
        {
            get
            {
                return _Value;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new DomainValidationException("Cannot be null or empty.");
                }

                if (value.Length < _MinLength || value.Length > _MaxLength)
                {
                    throw new DomainValidationException($"Must be in between {_MinLength} and {_MaxLength} characters.");
                }

                _Value = value;

            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _Value;
        }
    }
}
