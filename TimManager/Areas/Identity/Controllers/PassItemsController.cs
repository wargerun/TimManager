using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tim.Manager.Db.Entities;
using Tim.Manager.Db.Repositories.PassItems;

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
        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            List<PassItem> passItems = await _passItemRepository.GetPassItems(user.Id).ToListAsync();
            return View(passItems);
        }

        // GET: Identity/PassItems/Create
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_passItemRepository.Users, "Id", "Id");
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
                catch (System.Exception ex)                                   
                {
                    ModelState.AddModelError(ex.Data[PassItemRepository.ItemPropertyKey]?.ToString(), $"Unable to save changes. {ex.Message}.");
                }
            }

            return View(newPassItem);
        }
        /*
         *            

      // GET: Identity/PassItems/Details/5
      public async Task<IActionResult> Details(string id)
      {
          if (id == null)
          {
              return NotFound();
          }

          var passItem = await _passItemRepository.PassItems
              .Include(p => p.User)
              .FirstOrDefaultAsync(m => m.UserId == id);
          if (passItem == null)
          {
              return NotFound();
          }

          return View(passItem);
      }
         * 
      // GET: Identity/PassItems/Edit/5
      public async Task<IActionResult> Edit(string id)
      {
          if (id == null)
          {
              return NotFound();
          }

          var passItem = await _passItemRepository.PassItems.FindAsync(id);
          if (passItem == null)
          {
              return NotFound();
          }
          ViewData["UserId"] = new SelectList(_passItemRepository.Users, "Id", "Id", passItem.UserId);
          return View(passItem);
      }

      // POST: Identity/PassItems/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(string id, [Bind("UserId,Name,UserName,Password,Uri,Created,CreatedBy,Discription")] PassItem passItem)
      {
          if (id != passItem.UserId)
          {
              return NotFound();
          }

          if (ModelState.IsValid)
          {
              try
              {
                  _passItemRepository.Update(passItem);
                  await _passItemRepository.SaveChangesAsync();
              }
              catch (DbUpdateConcurrencyException)
              {
                  if (!PassItemExists(passItem.UserId))
                  {
                      return NotFound();
                  }
                  else
                  {
                      throw;
                  }
              }
              return RedirectToAction(nameof(Index));
          }
          ViewData["UserId"] = new SelectList(_passItemRepository.Users, "Id", "Id", passItem.UserId);
          return View(passItem);
      }

      // GET: Identity/PassItems/Delete/5
      public async Task<IActionResult> Delete(string id)
      {
          if (id == null)
          {
              return NotFound();
          }

          var passItem = await _passItemRepository.PassItems
              .Include(p => p.User)
              .FirstOrDefaultAsync(m => m.UserId == id);
          if (passItem == null)
          {
              return NotFound();
          }

          return View(passItem);
      }

      // POST: Identity/PassItems/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(string id)
      {
          var passItem = await _passItemRepository.PassItems.FindAsync(id);
          _passItemRepository.PassItems.Remove(passItem);
          await _passItemRepository.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
      }

      private bool PassItemExists(string id)
      {
          return _passItemRepository.PassItems.Any(e => e.UserId == id);
      }

      */
    }
}
