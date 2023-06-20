using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo__List.Migrations;
using ToDo__List.Models;

namespace ToDo__List.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class CategoryController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        
        public CategoryController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Category
        public async Task<IActionResult> Index(string encryptedUserId, string salt)
        {
            // Decrypt the user ID
            string userId = QueryStringHelper.DecryptQueryStringParameter(encryptedUserId, salt);

            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return View(categories);
        }



        // GET: Category/Create

        public IActionResult Create()
        {
            return View();
        }
   
        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category, string encryptedUserId, string salt)
        {
            if (ModelState.IsValid)
            {
                // Decrypt the user ID
                string userId = QueryStringHelper.DecryptQueryStringParameter(encryptedUserId, salt);

                // Assign the user ID to the category
                category.UserId = userId;

                _context.Add(category);
                await _context.SaveChangesAsync();

                // Retrieve the updated list of tasks for the current user
                var tasks = await _context.Categories.Where(c => c.UserId == userId).ToListAsync();

                // Pass the tasks to the Index view
                return View("Index", tasks);
            }
            return View(category);
        }


        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category, string encryptedUserId, string salt)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Decrypt the user ID
                string userId = QueryStringHelper.DecryptQueryStringParameter(encryptedUserId, salt);

                // Retrieve the updated list of tasks for the current user
                var tasks = await _context.Categories.Where(c => c.UserId == userId).ToListAsync();

                // Pass the tasks to the Index view
                return View("Index", tasks);
            }
            return View(category);
        }


        // GET: Category/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string encryptedUserId, string salt)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories' is null.");
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            // Decrypt the user ID
            string userId = QueryStringHelper.DecryptQueryStringParameter(encryptedUserId, salt);

            return RedirectToAction(nameof(Index), new { userId = category.UserId });

        }


        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
