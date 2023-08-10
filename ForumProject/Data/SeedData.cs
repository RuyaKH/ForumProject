using ForumProject.Models;
using Microsoft.AspNetCore.Identity;

namespace ForumProject.Data
{
    public class SeedData
    {
        public static void Initialise(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ForumDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Adventurer>>();

            if (context.Adventurers.Any())
            {
                context.Adventurers.RemoveRange(context.Adventurers);
                context.Users.RemoveRange(context.Users);
                context.SaveChanges();
            }

            var ruya = new Adventurer
            {
                UserName = "ruya@forum.com",
                Email = "ruya@forum.com",
                EmailConfirmed = true,
                
            };

            userManager
                .CreateAsync(ruya, "Password_1")
                .GetAwaiter()
                .GetResult();

            if(context.Threads.Any())
            {
                context.Threads.RemoveRange(context.Threads);
                context.SaveChanges();
            }

            context.Threads.AddRange(
                new ForumModel
                {
                    Title = "Testing Thread One",
                    Description = "The Adventurer(user) has a problem and describes it in this box here",
                    Status = "Open",
                    DatePosted = DateTime.Today,
                    UpVotes = 5,
                    Adventurer = ruya
                },
                new ForumModel
                {
                    Title = "Testing Thread Two",
                    Description = "The Adventurer(user) has a problem and describes it in this box here",
                    Status = "Open",
                    DatePosted = DateTime.Today.AddDays(-1),
                    UpVotes = 10,
                    Adventurer = ruya
                });


            context.SaveChanges();
        }
    }
}
