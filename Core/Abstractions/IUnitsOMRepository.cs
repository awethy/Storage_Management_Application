using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.Abstractions
{
    public interface IUnitsOMRepository 
    {
        Task<List<UnitsOM>> GetAllAsync();
        Task<UnitsOM> GetByIdAsync(int id);
        Task<List<UnitsOM>> GetActiveUnitsAsync();
        Task<List<UnitsOM>> GetInActiveUnitsAsync();
        Task CreateAsync(UnitsOM unit);
        Task UpdateAsync(UnitsOM unit);
        Task DeleteAsync(int id);
        Task InActive(int id);
        Task Active(int id);
    }
}
