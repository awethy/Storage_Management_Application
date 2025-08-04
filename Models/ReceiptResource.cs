using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Storage_Management_Application.Models
{
    public class ReceiptResource
    {
        [Key]
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int UnitsOMId { get; set; }
        public decimal Quantity { get; set; }
        public int ReceiptDocumentId { get; set; }
        [ValidateNever]
        public ReceiptDocument ReceiptDocument { get; set; }
        [ValidateNever]
        public Resource Resource { get; set; }
        [ValidateNever]
        public UnitsOM UnitsOM { get; set; }
    }
}
