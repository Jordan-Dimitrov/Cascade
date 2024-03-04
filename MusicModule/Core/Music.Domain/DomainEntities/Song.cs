using Music.Domain.ValueObjects;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Music.Domain.Aggregates.AlbumAggregate;

namespace Music.Domain.DomainEntities
{
    public sealed class Song : Entity
    {
        private const string _HiddenName = "Hidden";
        private const string _HiddenFileName = "hidden.ogg";
        private SongName _SongName;
        private AudioFile _AudioFile;
        private DateTime _DateCreated;
        private SongCategory _SongCategory;
        private Song(SongName songName, AudioFile audioFile, DateTime dateCreated, SongCategory songCategory)
        {
            Id = Guid.NewGuid();
            SongName = songName;
            DateCreated = dateCreated;
            AudioFile = audioFile;
            SongCategory = songCategory;
        }

        [JsonConstructor]
        private Song()
        {

        }

        public static Song CreateSong(string songName, string audioName,
            string songCategory, string generated)
        {
            Song song = new Song(new SongName(songName), AudioFile.CreateAudioFile(audioName, generated),
                DateTime.UtcNow, new SongCategory(songCategory));

            return song;
        }

        public void HideSong()
        {
            SongName = new SongName(_HiddenName);
            AudioFile = AudioFile.CreateAudioFile(_HiddenFileName, "hiddengg");
            SongCategory = new SongCategory(_HiddenName);
        }

        public DateTime DateCreated
        {
            get
            {
                return _DateCreated;
            }
            private set
            {
                _DateCreated = value;
            }
        }

        public SongName SongName
        {
            get
            {
                return _SongName;
            }
            private set
            {
                _SongName = value;
            }
        }

        public AudioFile AudioFile
        {
            get
            {
                return _AudioFile;
            }
            set
            {
                _AudioFile = value;
            }
        }

        public SongCategory SongCategory
        {
            get
            {
                return _SongCategory;
            }
            private set
            {
                _SongCategory = value;
            }
        }
    }
}
