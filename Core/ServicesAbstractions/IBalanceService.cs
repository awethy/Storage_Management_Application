using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.ServicesAbstractions
{
    public interface IBalanceService
    {
        Task CreateBalance(Balance balance);
    }
}
