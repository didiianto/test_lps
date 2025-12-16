using Microsoft.AspNetCore.Mvc;
using Perpustakaan.Data.Repository;
using Perpustakaan.Models;
using Perpustakaan.Data;

namespace Perpustakaan.Controllers
{
    public class BukuController : Controller
    {
        private readonly IBukuRepository _repo;
        private readonly IGenericRepository<MasterJenisBuku> _masterJenisBukuRepository;
        private readonly AppDbContext _appContext;
        private readonly IUnitOfWork _uow;

        public BukuController(IBukuRepository repo,
            IGenericRepository<Models.MasterJenisBuku> masterJenisBukuRepository,
            AppDbContext appContext,
            IUnitOfWork uow)
        {
            _repo = repo;
            _masterJenisBukuRepository = masterJenisBukuRepository;
            _appContext = appContext;
            _uow = uow;
        }

        public async Task<IActionResult> Index(string search)
        {
            var data = string.IsNullOrEmpty(search) ? await _uow.Buku.GetAllAsync(b => b.JenisBuku)
                : await _uow.Buku.Search(search);

            return View(data);
        }

        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Jenis = await _uow.MasterJenisBuku.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Buku model)
        {
            await _uow.BeginTransactionAsync();

            try
            {
                await _uow.Buku.AddAsync(model);
                await _uow.SaveAsync();

                await _uow.HistoryBuku.AddAsync(new HistoryBuku
                {
                    BukuId = model.Id,
                    Aksi = "Create",
                    Waktu = DateTime.Now,
                    DataSesudah = System.Text.Json.JsonSerializer.Serialize(model)
                });

                await _uow.SaveAsync();

                await _uow.CommitAsync(); // ✅ COMMIT

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                await _uow.RollbackAsync(); // ❌ ROLLBACK
                throw; // atau return error view
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            var buku = await _uow.Buku.GetByIdAsync(id);
            if (buku == null) return NotFound();

            ViewBag.Jenis = await _uow.MasterJenisBuku.GetAllAsync();
            return View(buku);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Buku model)
        {
            // ambil entity lama (sudah tracked)
            var oldData = await _uow.Buku.GetByIdAsync(model.Id);
            if (oldData == null) return NotFound();

            // simpan history sebelum update
            await _uow.HistoryBuku.AddAsync(new HistoryBuku
            {
                BukuId = model.Id,
                Aksi = "Edit",
                Waktu = DateTime.Now,
                DataSebelum = System.Text.Json.JsonSerializer.Serialize(oldData),
                DataSesudah = System.Text.Json.JsonSerializer.Serialize(model)
            });

            // update field-field entity yang sudah tracked
            oldData.Judul = model.Judul;
            oldData.Penulis = model.Penulis;
            oldData.JenisBukuId = model.JenisBukuId;
            oldData.TanggalRilis = model.TanggalRilis;
            oldData.JumlahHalaman = model.JumlahHalaman;

            await _uow.SaveAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var buku = await _uow.Buku.GetByIdAsync(id);
            if (buku == null) return NotFound();

            // simpan history sebelum delete
            await _uow.HistoryBuku.AddAsync(new HistoryBuku
            {
                BukuId = buku.Id,
                Aksi = "Delete",
                Waktu = DateTime.Now,
                DataSebelum = System.Text.Json.JsonSerializer.Serialize(buku)
            });

            _uow.Buku.DeleteAsync(buku);
            await _uow.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        // Export CSV
        public async Task<FileResult> Export()
        {
            var data = await _uow.Buku.GetAllAsync();
            var csv = "Judul,Penulis,Jenis Buku,Jumlah Halaman\n";

            foreach (var b in data)
                csv += $"{b.Judul},{b.Penulis},{b.JenisBukuId},{b.JumlahHalaman}\n";

            return File(new System.Text.UTF8Encoding().GetBytes(csv),
                "text/csv",
                "buku_export.csv");
        }
    }
}
