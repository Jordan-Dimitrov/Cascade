using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.ValueObjects
{
    public sealed class FollowCount : ValueObject
    {
        private int _Value;
        public FollowCount(int value)
        {
            Value = value;
        }

        [JsonConstructor]
        private FollowCount()
        {

        }

        public int Value
        {
            get
            {
                return _Value;
            }
            private set
            {
                if (value < 0)
                {
                    throw new DomainValidationException("Follow Count cannot be less than zero");
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
