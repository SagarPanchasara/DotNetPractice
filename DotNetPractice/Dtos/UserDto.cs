using System.ComponentModel.DataAnnotations;

namespace DotNetPractice.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [RegularExpression(@"^\d{13}$")]
        public string? IdNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
