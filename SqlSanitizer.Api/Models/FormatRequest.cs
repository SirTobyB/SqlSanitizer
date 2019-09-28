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
        public Casing IdentifierCase { get; set; }
        public Casing KeywordCase { get; set; }
        public bool StripComments { get; set; }
        public  SqlParameter[] Parameter { get; set; }
    }
}