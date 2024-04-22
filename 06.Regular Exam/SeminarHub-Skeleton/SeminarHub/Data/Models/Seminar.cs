using Microsoft.AspNetCore.Identity;
using SeminarHub.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        public Seminar()
        {
            SeminarsParticipants = new HashSet<SeminarParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.SeminarTopicMaxLength)]
        public string Topic { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.SeminarLecturerMaxLength)]
        public string Lecturer { get; set; } = null!;

       
        [MaxLength(EntityValidationConstants.SeminarDetailsMaxLength)]
        public string? Details { get; set; }//Грешка в условието! Details трябва да бъде nullable, а не required, както е зададено!

        [Required]
        public string OrganizerId { get; set; } = null!;

        [Required]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        public DateTime DateAndTime { get; set; }

        [Range(EntityValidationConstants.SeminarDurationMinLength,
            EntityValidationConstants.SeminarDurationMaxLength)]
        public int? Duration { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; }
    }
}
