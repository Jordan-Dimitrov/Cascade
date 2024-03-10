using Domain.Shared.Constants;
using Domain.Shared.Exceptions;
using Domain.Shared.Primitives;
using System.Text.Json.Serialization;

namespace Music.Domain.ValueObjects
{
    public sealed class AudioFile : ValueObject
    {
        private string _Value = null!;
        private static int _ByteCount = 4;
        public AudioFile(string value)
        {
            Value = value;
        }

        public static AudioFile CreateAudioFile(string value, string generated)
        {
            return new AudioFile(GenerateFileName(value, generated));
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

        private static string GenerateFileName(string value, string generated)
        {
            string extension = Path.GetExtension(value);

            if (!SupportedAudioMimeTypes.Types.Contains(extension.ToLowerInvariant()))
            {
                throw new DomainValidationException("Invalid file");
            }

            return $"{value
                .Substring(0, value.Length - extension.Length)}_{generated}{SupportedAudioMimeTypes.Ogg}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _Value;
        }
    }
}
