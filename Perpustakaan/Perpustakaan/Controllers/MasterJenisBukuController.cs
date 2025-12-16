using Microsoft.AspNetCore.Mvc;
using Perpustakaan.Data.Repository;
using Perpustakaan.Models;

namespace Perpustakaan.Controllers
{
    public class MasterJenisBukuController : Controller
    {
        private readonly IGenericRepository<MasterJenisBuku> _repository;
        private readonly IUnitOfWork _uow;

        public MasterJenisBukuController(IGenericRepository<MasterJenisBuku> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.MasterJenisBuku.GetAllAsync());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MasterJenisBuku model)
        {
            await _uow.MasterJenisBuku.AddAsync(model);
            await _uow.SaveAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _uow.MasterJenisBuku.GetByIdAsync(id);
            _uow.MasterJenisBuku.DeleteAsync(entity);
            await _uow.SaveAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _uow.MasterJenisBuku.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MasterJenisBuku model)
        {
            _uow.MasterJenisBuku.UpdateAsync(model);
            await _uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
