using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.Abstractions
{
    public interface IReceiptRepository
    {
        Task<List<ReceiptDocument>> GetAllReceipt();
        Task CreateReceipt(ReceiptDocument receiptDoc, ReceiptResource receiptRes);
    }
}
