using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Services
{
    public interface IRentalItemsService
    {
        Task<IQueryable<RentalItems>> GetAllRentalItemsAsync();
        Task<RentalItems> GetRentalItemByIdAsync(int id);
        Task AddRentalItemAsync(RentalItems rentalItem);
        Task UpdateRentalItemAsync(RentalItems rentalItem);
        Task DeleteRentalItemAsync(int id);
    }
}