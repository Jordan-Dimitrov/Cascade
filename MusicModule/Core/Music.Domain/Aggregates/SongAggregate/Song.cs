using Music.Domain.ValueObjects;
using Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.Aggregates.SongAggregate
{
    public sealed class Song : AggregateRoot
    {
        private SongName _SongName;
        private AudioFile _AudioFile;
        private DateTime _DateCreated;
        private Guid _UserId;
        private SongCategory _SongCategory;
        private Song(SongName songName, AudioFile audioFile, DateTime dateCreated, Guid userId, SongCategory songCategory)
        {
            Id = Guid.NewGuid();
            SongName = songName;
            DateCreated = dateCreated;
            UserId = userId;
            SongCategory = songCategory;
        }

        [JsonConstructor]
        private Song()
        {

        }

        public static Song CreateSong(string songName, string audioName, Guid userId, string songCategory)
        {
            Song song = new Song(new SongName(songName), new AudioFile(audioName), DateTime.UtcNow, userId , new SongCategory(songCategory));

            return song;
        }

        public void DeleteSong()
        {

        }

        public void HideSong()
        {

        }

        public Guid UserId 
        {
            get 
            {
                return _UserId; 
            }
            private set
            {
                _UserId = value;
            }
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
