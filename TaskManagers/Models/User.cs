namespace TaskManagers.Models
{
    public class User
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Login
    {
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
