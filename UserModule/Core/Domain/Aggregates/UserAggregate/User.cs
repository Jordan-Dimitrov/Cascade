using Domain.Shared.Constants;
using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System.Text.Json.Serialization;
using Users.Domain.DomainEvents;
using Users.Domain.ValueObjects;

namespace Users.Domain.Aggregates.UserAggregate
{
    public sealed class User : AggregateRoot
    {
        private const string _HiddenUsername = "Hidden";
        private byte[] _HiddenPassword = new byte[4];
        private Username _Username;
        private User(Username username,
            byte[] passwordHash, byte[] passwordSalt,
            UserRole permissionType)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            PermissionType = permissionType;
        }

        [JsonConstructor]
        private User()
        {

        }

        public static User CreateUser(string username,
            byte[] passwordHash,
            byte[] passwordSalt,
            UserRole userRole)
        {
            User user = new User(new Username(username), passwordHash, passwordSalt, userRole);

            if (userRole == UserRole.Artist)
            {
                user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.Username.Value, (int)user.PermissionType));
            }

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

        public User HideUserDetails()
        {
            _Username = new Username(_HiddenUsername);
            PasswordHash = _HiddenPassword;

            RaiseDomainEvent(new UserHiddenDomainEvent(Id, PermissionType));

            PermissionType = UserRole.Visitor;
            PasswordSalt = _HiddenPassword;

            return this;
        }

        public byte[] PasswordHash { get; private set; } = null!;
        public byte[] PasswordSalt { get; private set; } = null!;

        public void SetUsername(string username)
        {
            _Username = new Username(username);
        }

        public UserRole PermissionType { get; private set; }
    }
}
