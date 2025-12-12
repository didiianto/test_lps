using Microsoft.AspNetCore.Mvc;
using Perpustakaan.Data.Repository;
using Perpustakaan.Models;

namespace Perpustakaan.Controllers
{
    public class MasterJenisBukuController : Controller
    {
        private readonly IGenericRepository<MasterJenisBuku> _repository;

        public MasterJenisBukuController(IGenericRepository<MasterJenisBuku> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterJenisBuku model)
        {
            await _repository.AddAsync(model);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.DeleteAsync(entity);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
