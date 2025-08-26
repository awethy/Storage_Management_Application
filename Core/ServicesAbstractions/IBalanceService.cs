using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.ServicesAbstractions
{
    public interface IBalanceService
    {
        Task CreateBalance(Balance balance);
        Task PlusToBalance(Balance balance);
        Task PlusToBalances(List<Balance> balances);
        Task RemoveFromBalance(Balance balance);
    }
}
