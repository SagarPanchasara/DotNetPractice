using System.ComponentModel.DataAnnotations;

namespace DotNetPractice.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? IdNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
