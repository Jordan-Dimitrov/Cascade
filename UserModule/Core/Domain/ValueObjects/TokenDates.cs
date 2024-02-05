using Domain.Exceptions;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class TokenDates : ValueObject
    {
        private DateTime _TokenCreated;
        private DateTime _TokenExpires;
        public TokenDates(DateTime tokenCreated, DateTime tokenExpires)
        {
            TokenCreated = tokenCreated;
            TokenExpires = tokenExpires;
        }
        public void CheckTokenDates()
        {
            if (_TokenExpires < DateTime.UtcNow)
            {
                throw new DomainValidationException("Cannot expire before current day");
            }
        }

        [JsonConstructor]
        private TokenDates()
        {

        }
        public DateTime TokenExpires
        {
            get
            {
                return _TokenExpires;
            }
            private set
            {
                _TokenExpires = value;
            }
        }
        public DateTime TokenCreated
        {
            get
            {
                return _TokenCreated;
            }
            private set
            {
                _TokenCreated = value;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _TokenCreated;
            yield return _TokenExpires;
        }
    }
}
