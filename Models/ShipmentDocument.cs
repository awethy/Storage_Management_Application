using System.ComponentModel.DataAnnotations;

namespace Storage_Management_Application.Models
{
    public class ShipmentDocument
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsSigned { get; set; } = false;
        public Client Client { get; set; }
    }
}
