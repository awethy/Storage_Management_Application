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
    }
}
