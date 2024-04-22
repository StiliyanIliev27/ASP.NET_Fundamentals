using Microsoft.AspNetCore.Identity;
using SoftUniBazar.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data.Models
{
    public class Ad
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.AdNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.AdDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string OwnerId { get; set; } = null!;

        [Required]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

       public DateTime CreatedOn { get; set; }

       [Required]
       [ForeignKey(nameof(CategoryId))]    
       public int CategoryId { get; set; }

       [Required]
       public Category Category { get; set; }
    }
}
