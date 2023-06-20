using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using ToDo__List.Data;
using ToDo__List.Models;

namespace ToDo__List.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public Account(UserManager<IdentityUser> userManager,
                       SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Username, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    // Generate the salt
                    string salt = QueryStringHelper.SaltGenerator.GenerateSalt();

                    // Encrypt the user ID using the generated salt value
                    string encryptedUserId = QueryStringHelper.EncryptQueryStringParameter(user.Id, salt);

                    // Append the encrypted user ID to the return URL
                    string returnUrl = $"/Category/Index?encryptedUserId={Uri.EscapeDataString(encryptedUserId)}&salt={Uri.EscapeDataString(salt)}";

                    return Redirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // GET: Account/Login
        public IActionResult LogIn()
        {
            return View();
        }

        // POST: Account/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogIn model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.UsernameOrEmail);
                if (user == null)
                {
                    // If the user is not found by email, try finding them by username
                    user = await userManager.FindByNameAsync(model.UsernameOrEmail);
                }

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password,
                        model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        // Generate the salt
                        string salt = QueryStringHelper.SaltGenerator.GenerateSalt();

                        // Encrypt the user ID
                        string encryptedUserId = QueryStringHelper.EncryptQueryStringParameter(user.Id, salt);

                        // Build the return URL with encrypted user ID and salt
                        string returnUrl = $"/Category/Index?encryptedUserId={Uri.EscapeDataString(encryptedUserId)}&salt={Uri.EscapeDataString(salt)}";

                        // Redirect to the return URL
                        return Redirect(returnUrl);
                    }

                    //return RedirectToAction("Index", "Category", new { userId = user.Id });
                    //}
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            }
            return View(model);
        }

        //LOGOUT

        public async Task<IActionResult> Logout()
        {
            // Perform logout logic
            await signInManager.SignOutAsync();  

            return RedirectToAction("LogIn", "Account");
        }


    }
}