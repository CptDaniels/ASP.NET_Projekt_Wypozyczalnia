using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Services
{
    public interface IClientService
    {
        Task<(List<Client> Clients, int TotalCount)> GetAllClientsAsync(int pageNumber, int pageSize);
        Task<Client> GetClientByIdAsync(int id);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
    }
}
