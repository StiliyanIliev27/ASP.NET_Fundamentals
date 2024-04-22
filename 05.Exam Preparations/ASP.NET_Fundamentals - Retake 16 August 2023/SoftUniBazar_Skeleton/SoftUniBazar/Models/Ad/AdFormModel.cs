using SoftUniBazar.Common;
using SoftUniBazar.Models.Category;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Models.Ad
{
    public class AdFormModel
    {
        public AdFormModel()
        {
            Categories = new HashSet<CategoryViewModel>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(EntityValidationConstants.AdNameMaxLength,
            MinimumLength = EntityValidationConstants.AdNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.AdDescriptionMaxLength,
            MinimumLength = EntityValidationConstants.AdDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
