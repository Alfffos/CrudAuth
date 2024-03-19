using System.ComponentModel.DataAnnotations;

namespace Agenda_api.Models.DTOs
{
    public class AutenticationRequestBody
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
