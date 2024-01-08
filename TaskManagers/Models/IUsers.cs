using System.Security.Claims;

namespace TaskManagers.Models
{
    public interface IUsers
    {
        public c
        public string GetUserId(ClaimsPrincipal user);
        public IEnumerable<string> GetUserRole (ClaimsPrincipal user);
    }

    public class UserContext : IUsers
    {
        public List<User> users = new List<User>
        {
            new User {User_Id = 1, Name = "thameem" , Password = "123456789" , Role = Role.Admin  },
            new User {User_Id = 2, Name = "ajmal" , Password = "123456789", Role = Role.User}
        };
        public  
        public string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public IEnumerable<string> GetUserRole(ClaimsPrincipal user)
        {
            return user.FindAll(ClaimTypes.Role)?.Select(c => c.Value);
        }
    }
}
