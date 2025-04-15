using HataBildirimModel2.Models;
using HataBildirimModel2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HataBildirimModel2.Controllers
{

    public class Faultontroller : Controller
    {
        private readonly FaultInterface _repository;

        public Faultontroller(FaultInterface repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var faultNotifications = await _repository.GetAllAsync();
            return View(faultNotifications);
        }

        // GET: FaultNotifications/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var faultNotification = await _repository.GetByIdAsync(id);
            if (faultNotification == null)
            {
                return NotFound();
            }
            return View(faultNotification);
        }

        // GET: FaultNotifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FaultNotifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UserId,FaulTypeId,Explanation,FileId")] FaultNotification faultNotification)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(faultNotification);
                return RedirectToAction(nameof(Index));
            }
            return View(faultNotification);
        }

        // GET: FaultNotifications/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var faultNotification = await _repository.GetByIdAsync(id);
            if (faultNotification == null)
            {
                return NotFound();
            }
            return View(faultNotification);
        }

        // POST: FaultNotifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId,FaulTypeId,Explanation,FileId")] FaultNotification faultNotification)
        {
            if (id != faultNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(faultNotification);
                return RedirectToAction(nameof(Index));
            }
            return View(faultNotification);
        }

        // GET: FaultNotifications/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var faultNotification = await _repository.GetByIdAsync(id);
            if (faultNotification == null)
            {
                return NotFound();
            }
            return View(faultNotification);
        }

        // POST: FaultNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }


}
