using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo__List.Migrations;
using ToDo__List.Models;

namespace ToDo__List.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: Category

        public async Task<IActionResult> Index(string userId)
        {
            //return _context.Categories != null ? 
            //            View(await _context.Categories.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            var categories = await _context.Categories.Where(c => c.UserId == userId).ToListAsync();

            return View(categories);
        }
        
        /*
        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
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
        } */


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
        public async Task<IActionResult> Create(Category category, string userId)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, Category category, string userId)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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

            return RedirectToAction(nameof(Index), new { userId = category.UserId });

        }


        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
