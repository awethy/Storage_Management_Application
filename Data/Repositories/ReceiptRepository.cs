using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Data.Contexts;
using Storage_Management_Application.Models;
using System.Security.Cryptography.X509Certificates;

namespace Storage_Management_Application.Data.Repositories
{
    public class ReceiptRepository
    {
        private readonly AppDbContext _context;
        public ReceiptRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<ReceiptDocument>> GetAllReceipt()
        {
            return await _context.ReceiptDocuments
                .Include(r => r.ReceiptResource)
                .ToListAsync();
        }
    }
}
