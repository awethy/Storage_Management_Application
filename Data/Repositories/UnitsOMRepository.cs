using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Data.Contexts;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Data.Repositories
{
    public class UnitsOMRepository : IUnitsOMRepository
    {
        private readonly AppDbContext context;

        public UnitsOMRepository(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<UnitsOM>> GetAllAsync()
        {
            return await context.UnitsOMs.ToListAsync();
        }

        public async Task<UnitsOM> GetByIdAsync(int id)
        {
            return await context.UnitsOMs.FindAsync(id);
        }

        public async Task<List<UnitsOM>> GetActiveUnitsAsync()
        {
            var allUnits = await GetAllAsync();
            return allUnits.Where(u => u.IsActive).ToList();
        }

        public async Task<List<UnitsOM>> GetInActiveUnitsAsync()
        {
            var allUnits = await GetAllAsync();
            return allUnits.Where(u => !u.IsActive).ToList();
        }

        public async Task CreateAsync(UnitsOM unit)
        {
            context.UnitsOMs.Add(unit);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UnitsOM unit)
        {
            context.UnitsOMs.Update(unit);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var unit = await GetByIdAsync(id);
            if (unit != null)
            {
                context.UnitsOMs.Remove(unit);
                await context.SaveChangesAsync();
            }
        }

        public async Task InActive(int id)
        {
            var unit = await GetByIdAsync(id);
            if (unit != null)
            {
                unit.IsActive = false;
                context.UnitsOMs.Update(unit);
                await context.SaveChangesAsync();
            }
        }

        public async Task Active(int id)
        {
            var unit = await GetByIdAsync(id);
            if (unit != null)
            {
                unit.IsActive = true;
                context.UnitsOMs.Update(unit);
                await context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsUsedInAnyRelationAsync(int id)
        {
            // Проверяем, есть ли связи с другими таблицами
            bool isUsedInReceiptRes = await context.ReceiptResources
                .AnyAsync(p => p.ResourceId == id);

            bool isUsedInBalances = await context.Balances
                .AnyAsync(t => t.ResourceId == id);

            bool isUsedInShipmentRes = await context.ShipmentResources
                .AnyAsync(a => a.ResourceId == id);

            // Если ресурс используется хотя бы в одной из таблиц → вернём true
            return isUsedInReceiptRes || isUsedInBalances || isUsedInShipmentRes;
        }

    }
}
