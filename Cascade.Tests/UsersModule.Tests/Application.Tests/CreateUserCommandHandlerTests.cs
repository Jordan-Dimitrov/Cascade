using Domain.Shared.Constants;
using FakeItEasy;
using System.Text;
using Users.Application.Abstractions;
using Users.Application.Users.Commands;
using Users.Domain.Abstractions;
using Users.Domain.Aggregates.UserAggregate;
using Users.Domain.Wrappers;
using Xunit;

namespace Cascade.Tests.UsersModule.Tests.Application.Tests
{
    public class CreateUserCommandHandlerTests
    {
        private readonly IUserCommandRepository _UserCommandRepository;
        private readonly IUserQueryRepository _UserQueryRepository;
        private readonly IUserUnitOfWork _UnitOfWork;
        private readonly IAuthService _AuthService;
        public CreateUserCommandHandlerTests()
        {
            _UserCommandRepository = A.Fake<IUserCommandRepository>();
            _UserQueryRepository = A.Fake<IUserQueryRepository>();
            _UnitOfWork = A.Fake<IUserUnitOfWork>();
            _AuthService = A.Fake<IAuthService>();
        }
        [Fact]
        public async Task Handle_Should_Return_UserId()
        {
            UserPassword pass = A.Fake<UserPassword>();
            pass.PasswordHash = Encoding.UTF8.GetBytes("fakeHash");
            pass.PasswordSalt = Encoding.UTF8.GetBytes("fakeSalt");
            User user = User.CreateUser("FakeName", pass.PasswordHash, pass.PasswordSalt, UserRole.User);

            var command = new CreateUserCommand("FakeName", "FakePass", UserRole.User);
            var handler = new CreateUserCommandHandler(_UserCommandRepository,
                _AuthService, _UnitOfWork, _UserQueryRepository);

            A.CallTo(() => _UserQueryRepository.ExistsAsync(x => x.Username == user.Username)).Returns(false);
            A.CallTo(() => _UserCommandRepository.InsertAsync(user));
            A.CallTo(() => _UnitOfWork.SaveChangesAsync(default)).Returns(true);

            var result = await handler.Handle(command, default);

            Assert.IsType<Guid>(result);

        }
    }
}
