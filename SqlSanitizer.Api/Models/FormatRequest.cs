using System.Collections;

namespace SqlSanitizer.Api.Models
{
    public class FormatRequest
    {
        public string SqlQuery { get; set; }
        
        public string[] CharsToRemove { get; set; }
    }
}