using Storage_Management_Application.Models;

namespace Storage_Management_Application.Core.ServicesAbstractions
{
    public interface IClientService
    {
        Task<List<Client>> GetActiveClients();
        Task<List<Client>> GetInActiveClients();
        Task CreateClient(Client client);
        Task InActiveClient(int id);
    }
}
