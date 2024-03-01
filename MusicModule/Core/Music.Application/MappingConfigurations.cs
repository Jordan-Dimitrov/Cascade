using AutoMapper;
using Music.Application.Dtos;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.Aggregates.ListenerAggregate;
using Music.Domain.Aggregates.PlaylistAggregate;
using Music.Domain.Aggregates.SongAggregate;
using Music.Domain.ValueObjects;

namespace Music.Application
{
    public class MappingConfigurations : Profile
    {
        public MappingConfigurations()
        {
            CreateMap<Album, AlbumDto>()
               .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.AlbumName.Value))
               .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<AlbumPatchDto, Album>()
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => new AlbumName(src.AlbumName)))
                .ReverseMap();

            CreateMap<Artist, ArtistDto>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
               .ForMember(dest => dest.FollowCount, opt => opt.MapFrom(src => src.FollowCount))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Listener, ListenerDto>()
              .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Playlist, PlaylistDto>()
              .ForMember(dest => dest.PlaylistName, opt => opt.MapFrom(src => src.PlaylistName.Value))
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<PlaylistPatchDto, Playlist>()
                .ForMember(dest => dest.PlaylistName, opt => opt.MapFrom(src => new AlbumName(src.PlaylistName)))
                .ReverseMap();

            CreateMap<Song, SongDto>()
                .ForMember(dest => dest.SongName, opt => opt.MapFrom(src => src.SongName.Value))
                .ForMember(dest => dest.AudioFile, opt => opt.MapFrom(src => src.AudioFile.Value))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.SongCategory, opt => opt.MapFrom(src => src.SongCategory))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
