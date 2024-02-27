using Domain.Shared.Constants;
using MassTransit;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.ListenerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.IntegrationEvents;

namespace Music.Application.Consumers
{
    public sealed class UserHiddenEventConsumer : IConsumer<UserHiddenIntegrationEvent>
    {
        private readonly IArtistCommandRepository _ArtistCommandRepository;
        private readonly IListenerCommandRepository _ListenerCommandRepository;
        private readonly IMusicUnitOfWork _UnitOfWork;
        private readonly IArtistQueryRepository _ArtistQueryRepository;
        private readonly IListenerQueryRepository _ListenerQueryRepository;

        public UserHiddenEventConsumer(IArtistCommandRepository artistCommandRepository,
            IListenerCommandRepository listenerCommandRepository,
            IMusicUnitOfWork musicUnitOfWork,
            IArtistQueryRepository artistQueryRepository,
            IListenerQueryRepository listenerQueryRepository)
        {
            _ArtistCommandRepository = artistCommandRepository;
            _ListenerCommandRepository = listenerCommandRepository;
            _UnitOfWork = musicUnitOfWork;
            _ArtistQueryRepository = artistQueryRepository;
            _ListenerQueryRepository = listenerQueryRepository;
        }
        public async Task Consume(ConsumeContext<UserHiddenIntegrationEvent> context)
        {
            int role = context.Message.Role;

            if(role == (int)UserRole.Artist)
            {
                Artist? artist = await _ArtistQueryRepository.GetByIdAsync(context.Message.UserId, true);

                if(artist is null)
                {
                    throw new ApplicationException("Such artist does not exist");
                }

                artist.HideArtist();

                await _ArtistCommandRepository.UpdateAsync(artist);
            }
            else if(role == (int)UserRole.User)
            {
                Listener? listener = await _ListenerQueryRepository.GetByIdAsync(context.Message.UserId, true);

                if (listener is null)
                {
                    throw new ApplicationException("Such listener does not exist");
                }

                listener.HideListener();

                await _ListenerCommandRepository.UpdateAsync(listener);
            }
            else
            {
                return;
            }

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }
        }
    }
}
