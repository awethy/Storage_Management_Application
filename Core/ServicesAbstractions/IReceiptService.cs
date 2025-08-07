using Storage_Management_Application.DTO;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.ServicesAbstractions
{
    public interface IReceiptService
    {
        Task<List<ReceiptDocument>> GetReceipts();
        Task UpdateReceipt(ReceiptDocumentDTO receiptDoc, int Id);
        Task<ReceiptDocument> GetReceiptById(int id);
        Task CreateReceiptDoc(ReceiptDocumentDTO receiptDoc);
    }
}
