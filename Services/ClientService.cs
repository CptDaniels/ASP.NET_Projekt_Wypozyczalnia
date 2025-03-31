using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IQueryable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllClients();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _clientRepository.GetByIdAsync(id);
        }

        public async Task AddClientAsync(Client client)
        {
            await _clientRepository.AddAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }
    }
}
