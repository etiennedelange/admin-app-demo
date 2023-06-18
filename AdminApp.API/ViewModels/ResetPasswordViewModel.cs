namespace AdminApp.API.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}