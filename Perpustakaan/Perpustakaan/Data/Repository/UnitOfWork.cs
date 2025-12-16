using Microsoft.EntityFrameworkCore.Storage;
using Perpustakaan.Models;

namespace Perpustakaan.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<MasterJenisBuku> MasterJenisBuku { get; }
        IBukuRepository Buku { get; }
        IGenericRepository<HistoryBuku> HistoryBuku { get; }
        Task<int> SaveAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        private IGenericRepository<MasterJenisBuku> _masterJenisBuku;
        private IBukuRepository _buku;
        private IGenericRepository<HistoryBuku> _historyBuku;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _masterJenisBuku = new GenericRepository<MasterJenisBuku>(_context);
            _buku = new BukuRepository(_context);
            _historyBuku = new GenericRepository<HistoryBuku>(_context);
        }

        public IGenericRepository<MasterJenisBuku> MasterJenisBuku
            => _masterJenisBuku ??= new GenericRepository<MasterJenisBuku>(_context);

        public IBukuRepository Buku
            => _buku ??= new BukuRepository(_context);

        public IGenericRepository<HistoryBuku> HistoryBuku
            => _historyBuku ??= new GenericRepository<HistoryBuku>(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // ================= TRANSACTION =================

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction!.CommitAsync();
            }
            finally
            {
                await _transaction!.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _transaction!.RollbackAsync();
            }
            finally
            {
                await _transaction!.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }

}
