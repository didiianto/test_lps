using Microsoft.EntityFrameworkCore;
using Perpustakaan.Models;

namespace Perpustakaan.Data.Repository
{
    public interface IBukuRepository : IGenericRepository<Buku>
    {
        Task<List<Buku>> Search(string keyword);
    }

    public class BukuRepository : GenericRepository<Buku>, IBukuRepository
    {
        private readonly AppDbContext _db;
        public BukuRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Buku>> Search(string keyword)
        {
            return await _db.Buku
                .Include(j => j.JenisBuku)
                .Where(b => b.Judul.Contains(keyword)).ToListAsync();
        }
    }
    
}
