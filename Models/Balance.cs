using System.ComponentModel.DataAnnotations;

namespace Storage_Management_Application.Models
{
    public class Balance
    {
        [Key]
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int UnitsOMId { get; set; }
        public decimal Quantity { get; set; }
        public Resource Resource { get; set; }
        public UnitsOM UnitsOM { get; set; }
    }
}
