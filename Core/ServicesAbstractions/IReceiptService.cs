using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.ServicesAbstractions
{
    public interface IReceiptService
    {
        Task<List<ReceiptDocument>> GetReceipts();
        Task CreateReceipt(ReceiptDocument receiptDoc, ReceiptResource receiptRes);
    }
}
