using SeminarHub.Common;
using SeminarHub.Models.Category;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models.Seminar
{
    public class SeminarFormModel
    {
        public SeminarFormModel()
        {
            Categories = new HashSet<CategoryViewModel>();
        }

        [Required]
        [MinLength(EntityValidationConstants.SeminarTopicMinLength)]
        [MaxLength(EntityValidationConstants.SeminarTopicMaxLength)]
        public string Topic { get; set; } = null!;

        [Required]
        [MinLength(EntityValidationConstants.SeminarLecturerMinLength)]
        [MaxLength(EntityValidationConstants.SeminarLecturerMaxLength)]
        public string Lecturer { get; set; } = null!;


        [MinLength(EntityValidationConstants.SeminarDetailsMinLength)]
        [MaxLength(EntityValidationConstants.SeminarDetailsMaxLength)]
        public string? Details { get; set; } = string.Empty;

        [Required]
        public string DateAndTime { get; set; } = null!;

        
        [Range(EntityValidationConstants.SeminarDurationMinLength,
            EntityValidationConstants.SeminarDurationMaxLength)]
        public int? Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
