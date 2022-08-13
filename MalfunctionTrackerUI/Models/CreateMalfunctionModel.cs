using System.ComponentModel.DataAnnotations;

namespace MalfunctionTrackerUI.Models
{
    public class CreateMalfunctionModel
    {
        [Required]
        [MaxLength(75)]
        public string Malfunction { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Priority")]
        public string PriorityId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
