using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Data.Contexts;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Data.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly AppDbContext context;

        public BalanceRepository(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Balance>> GetAllBalancesAsync()
        {
            return await context.Balances
                .Include(b => b.Resource)
                .Include(b => b.UnitsOM)
                .ToListAsync();
        }

        public async Task<Balance> GetBalanceByResourceAndUnitIdAsync(int resourceId, int unitId)
        {
            return await context.Balances
                .Include(b => b.Resource)
                .Include(b => b.UnitsOM)
                .FirstOrDefaultAsync(b => b.ResourceId == resourceId && b.UnitsOMId == unitId);
        }

        public async Task<Balance> GetBalanceByIdAsync(int id)
        {
            return await context.Balances
                .Include(b => b.Resource)
                .Include(b => b.UnitsOM)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Balance>> GetBalanceByResourceNameAsync(string resourceName)
        {
            return await context.Balances
                .Include(b => b.Resource)
                .Include(b => b.UnitsOM)
                .Where(b => b.Resource.Name.Contains(resourceName))
                .ToListAsync();
        }

        public async Task<List<Balance>> GetBalanceByUnitNameAsync(string unitName)
        {
            return await context.Balances
                .Include(b => b.Resource)
                .Include(b => b.UnitsOM)
                .Where(b => b.UnitsOM.Name == unitName)
                .ToListAsync();
        }

        public async Task DeleteBalance(int id)
        {
            var balance = await context.Balances.FindAsync(id);
            if (balance != null)
            {
                context.Balances.Remove(balance);
                await context.SaveChangesAsync();
            }
        }   

        public async Task CreateBalance(Balance balance)
        {
            context.Balances.Add(balance);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBalance(Balance balance)
        {
            context.Balances.Update(balance);
            await context.SaveChangesAsync();
        }

        public async Task PlusToBalances(List<Balance> balances)
        {
            var keys = balances.Select(b => new { b.ResourceId, b.UnitsOMId }).ToList();

            var existingBalances = await context.Balances
                .Where(b => keys.Any(k => k.ResourceId == b.ResourceId && k.UnitsOMId == b.UnitsOMId))
                .ToListAsync();

            foreach (var balance in balances)
            {
                var existingBalance = existingBalances
                    .FirstOrDefault(b => b.ResourceId == balance.ResourceId && b.UnitsOMId == balance.UnitsOMId);

                if (existingBalance != null)
                {
                    existingBalance.Quantity += balance.Quantity;
                }
                else
                {
                    context.Balances.Add(balance);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
