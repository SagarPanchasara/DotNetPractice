using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPractice.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "name is required")]
        [StringLength(20)]
        public required string Name { get; set; }

        [Column("id_number")]
        [StringLength(13)]
        public string? IdNumber { get; set; }

        [Column("created_on")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }

    }
}
