using Application.Shared.CustomExceptions;
using Domain.Shared.Constants;
using MassTransit;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.DomainServices;
using System.Net;
using Users.IntegrationEvents;

namespace Music.Application.Consumers
{
    public sealed class UserHiddenEventConsumer : IConsumer<UserHiddenIntegrationEvent>
    {
        private readonly ArtistService _ArtistService;
        private readonly IMusicUnitOfWork _UnitOfWork;
        private readonly IArtistQueryRepository _ArtistQueryRepository;

        public UserHiddenEventConsumer(IMusicUnitOfWork musicUnitOfWork,
            IArtistQueryRepository artistQueryRepository,
            ArtistService artistService)
        {
            _UnitOfWork = musicUnitOfWork;
            _ArtistQueryRepository = artistQueryRepository;
            _ArtistService = artistService;
        }
        public async Task Consume(ConsumeContext<UserHiddenIntegrationEvent> context)
        {
            if (context.Message.Role != (int)UserRole.Artist)
            {
                return;
            }

            Artist? artist = await _ArtistQueryRepository.GetByIdAsync(context.Message.UserId, true);

            if (artist is null)
            {
                throw new AppException("No such artist exists!", HttpStatusCode.NotFound);
            }

            await _ArtistService.HideArtist(artist);

            if (!await _UnitOfWork.SaveChangesAsync())
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
