using CrudAuth.Enums;

namespace CrudAuth.Models.DTOs
{
    public class UserGetDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
