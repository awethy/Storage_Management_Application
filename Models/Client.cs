using System.ComponentModel.DataAnnotations;

namespace Storage_Management_Application.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
