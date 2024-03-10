﻿using Music.Application.Attributes;

namespace Music.Application.Dtos
{
    public class CreateAlbumDto
    {
        public string AlbumName { get; set; }
        public string SongName { get; set; }
        public string SongCategory { get; set; }
        [LyricsPattern]
        public string[] Lyrics { get; set; }
    }
}
