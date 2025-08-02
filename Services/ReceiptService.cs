using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IBalanceRepository _balanceRepository;

        public ReceiptService(IReceiptRepository receiptRepository, IBalanceRepository balanceRepository)
        {
            _receiptRepository = receiptRepository;
            _balanceRepository = balanceRepository;
        }

        public async Task<List<ReceiptDocument>> GetReceipts()
        {
            return await _receiptRepository.GetAllReceipt();
        }

        public async Task CreateReceipt(ReceiptDocument receiptDoc, ReceiptResource receiptRes)
        {
            var allReceipts = await _receiptRepository.GetAllReceipt();
            if (allReceipts.Any(r => r.Number == receiptDoc.Number))
            {
                throw new InvalidOperationException($"Поступление с номером '{receiptDoc.Number}' уже существует.");
            }
            else
            {
                await _receiptRepository.CreateReceipt(receiptDoc, receiptRes);
                var newBalance = new Balance
                {
                    ResourceId = receiptRes.ResourceId,
                    UnitsOMId = receiptRes.UnitsOMId,
                    Quantity = receiptRes.Quantity
                };
                await _balanceRepository.CreateBalance(newBalance);
            }
        }
    }
}
