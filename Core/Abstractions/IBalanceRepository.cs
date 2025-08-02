using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.Abstractions
{
    public interface IBalanceRepository
    {
        Task<List<Balance>> GetAllBalancesAsync();
        Task<List<Balance>> GetBalanceByResourceNameAsync(string resourceName);
        Task<List<Balance>> GetBalanceByUnitNameAsync(string unitName);
        Task CreateBalance(Balance balance);
        Task UpdateBalance(Balance balance);
    }
}
