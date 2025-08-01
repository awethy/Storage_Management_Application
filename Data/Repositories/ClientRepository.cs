using Microsoft.EntityFrameworkCore;
using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Data.Contexts;
using Storage_Management_Application.Models;

namespace Storage_Management_Application.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext context;

        public ClientRepository (AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await context.Clients.FindAsync(id);
        }

        public async Task<List<Client>> GetActiveClientsAsync()
        {
            var allClients = await GetClientsAsync();
            return allClients.Where(c => c.IsActive).ToList();
        }

        public async Task<List<Client>> GetInActiveClientAsync()
        {
            var allClients = await GetClientsAsync();
            return allClients.Where(c => !c.IsActive).ToList();
        }

        public async Task CreateAsync(Client client)
        {
            context.Clients.Add(client);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            context.Clients.Update(client);
            await context.SaveChangesAsync();
        }
    }
}
