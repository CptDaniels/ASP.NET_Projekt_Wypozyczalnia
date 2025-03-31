using ASP.NET_Projekt_Wypozyczalnia.Models;

namespace ASP.NET_Projekt_Wypozyczalnia.Services
{
    public interface ICarService
    {
        Task<IQueryable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(int id);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
    }
}
