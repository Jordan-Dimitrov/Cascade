using Domain.Exceptions;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Token : ValueObject
    {
        private const int _Length = 64;
        private string _Value = null!;
        public Token(string value)
        {
            Value = value;
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

                if (value.Length != _Length)
                {
                    throw new DomainValidationException($"Must be {_Length} characters.");
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
