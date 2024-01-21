using Domain.Exceptions;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class TokenDates : ValueObject
    {
        private DateTime _MinDate = DateTime.UtcNow;
        private DateTime _TokenCreated;
        private DateTime _TokenExpires;
        public TokenDates(DateTime tokenCreated, DateTime tokenExpires)
        {
            TokenCreated = tokenCreated;
            TokenExpires = tokenExpires;
        }
        public DateTime TokenExpires
        {
            get
            {
                return _TokenExpires;
            }
            private set
            {
                if (value < _MinDate)
                {
                    throw new DomainValidationException("Cannot expire before current day ");
                }
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
