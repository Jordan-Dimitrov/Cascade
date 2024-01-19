using Domain.Aggregates.RefreshTokenAggregate;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Aggregates.UserAggregate
{
    public enum UserRole
    {
        Visitor,
        User,
        Admin
    }
    public sealed class User : AggregateRoot
    {
        private Username _Username;
        private Guid _RefreshTokenId;
        internal User(Username username,
            byte[] passwordHash, byte[] passwordSalt,
            Guid refreshTokenId, UserRole permissionType)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            RefreshTokenId = refreshTokenId;
            PermissionType = permissionType;
        }

        [JsonConstructor]
        private User()
        {

        }

        public Username Username
        {
            get
            {
                return _Username;
            }
            private set
            {
                _Username = value ?? throw new DomainValidationException("The name cannot be null");
            }
        }
        public byte[] PasswordHash { get; private set; } = null!;
        public byte[] PasswordSalt { get; private set; } = null!;
        public Guid RefreshTokenId 
        { 
            get 
            {
                return _RefreshTokenId;
            }
            private set 
            {
                if(value == Guid.Empty)
                {
                    throw new DomainValidationException("The RefreshToken cannot be null");
                }

                _RefreshTokenId = value;
            } 
        }
        public UserRole PermissionType { get; private set; }
    }
}
