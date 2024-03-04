using Domain.Shared.Constants;
using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public static AudioFile CreateAudioFile(string value)
        {
            return new AudioFile(GenerateFileName(value));
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
                _Value = value;
            }
        }

        private static string GenerateFileName(string value)
        {
            string extension = Path.GetExtension(value);

            if (!SupportedAudioMimeTypes.Types.Contains(extension.ToLowerInvariant()))
            {
                throw new DomainValidationException("Invalid file");
            }

            return $"{value
                .Substring(0, value.Length - extension.Length)}_{Convert
                .ToHexString(RandomNumberGenerator.GetBytes(4))}{SupportedAudioMimeTypes.Types[1]}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _Value;
        }
    }
}
