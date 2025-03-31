using ASP.NET_Projekt_Wypozyczalnia.Models;
using ASP.NET_Projekt_Wypozyczalnia.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Services
{
    public class RentalItemsService : IRentalItemsService
    {
        private readonly IRentalItemsRepository _rentalItemsRepository;

        public RentalItemsService(IRentalItemsRepository rentalItemsRepository)
        {
            _rentalItemsRepository = rentalItemsRepository;
        }

        public async Task<IQueryable<RentalItems>> GetAllRentalItemsAsync()
        {
            return await _rentalItemsRepository.GetAllAsync();
        }

        public async Task<RentalItems> GetRentalItemByIdAsync(int id)
        {
            return await _rentalItemsRepository.GetByIdAsync(id);
        }

        public async Task AddRentalItemAsync(RentalItems rentalItem)
        {
            await _rentalItemsRepository.AddAsync(rentalItem);
        }

        public async Task UpdateRentalItemAsync(RentalItems rentalItem)
        {
            await _rentalItemsRepository.UpdateAsync(rentalItem);
        }

        public async Task DeleteRentalItemAsync(int id)
        {
            await _rentalItemsRepository.DeleteAsync(id);
        }
    }
}