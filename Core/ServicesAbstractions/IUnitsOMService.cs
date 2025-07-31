using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.ServicesAbstractions
{
    public interface IUnitsOMService
    {
        Task<List<UnitsOM>> GetAllUnitsAsync();
        Task<UnitsOM> GetUnitByIdAsync(int id);
        Task<List<UnitsOM>> GetActiveUnitsAsync();
        Task<List<UnitsOM>> GetInActiveUnitsAsync();
        Task CreateUnitAsync(UnitsOM unit);
        Task UpdateUnitAsync(UnitsOM unit);
        Task DeleteUnitAsync(int id);
        Task InActiveUnit(int id);
        Task ActiveUnit(int id);
    }
}
