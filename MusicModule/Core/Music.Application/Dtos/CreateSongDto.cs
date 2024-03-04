using Music.Application.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Dtos
{
    public class CreateSongDto
    {
        public string SongName { get; set; }
        public string SongCategory { get; set; }
        [LyricsPattern]
        public string[] Lyrics { get; set; }
    }
}
