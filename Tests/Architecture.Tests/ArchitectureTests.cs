using NetArchTest.Rules;

namespace Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string _MusicDomainNamespace = "Music.Domain";
        private const string _MusicApplicationNamespace = "Music.Application";
        private const string _MusicInfrastructureNamespace = "Music.Infrastructure";
        private const string _MusicPresentationNamespace = "Music.Presentation";
        private const string _MusicPersistenceNamespace = "Music.Persistence";
        private const string _MusicIntegrationEventsNamespace = "Music.IntegrationEvents";

        private const string _UsersDomainNamespace = "Users.Domain";
        private const string _UsersApplicationNamespace = "Users.Application";
        private const string _UsersInfrastructureNamespace = "Users.Infrastructure";
        private const string _UsersPresentationNamespace = "Users.Presentation";
        private const string _UsersPersistenceNamespace = "Users.Persistence";
        private const string _UsersIntegrationEventsNamespace = "Users.IntegrationEvents";

        private const string _WebNamespace = "Web";
        private const string _HandlerNameConvention = "Handler";

        [Fact]
        public void MusicDomain_Should_Not_HaveDependenciesOnOtherProjects()
        {
            var assembly = typeof(Music.Domain.AssemblyRefrence).Assembly;

            var otherProjects = new[]
            {
                _MusicApplicationNamespace,
                _MusicInfrastructureNamespace,
                _MusicPresentationNamespace,
                _MusicPersistenceNamespace,
                _UsersDomainNamespace,
                _UsersApplicationNamespace,
                _UsersPresentationNamespace,
                _UsersPersistenceNamespace,
                _UsersInfrastructureNamespace,
                _MusicIntegrationEventsNamespace,
                _UsersIntegrationEventsNamespace,
                _WebNamespace
            };

            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void MusicHandlers_Should_Have_DependencyOnDomain()
        {
            var assembly = typeof(Music.Application.AssemblyRefrence).Assembly;

            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith(_HandlerNameConvention)
                .Should()
                .HaveDependencyOn(_MusicDomainNamespace)
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        [Fact]
        public void MusicApplication_Should_Not_HaveDependenciesOnOtherProjects()
        {
            var assembly = typeof(Music.Domain.AssemblyRefrence).Assembly;

            var otherProjects = new[]
            {
                _MusicInfrastructureNamespace,
                _MusicPresentationNamespace,
                _MusicPersistenceNamespace,
                _UsersDomainNamespace,
                _UsersApplicationNamespace,
                _UsersPresentationNamespace,
                _UsersPersistenceNamespace,
                _UsersInfrastructureNamespace,
                _WebNamespace
            };

            var result = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            Assert.True(result.IsSuccessful);
        }
    }
}