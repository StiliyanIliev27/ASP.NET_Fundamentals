using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homies.Data.Models
{
    public class EventParticipant
    {
        [Required]
        [ForeignKey(nameof(HelperId))]
        public string HelperId { get; set; } = null!;
        public IdentityUser Helper { get; set; }

        [Required]
        [ForeignKey(nameof(EventId))]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
