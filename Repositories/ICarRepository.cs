using ASP.NET_Projekt_Wypozyczalnia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(int id);
    }
}
