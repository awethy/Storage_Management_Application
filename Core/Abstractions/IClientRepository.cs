using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.Abstractions
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<List<Client>> GetActiveClientsAsync();
        Task<List<Client>> GetInActiveClientAsync();
        Task CreateAsync(Client client);
        Task UpdateAsync(Client client);
    }
}
