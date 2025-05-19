using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Repositories
{
    public interface IRentalItemsRepository
    {
        Task<List<RentalItems>> GetAllAsync();
        Task<RentalItems> GetByIdAsync(int id);
        Task AddAsync(RentalItems rentalItem);
        Task UpdateAsync(RentalItems rentalItem);
        Task DeleteAsync(int id);
    }
}
