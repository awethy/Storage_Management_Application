using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.Abstractions
{
    public interface IBalanceRepository
    {
        Task<List<Balance>> GetAllBalancesAsync();
        Task<Balance> GetBalanceByIdAsync(int id);
        Task<List<Balance>> GetBalanceByResourceNameAsync(string resourceName);
        Task<List<Balance>> GetBalanceByUnitNameAsync(string unitName);
        Task DeleteBalance(int id);
        Task CreateBalance(Balance balance);
        Task UpdateBalance(Balance balance);
    }
}
