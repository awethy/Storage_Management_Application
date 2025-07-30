using System.ComponentModel.DataAnnotations;

namespace Storage_Management_Application.Models
{
    public class UnitsOM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
