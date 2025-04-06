using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Services
{
    public interface IClientService
    {
        Task<IQueryable<Client>> GetAllClientsAsync(int pageNumber = 1, int pageSize = 10);
        Task<Client> GetClientByIdAsync(int id);
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(int id);
    }
}
