﻿using System.ComponentModel.DataAnnotations;

namespace Storage_Management_Application.Models
{
    public class ReceiptDocument
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public ICollection<ReceiptResource> ReceiptResources
        {
            get; set;
        }
    }
}
