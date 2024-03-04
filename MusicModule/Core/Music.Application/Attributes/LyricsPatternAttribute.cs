using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class LyricsPatternAttribute : ValidationAttribute
    {
        private readonly string _Pattern = @"\[\d{2}:\d{2}\.\d{1,2}\].*";

        public LyricsPatternAttribute()
        {
            ErrorMessage = "Lyrics must follow the pattern '[mm:ss.xx]Lyrics'.";
        }

        public override bool IsValid(object value)
        {
            if (value == null || !(value is string[] lyrics))
            {
                return false;
            }

            return lyrics.All(lyric => System.Text.RegularExpressions.Regex.IsMatch(lyric, _Pattern));
        }
    }
}
