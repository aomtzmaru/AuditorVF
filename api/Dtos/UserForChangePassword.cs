namespace api.Dtos
{
    public class UserForChangePassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}