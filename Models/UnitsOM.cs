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

        public ICollection<Balance> Balances
        {
            get; set;
        }
        public ICollection<ReceiptResource> ReceiptResources
        {
            get; set;
        }
        public ICollection<ShipmentResource> ShipmentResources
        {
            get; set;
        }
    }
}
