namespace AdminApp.API.Options
{
    public record ConnectionStringsOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";

        public string DefaultConnection { get; init; }
    }
}
