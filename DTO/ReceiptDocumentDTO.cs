namespace Storage_Management_Application.DTO
{
    public class ReceiptDocumentDTO
    {
        public int Number { get; set; }
        [DateValidation(ErrorMessage = "Дата должна быть от 2000 года до сегодняшнего дня")]
        public DateTime Date { get; set; }
        public List<ReceiptResourceDTO> ReceiptResources { get; set; } = new();
    }
}
