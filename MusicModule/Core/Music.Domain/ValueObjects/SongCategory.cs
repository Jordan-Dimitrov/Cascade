using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System.Text.Json.Serialization;

namespace Music.Domain.ValueObjects
{
    public sealed class SongCategory : ValueObject
    {
        private string[] _AllowedCategories = { "Srubsko", "Hidden" };
        private string _Value = null!;
        public SongCategory(string value)
        {
            Value = value;
        }

        [JsonConstructor]
        private SongCategory()
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

                if (!_AllowedCategories.Contains(value))
                {
                    throw new DomainValidationException($"{value} is not a valid category!");
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
