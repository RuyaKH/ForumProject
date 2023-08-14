using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumProject.Models
{
    public class ForumViewModel
    {
        public List<ForumModel>? Threads { get; set; }
        public string? SearchString { get; set; }
        public string? StatusString { get; set; }
        public SelectList? StatusList { get; set; }
    }
}
