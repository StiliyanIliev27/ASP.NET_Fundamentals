
using Homies.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    public class Event
    {
        public Event()
        {
            EventsParticipants = new HashSet<EventParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationsConstants.EventNameMaxLength)]
        public string Name { get; set; } = null!;
       
        [Required]
        [MaxLength(EntityValidationsConstants.EventDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(OrganiserId))]
        public string OrganiserId { get; set; } = null!;

        [Required]
        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        public int TypeId { get; set; }

        [Required]
        public Type Type { get; set; } = null!;

        public ICollection<EventParticipant> EventsParticipants { get; set; }


        //Added additionally to help myself with date time formats.

        [NotMapped]
        public string DateTimeFormat { get; set; } = "dd-MM-yyyy HH:mm";
    }
}
