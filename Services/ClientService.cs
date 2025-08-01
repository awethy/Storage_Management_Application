using Storage_Management_Application.Core.Abstractions;
using Storage_Management_Application.Core.ServicesAbstractions;
using Storage_Management_Application.Models;
using System.Security.AccessControl;

namespace Storage_Management_Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<Client>> GetActiveClients()
        {
            return await _clientRepository.GetActiveClientsAsync();
        }

        public async Task<List<Client>> GetInActiveClients()
        {
            return await _clientRepository.GetInActiveClientAsync();
        }

        public async Task CreateClient(Client client)
        {
            var allClients = await _clientRepository.GetClientsAsync();
            if (allClients.Any(c => c.Name == client.Name))
            {
                throw new InvalidOperationException($"Ресурс с именем '{client.Name}' уже существует.");
            }
            await _clientRepository.CreateAsync(client);
        }

        public async Task InActiveClient(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null)
            {
                throw new InvalidOperationException($"Ресурс с ID '{id}' не найден.");
            }
            client.IsActive = false;
            await _clientRepository.UpdateAsync(client);
        }
    }
}
