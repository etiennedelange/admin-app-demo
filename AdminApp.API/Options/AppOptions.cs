namespace AdminApp.API.Options
{
    public record AppOptions
    {
        public const string Name = "App";

        public AuthenticationOptions AuthenticationOptions { get; init; }
        public EmailOptions Email { get; init; }
    }
}
