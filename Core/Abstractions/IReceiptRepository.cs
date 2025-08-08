using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.Abstractions
{
    public interface IReceiptRepository
    {
        Task<List<ReceiptDocument>> GetAllReceipt();
        Task<ReceiptDocument> GetReceiptById(int id);
        Task DeleteReceipt(int id);
        Task UpdateReceipt(ReceiptDocument receipt);
        Task CreateReceiptDoc(ReceiptDocument receiptDoc);
        Task CreateReceipRes(ReceiptResource receiptRes);
    }
}
