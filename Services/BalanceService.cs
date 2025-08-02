using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository balanceRepository;

        public BalanceService (IBalanceRepository balanceRepository)
        {
            this.balanceRepository = balanceRepository;
        }

        public async Task CreateBalance(Balance balance)
        {
            var allBalances = await balanceRepository.GetAllBalancesAsync();
            var existingBalance = allBalances.FirstOrDefault(b => b.ResourceId == balance.ResourceId && b.UnitsOMId == balance.UnitsOMId);

            if (existingBalance != null)
            {
                existingBalance.Quantity = existingBalance.Quantity + balance.Quantity;
                await balanceRepository.UpdateBalance(existingBalance);
            }
            else
            {
                await balanceRepository.CreateBalance(balance);
            }
        }
    }
}
