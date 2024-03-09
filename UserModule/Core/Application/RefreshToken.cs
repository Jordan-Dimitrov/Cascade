using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application
{
    public sealed class RefreshToken
    {
        private const int _Length = 64;
        private string _Value = null!;
        private DateTime _Expires;
        private Guid _UserId;
        public RefreshToken(string value, DateTime expires, Guid userId)
        {
            Value = value;
            Expires = expires;
            UserId = userId;
        }

        public DateTime Expires
        {
            get
            {
                return _Expires;
            }
            private set
            {
                _Expires = value;
            }
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

        public Guid UserId
        {
            get
            {
                return _UserId;
            }
            private set
            {
                _UserId = value;
            }
        }
    }
}
