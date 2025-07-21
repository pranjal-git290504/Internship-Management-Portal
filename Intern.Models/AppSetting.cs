namespace Intern.Models
{
    public class AppSetting
    {
        public string? ConnectionString { get; set; }
        public EmailConfig? EmailConfig { get; set; }
        public JWTSetting? JWTSetting { get; set; }
        public int? SqlCommandTimeout { get; set; }
    }

    public class EmailConfig
    {
        public string? From { get; set; }
        public string? Key { get; set; }
    }
    public class JWTSetting
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
