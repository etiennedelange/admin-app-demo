namespace AdminApp.API.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}