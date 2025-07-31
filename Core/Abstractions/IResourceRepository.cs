using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.Abstractions
{
    public interface IResourceRepository
    {
        Task<List<Resource>> GetAllAsync();
        Task<Resource> GetByIdAsync(int id);
        Task<List<Resource>> GetActiveResourcesAsync();
        Task<List<Resource>> GetInActiveResourcesAsync();
        Task CreateAsync(Resource resource);
        Task UpdateAsync(Resource resource);
        Task DeleteAsync(int id);
        Task InActive(int id);
        Task Active(int id);
    }
}
