using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Data.Contexts;
using Storage_Management_Application.Models;
using System.Security.Cryptography.X509Certificates;

namespace Storage_Management_Application.Data.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;
        public ReceiptRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<ReceiptDocument>> GetAllReceipt()
        {
            return await _context.ReceiptDocuments
                .Include(r => r.ReceiptResources)
                .ToListAsync();
        }

        public async Task CreateReceiptDoc(ReceiptDocument receiptDoc)
        {
            _context.ReceiptDocuments.Add(receiptDoc);
            await _context.SaveChangesAsync();
        }
        
        public async Task CreateReceipRes(ReceiptResource receiptRes)
        {
            _context.ReceiptResources.Add(receiptRes);
            await _context.SaveChangesAsync();
        }
    }
}
