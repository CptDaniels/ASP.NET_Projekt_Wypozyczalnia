using ASP.NET_Projekt_Wypozyczalnia.Data;
using ASP.NET_Projekt_Wypozyczalnia.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP.NET_Projekt_Wypozyczalnia.Repositories
{
    public class RentalItemsRepository : IRentalItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public RentalItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<RentalItems>> GetAllAsync()
        {
            return await Task.FromResult(_context.RentalItems.AsQueryable());
        }

        public async Task<RentalItems> GetByIdAsync(int id)
        {
            return await _context.RentalItems.FindAsync(id);
        }

        public async Task AddAsync(RentalItems rentalItem)
        {
            _context.RentalItems.Add(rentalItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RentalItems rentalItem)
        {
            _context.Entry(rentalItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var rentalItem = await _context.RentalItems.FindAsync(id);
            if (rentalItem != null)
            {
                _context.RentalItems.Remove(rentalItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}