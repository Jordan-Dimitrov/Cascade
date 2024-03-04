using AutoMapper;
using Music.Application.Dtos;
using Music.Domain.Aggregates.AlbumAggregate;
using Music.Domain.Aggregates.ArtistAggregate;
using Music.Domain.DomainEntities;
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
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<Song, SongDto>()
                .ForMember(dest => dest.SongName, opt => opt.MapFrom(src => src.SongName.Value))
                .ForMember(dest => dest.AudioFile, opt => opt.MapFrom(src => src.AudioFile.Value))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.SongCategory, opt => opt.MapFrom(src => src.SongCategory.Value))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Album, AlbumWithSongsDto>()
                .ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.AlbumName.Value))
               .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Songs, opt => opt.MapFrom(src => src.Songs))
               .ReverseMap();


        }
    }
}
