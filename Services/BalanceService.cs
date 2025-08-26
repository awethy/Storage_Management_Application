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

        public async Task<Balance> GetBalanceById(int id)
        {
            try
            {
                return await balanceRepository.GetBalanceByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the balance.", ex);
            }
        }

        public async Task PlusToBalance(Balance balance)
        {
            try
            {
                var existingBalance = await balanceRepository.GetBalanceByResourceAndUnitIdAsync(balance.ResourceId, balance.UnitsOMId);
                
                if (existingBalance != null)
                {
                    existingBalance.Quantity += balance.Quantity;
                    await balanceRepository.UpdateBalance(existingBalance);
                }
                else
                {
                    await CreateBalance(balance);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the balance.", ex);
            }
        }

        public async Task PlusToBalances(List<Balance> balances)
        {
            try
            {
                    await balanceRepository.PlusToBalances(balances);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the balances.", ex);
            }
        }

        public async Task RemoveFromBalance(Balance balance)
        {
            try
            {
                var existingBalance = await balanceRepository.GetBalanceByResourceAndUnitIdAsync(balance.ResourceId, balance.UnitsOMId);
                        
                if (existingBalance != null)
                {
                    existingBalance.Quantity -= balance.Quantity;
                    if (existingBalance.Quantity < 0)
                    {
                        throw new InvalidOperationException("Insufficient balance to perform this operation.");
                    }
                    else if (existingBalance.Quantity == 0)
                    {
                        await balanceRepository.DeleteBalance(existingBalance.Id);
                        return;
                    }
                    else
                    {
                        await balanceRepository.UpdateBalance(existingBalance);
                    }
                }
                else
                {
                    await CreateBalance(balance);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the balance.", ex);
            }
        }

        public async Task CreateBalance(Balance balance)
        {
            await balanceRepository.CreateBalance(balance);
        }
    }
}
