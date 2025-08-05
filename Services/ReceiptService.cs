using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Data.Repositories;
using Storage_Management_Application.DTO;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptService(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task<List<ReceiptDocument>> GetReceipts()
        {
            return await _receiptRepository.GetAllReceipt();
        }

        public async Task CreateReceiptDoc(ReceiptDocumentDTO receiptDoc)
        {
            var allReceipts = await _receiptRepository.GetAllReceipt();
            if (allReceipts.Any(r => r.Number == receiptDoc.Number))
            {
                throw new InvalidOperationException($"Поступление с номером '{receiptDoc.Number}' уже существует.");
            }
            else
            {

                var receipt = new ReceiptDocument
                {
                    Number = receiptDoc.Number,
                    Date = receiptDoc.Date = DateTime.SpecifyKind(receiptDoc.Date, DateTimeKind.Utc),
                    ReceiptResources = receiptDoc.ReceiptResources.Select(rr => new ReceiptResource
                    {
                        ResourceId = rr.ResourceId,
                        UnitsOMId = rr.UnitsOMId,
                        Quantity = rr.Quantity
                    }).ToList()
                };
                await _receiptRepository.CreateReceiptDoc(receipt);
            }
        }
    }
}
