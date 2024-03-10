using Domain.Shared.Constants;
using MassTransit;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;
using Users.IntegrationEvents;

namespace Music.Application.Consumers
{
    public sealed class UserCreatedEventConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly IArtistCommandRepository _ArtistCommandRepository;
        private readonly IMusicUnitOfWork _UnitOfWork;
        public UserCreatedEventConsumer(
            IArtistCommandRepository artistCommandRepository,
            IMusicUnitOfWork unitOfWork)
        {
            _ArtistCommandRepository = artistCommandRepository;
            _UnitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            if (context.Message.Role != (int)UserRole.Artist)
            {
                return;
            }

            Artist artist = Artist.CreateArtist(context.Message.Username, context.Message.UserId);
            await _ArtistCommandRepository.InsertAsync(artist);

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
