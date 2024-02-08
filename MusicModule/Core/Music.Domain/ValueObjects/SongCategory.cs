using Shared.Exceptions;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.ValueObjects
{
    public sealed class SongCategory : ValueObject
    {
        private const int _MinLength = 4;
        private const int _MaxLength = 16;
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
