using Domain.Shared.Constants;
using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Users.Domain.DomainEntities;
using Users.Domain.DomainEvents;
using Users.Domain.ValueObjects;

namespace Users.Domain.Aggregates.UserAggregate
{
    public sealed class User : AggregateRoot
    {
        private const string _HiddenUsername = "Hidden";
        private byte[] _HiddenPassword = new byte[4];
        private Username _Username;
        private RefreshToken _RefreshToken;
        private User(Username username,
            byte[] passwordHash, byte[] passwordSalt,
            RefreshToken refreshToken, UserRole permissionType)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            RefreshToken = refreshToken;
            PermissionType = permissionType;
        }

        [JsonConstructor]
        private User()
        {

        }

        public static User CreateUser(string username,
            byte[] passwordHash,
            byte[] passwordSalt,
            RefreshToken refreshToken,
            UserRole userRole)
        {
            User user = new User(new Username(username), passwordHash, passwordSalt, refreshToken, userRole);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.Username.Value, (int)user.PermissionType));

            return user;
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

        public Guid RefreshTokenId
        {
            get
            {
                return _RefreshToken.Id;
            }
            private set
            {
                _RefreshTokenId = value;
            }
        }

        private Guid _RefreshTokenId;
        public User HideUserDetails()
        {
            _Username = new Username(_HiddenUsername);
            PasswordHash = _HiddenPassword;
            PermissionType = UserRole.Visitor;
            PasswordSalt = _HiddenPassword;

            RaiseDomainEvent(new UserHiddenDomainEvent(Id));

            return this;
        }

        public byte[] PasswordHash { get; private set; } = null!;
        public byte[] PasswordSalt { get; private set; } = null!;

        public RefreshToken RefreshToken
        {
            get
            {
                return _RefreshToken;
            }
            private set
            {
                _RefreshToken = value;
            }
        }

        public void SetRfreshToken(RefreshToken refreshToken)
        {
            _RefreshToken = refreshToken;
        }

        public void SetUsername(string username)
        {
            _Username = new Username(username);
        }

        public UserRole PermissionType { get; private set; }
    }
}
