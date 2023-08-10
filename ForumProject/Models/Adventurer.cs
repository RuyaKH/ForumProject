using Microsoft.AspNetCore.Identity;

namespace ForumProject.Models
{
    public class Adventurer : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<ForumModel>? Threads { get; set; }

    }
}
