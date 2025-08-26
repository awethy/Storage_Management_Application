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
        private readonly IBalanceService _balanceService;

        public ReceiptService(IReceiptRepository receiptRepository, IBalanceService balanceService)
        {
            _receiptRepository = receiptRepository;
            _balanceService = balanceService;
        }

        public async Task<List<ReceiptDocument>> GetReceipts()
        {
            return await _receiptRepository.GetAllReceipt();
        }

        public async Task<ReceiptDocument> GetReceiptById(int id)
        {
            var receipt = await _receiptRepository.GetReceiptById(id);
            if (receipt == null)
            {
                throw new KeyNotFoundException($"Поступление с ID '{id}' не найдено.");
            }
            return receipt;
        }

        public async Task DeleteReceipt(int id)
        {
            var receipt = await _receiptRepository.GetReceiptById(id);
            if (receipt == null)
            {
                throw new KeyNotFoundException($"Поступление с ID '{id}' не найдено.");
            }
            foreach (var res in receipt.ReceiptResources)
            {
                var balanceToRemove = new Balance
                {
                    ResourceId = res.ResourceId,
                    UnitsOMId = res.UnitsOMId,
                    Quantity = res.Quantity
                };
                await _balanceService.RemoveFromBalance(balanceToRemove);
            }
            await _receiptRepository.DeleteReceipt(id);
        }

        public async Task UpdateReceipt(ReceiptDocumentDTO receiptDoc, int Id)
        {
            var existingReceipt = await _receiptRepository.GetReceiptById(Id);
            if (existingReceipt == null)
            {
                throw new KeyNotFoundException($"Поступление с ID '{Id}' не найдено.");
            }
            // Проверка на уникальность номера
            var allReceipts = await _receiptRepository.GetAllReceipt();
            if (allReceipts.Any(r => r.Number == receiptDoc.Number && r.Id != Id))
            {
                throw new InvalidOperationException($"Поступление с номером '{receiptDoc.Number}' уже существует.");
            }

            foreach (var oldRes in existingReceipt.ReceiptResources)
            {
                var balanceToRemove = new Balance
                {
                    ResourceId = oldRes.ResourceId,
                    UnitsOMId = oldRes.UnitsOMId,
                    Quantity = oldRes.Quantity
                };
                await _balanceService.RemoveFromBalance(balanceToRemove);
            }

            existingReceipt.Number = receiptDoc.Number;
            existingReceipt.Date = DateTime.SpecifyKind(receiptDoc.Date, DateTimeKind.Utc);
            existingReceipt.ReceiptResources = receiptDoc.ReceiptResources.Select(rr => new ReceiptResource
            {
                ResourceId = rr.ResourceId,
                UnitsOMId = rr.UnitsOMId,
                Quantity = rr.Quantity
            }).ToList();
            await _receiptRepository.UpdateReceipt(existingReceipt);

            var balancesToUpdate = receiptDoc.ReceiptResources
            .GroupBy(r => new { r.ResourceId, r.UnitsOMId })
            .Select(g => new Balance
            {
                ResourceId = g.Key.ResourceId,
                UnitsOMId = g.Key.UnitsOMId,
                Quantity = g.Sum(x => x.Quantity)
            })
            .ToList();

            await _balanceService.PlusToBalances(balancesToUpdate);
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
