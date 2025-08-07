using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public Task<List<Resource>> GetAllResourcesAsync()
        {
            return _resourceRepository.GetAllAsync();
        }

        public Task<Resource> GetResourceByIdAsync(int id)
        {
            return _resourceRepository.GetByIdAsync(id);
        }

        public Task<List<Resource>> GetActiveResourcesAsync()
        {
            return _resourceRepository.GetActiveResourcesAsync();
        }

        public Task<List<Resource>> GetInActiveResourcesAsync()
        {
            return _resourceRepository.GetInActiveResourcesAsync();
        }

        public async Task CreateResourceAsync(Resource resource)
        {
            // Проверка на уникальность имени
            var allResources = await _resourceRepository.GetAllAsync();
            if (allResources.Any(r => r.Name == resource.Name))
            {
                throw new InvalidOperationException($"Ресурс с именем '{resource.Name}' уже существует.");
            }
            await _resourceRepository.CreateAsync(resource);
        }

        public async Task UpdateResourceAsync(Resource resource)
        {
            // Проверка на уникальность имени
            var allResources = await _resourceRepository.GetAllAsync();
            if (allResources.Any(r => r.Name == resource.Name && r.Id != resource.Id))
            {
                throw new InvalidOperationException($"Ресурс с именем '{resource.Name}' уже существует.");
            }
            await _resourceRepository.UpdateAsync(resource);
        }

        public async Task DeleteResourceAsync(int id)
        {
            // Проверка на существование ресурса
            var resource = await _resourceRepository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new InvalidOperationException($"Ресурс с ID '{id}' не найден.");
            }
            // Проверка
            bool isUsed = await _resourceRepository.IsUsedInAnyRelationAsync(id);
            if (isUsed)
                throw new InvalidOperationException($"Ресурс '{resource.Name}' связан с другими данными.");
            await _resourceRepository.DeleteAsync(id);
        }

        public async Task InActiveResourceAsync(int id)
        {
            // Проверка на существование ресурса
            var resource = await _resourceRepository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new InvalidOperationException($"Ресурс с ID '{id}' не найден.");
            }
            await _resourceRepository.InActive(id);
        }

        public async Task ActiveResourceAsync(int id)
        {
            // Проверка на существование ресурса
            var resource = await _resourceRepository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new InvalidOperationException($"Ресурс с ID '{id}' не найден.");
            }
            await _resourceRepository.Active(id);
        }
    }
}
