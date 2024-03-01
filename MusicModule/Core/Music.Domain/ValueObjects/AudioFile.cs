using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Music.Domain.ValueObjects
{
    public sealed class AudioFile : ValueObject
    {
        private static List<string> _AllowedFormats = new List<string>() { ".mp3", ".ogg" };
        private string _Value = null!;
        public AudioFile(string value)
        {
            Value = value;
        }

        [JsonConstructor]
        private AudioFile()
        {

        }

        public string Value
        {
            get
            {
                return _Value;
            }
            private set
            {
                if (!_AllowedFormats.Contains(Path.GetExtension(value).ToLowerInvariant()))
                {
                    throw new DomainValidationException("Invalid file");
                }

                _Value = value;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _Value;
        }
    }
}
