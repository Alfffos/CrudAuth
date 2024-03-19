using CrudAuth.Enums;

namespace CrudAuth.Models.DTOs
{
    public class UserCreateDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
