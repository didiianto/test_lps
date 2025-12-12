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

        public BukuController(IBukuRepository repo,
            IGenericRepository<Models.MasterJenisBuku> masterJenisBukuRepository,
            AppDbContext appContext)
        {
            _repo = repo;
            _masterJenisBukuRepository = masterJenisBukuRepository;
            _appContext = appContext;
        }

        public async Task<IActionResult> Index(string search)
        {
            var data = string.IsNullOrEmpty(search) ? await _repo.GetAllAsync(b => b.JenisBuku)
                : await _repo.Search(search);

            return View(data);
        }

        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Jenis = await _masterJenisBukuRepository.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Buku model)
        {
            await _repo.AddAsync(model);
            await _repo.SaveAsync();

            await _appContext.HistoryBuku.AddAsync(new HistoryBuku
            {
                BukuId = model.Id,
                Aksi = "Create",
                Waktu = DateTime.Now,
                DataSesudah = System.Text.Json.JsonSerializer.Serialize(model)
            });
            await _appContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
