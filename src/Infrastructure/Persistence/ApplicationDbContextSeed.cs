using System.Linq;
using System.Threading.Tasks;
using CleanArchWeb.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace CleanArchWeb.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var administratorRole = new ApplicationRole("Administrator");

            if (!roleManager.Roles.Any(r => r.Name == administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (!userManager.Users.Any(u => u.UserName == administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            //TODO: SeedSampleData
            // // Seed, if necessary
            // if (!context.TodoLists.Any())
            // {
            //     context.TodoLists.Add(new TodoList
            //     {
            //         Title = "Shopping",
            //         Colour = Colour.Blue,
            //         Items =
            //         {
            //             new TodoItem { Title = "Apples", Done = true },
            //             new TodoItem { Title = "Milk", Done = true },
            //             new TodoItem { Title = "Bread", Done = true },
            //             new TodoItem { Title = "Toilet paper" },
            //             new TodoItem { Title = "Pasta" },
            //             new TodoItem { Title = "Tissues" },
            //             new TodoItem { Title = "Tuna" },
            //             new TodoItem { Title = "Water" }
            //         }
            //     });
            //
            //     await context.SaveChangesAsync();
            // }
            await Task.CompletedTask;
        }
    }
}
