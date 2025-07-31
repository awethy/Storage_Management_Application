using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.ServicesAbstractions
{
    public interface IResourceService
    {
        Task<List<Resource>> GetAllResourcesAsync();
        Task<Resource> GetResourceByIdAsync(int id);
        Task<List<Resource>> GetActiveResourcesAsync();
        Task<List<Resource>> GetInActiveResourcesAsync();
        Task CreateResourceAsync(Resource resource);
        Task UpdateResourceAsync(Resource resource);
        Task DeleteResourceAsync(int id);
        Task InActiveResourceAsync(int id);
        Task ActiveResourceAsync(int id);
    }
}
