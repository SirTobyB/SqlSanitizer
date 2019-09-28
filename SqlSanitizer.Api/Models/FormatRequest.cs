using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SqlSanitizer.Api.Models
{
    public enum Casing
    {
        Default,
        Upper,
        Lower,
        Capitalize
    }
    
    public class FormatRequest
    {
        public string SqlQuery { get; set; }
        public string[] CharsToRemove { get; set; }
        public bool Reindent { get; set; }
        public int IndentWidth { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Casing IdentifierCase { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Casing KeywordCase { get; set; }
        public bool StripComments { get; set; }
        public  SqlParameter[] Parameter { get; set; }
    }
}