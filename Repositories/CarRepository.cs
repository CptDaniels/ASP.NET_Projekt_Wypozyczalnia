using ASP.NET_Projekt_Wypozyczalnia.Data;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ASP.NET_Projekt_Wypozyczalnia.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Car>> GetAllAsync()
        {
            return await Task.FromResult(_context.Cars.AsQueryable());
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task AddAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.Cars.Any(c => c.CarID == id);
        }
    }
}