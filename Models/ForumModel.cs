using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForumProject.Models
{
    public class ForumModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Descrption is required")]
        public string Description { get; set; } = null!;

        [Display(Name = "Date Posted")]
        public DateTime DatePosted { get; set; }
        public bool? Favourite { get; set; }
        public string? Status { get; set; }
        public int? UpVotes { get; set; }

        //public string Tags { get; set; } (probs a select list)

        [ValidateNever]
        [ForeignKey("Adventurer")]
        public string AdventurerId { get; set; } = null!;
        public Adventurer? Adventurer { get; set; }


    }
}
