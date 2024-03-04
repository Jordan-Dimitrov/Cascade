using Domain.Shared.Constants;
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
                string extension = Path.GetExtension(value);

                if (!SupportedAudioMimeTypes.Types.Contains(extension.ToLowerInvariant()))
                {
                    throw new DomainValidationException("Invalid file");
                }

                _Value = $"{value
                    .Substring(0, value.Length - extension.Length)}_{Guid.NewGuid()}{SupportedAudioMimeTypes.Types[1]}";
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _Value;
        }
    }
}
