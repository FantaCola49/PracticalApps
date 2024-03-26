using Microsoft.AspNetCore.Identity; // RoleManager, RoleUser
using Microsoft.AspNetCore.Mvc; // Controller, IActionResult
using static System.Console;

namespace Northwind.Mvc.Controllers;

public class RolesController : Controller
{
    private string AdminRole = "Administrators";
    private string UserEmail = "test@example.com";
    private string UserPass = "Pa$$w0rd";

    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<IdentityUser> userManager;

    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager )
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        // Если админчика нет в БД, то создаём его
        if(!(await roleManager.RoleExistsAsync(AdminRole)))
        {
            await roleManager.CreateAsync(new IdentityRole(AdminRole));
        }
        IdentityUser user = await userManager.FindByEmailAsync(UserEmail);
        // если пользователя нет, мы его тоже создаём
        if (user == null)
        {
            user = new();
            user.UserName = UserEmail;
            user.Email = UserEmail;
            IdentityResult result = await userManager.CreateAsync(user, UserPass);

            if (result.Succeeded)
            {
                WriteLine($"User {user.UserName} created successfully");
            }
            else
            {
                foreach(IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }
        if (!user.EmailConfirmed)
        {
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            IdentityResult result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                WriteLine($"Email пользователя {user.UserName} успешно подтверждён");
            }
            else
            {
                foreach(IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }


        if (!(await userManager.IsInRoleAsync(user, AdminRole)))
        {
            IdentityResult result = await userManager.AddToRoleAsync(user, AdminRole);

            if (result.Succeeded)
            {
                WriteLine($"Пользователю {user.UserName} успешно добавлена роль {AdminRole}");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    WriteLine(error.Description);
                }
            }
        }
        return Redirect("/");
    }
}
