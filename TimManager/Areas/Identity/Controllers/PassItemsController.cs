using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tim.Manager.Db.Entities;
using Tim.Manager.Db.Repositories.PassItems;
using System;
using System.Security.Authentication;

namespace TimManager.Areas.Identity.Controllers.PassManager
{
    [Area("Identity")]
    [Authorize]
    public class PassItemsController : Controller
    {
        private readonly IPassItemRepository _passItemRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public PassItemsController(
            IPassItemRepository passItemRepository,
            UserManager<IdentityUser> userManager)

        {
            _passItemRepository = passItemRepository;
            _userManager = userManager;
        }

        // GET: Identity/PassItems
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<PassItem> passItems = await _passItemRepository.GetPassItems(user.Id).ToListAsync();
            return View(passItems);
        }

        // GET: Identity/PassItems/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Identity/PassItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PassItem newPassItem)
        {
            ModelState.Remove(nameof(newPassItem.UserId));

            if (ModelState.IsValid)
            {
                try
                {
                    IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
                    newPassItem.UserId = user.Id;
                    newPassItem.User = user;

                    await _passItemRepository.InsertAsync(newPassItem);

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(ex.Data[PassItemRepository.ItemPropertyKey]?.ToString() ?? "", $"Unable to save changes. {ex.Message}.");
                }
            }

            return View(newPassItem);
        }

        // GET: Identity/PassItems/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            PassItem passItem = await getPassItem(id);

            if (passItem == null)
            {
                return NotFound();
            }

            return View(passItem);
        }

        // GET: Identity/PassItems/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PassItem passItem = await getPassItem(id);

            if (passItem == null)
            {
                return NotFound();
            }

            return View(passItem);
        }

        private async Task<PassItem> getPassItem(int id)
        {
            if (id == 0)
            {
                return null;
            }

            throwIfNotAuthenticated();
            PassItem passItem = await _passItemRepository.GetPassItemAsync(id);

            return passItem;
        }

        // POST: Identity/PassItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PassItem newPassItem)
        {
            ModelState.Remove(nameof(newPassItem.UserId));
            ModelState.Remove(nameof(newPassItem.Password));

            if (ModelState.IsValid)
            {
                try
                {
                    throwIfNotAuthenticated();

                    await _passItemRepository.UpdateAsync(newPassItem);
                    return RedirectToAction(nameof(Details), new { id = newPassItem.Id });
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(ex.Data[PassItemRepository.ItemPropertyKey]?.ToString() ?? "", $"Unable to save changes. {ex.Message}.");
                }
            }

            return View(newPassItem);
        }

        // GET: Identity/PassItems/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            throwIfNotAuthenticated();

            PassItem passItem = await getPassItem(id);

            if (passItem == null)
            {
                return NotFound();
            }

            return View(passItem);
        }

        // POST: Identity/PassItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _passItemRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private void throwIfNotAuthenticated()
        {
            if ((HttpContext.User == null) || !HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthenticationException();
            }
        }
    }
}
