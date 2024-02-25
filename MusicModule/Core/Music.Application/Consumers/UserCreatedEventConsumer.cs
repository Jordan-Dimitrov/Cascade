using Domain.Shared.Abstractions;
using Domain.Shared.Constants;
using MassTransit;
using Music.Application.Abstractions;
using Music.Domain.Abstractions;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.ListenerAggregate;
using Music.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.IntegrationEvents;

namespace Music.Application.Consumers
{
    public sealed class UserCreatedEventConsumer : IConsumer<UserCreatedIntegrationEvent>
    {
        private readonly IArtistCommandRepository _ArtistCommandRepository;
        private readonly IListenerCommandRepository _ListenerCommandRepository;
        private readonly IMusicUnitOfWork _UnitOfWork;
        public UserCreatedEventConsumer(IListenerCommandRepository listenerCommandRepository,
            IArtistCommandRepository artistCommandRepository,
            IMusicUnitOfWork unitOfWork)
        {
            _ListenerCommandRepository = listenerCommandRepository;
            _ArtistCommandRepository = artistCommandRepository;
            _UnitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
        {
            if(context.Message.Role == (int)UserRole.Artist)
            {
                Artist artist = Artist.CreateArtist(context.Message.UserId, context.Message.Username);
                await _ArtistCommandRepository.InsertAsync(artist);
            }
            else if(context.Message.Role == (int)UserRole.User)
            {
                Listener listener = Listener.CreateListener(context.Message.Username, context.Message.UserId);
                await _ListenerCommandRepository.InsertAsync(listener);
            }

            if (await _UnitOfWork.SaveChangesAsync() <= 0)
            {
                throw new ApplicationException("Unexpected error");
            }

            Console.WriteLine("111111111aaaaaa");
        }
    }
}
