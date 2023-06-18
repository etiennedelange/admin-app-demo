namespace AdminApp.API.Options
{
    public record AuthenticationOptions
    {
        public const string Authentication = "Authentication";

        public string Secret { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int TokenExpiryMinutes { get; init; }
    }
}
