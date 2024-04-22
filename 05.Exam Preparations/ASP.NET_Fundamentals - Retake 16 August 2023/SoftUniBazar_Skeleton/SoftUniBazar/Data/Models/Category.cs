using SoftUniBazar.Common;
using System.ComponentModel.DataAnnotations;

namespace SoftUniBazar.Data.Models
{
    public class Category
    {
        public Category()
        {
             Ads = new HashSet<Ad>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.CategoryNameMaxLength)]
        public string Name { get; set; } = null!;
       
        public ICollection<Ad> Ads { get; set; }
    }
}
