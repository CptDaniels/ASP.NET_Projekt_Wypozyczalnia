using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
    }
}
