using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Data.Contexts;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Data.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AppDbContext context;

        public ResourceRepository(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            return await context.Resources.ToListAsync();
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
            return await context.Resources.FindAsync(id);
        }

        public async Task<List<Resource>> GetActiveResourcesAsync()
        {
            var allResource = await GetAllAsync();
            return allResource.Where(u => u.IsActive).ToList();
        }

        public async Task<List<Resource>> GetInActiveResourcesAsync()
        {
            var allResource = await GetAllAsync();
            return allResource.Where(u => !u.IsActive).ToList();
        }

        public async Task CreateAsync(Resource resource)
        {
            context.Resources.Add(resource);
            context.SaveChanges();
        }

        public async Task UpdateAsync(Resource resource)
        {
            context.Resources.Update(resource);
            context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await context.Resources.FindAsync(id);
            context.Resources.Remove(resource);
            context.SaveChanges();
        }

        public async Task InActive(int id)
        {
            var resource = await GetByIdAsync(id);
            resource.IsActive = false;
            context.Resources.Update(resource);
            context.SaveChanges();
        }

        public async Task Active(int id)
        {
            var resource = await GetByIdAsync(id);
            resource.IsActive = true;
            context.Resources.Update(resource);
            context.SaveChanges();
        }
    }
}
