using Homies.Common;
using Homies.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Homies.Models.Type;

namespace Homies.Models.Event
{
    public class EventFormModel
    {
        public EventFormModel()
        {
            Types = new HashSet<TypeViewModel>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(EntityValidationsConstants.EventNameMinLength)]
        [MaxLength(EntityValidationsConstants.EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(EntityValidationsConstants.EventDescriptionMinLength)]
        [MaxLength(EntityValidationsConstants.EventDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string Start { get; set; } = string.Empty;

        [Required]
        public string End { get; set; } = string.Empty;

        [Required]
        public int TypeId { get; set; } 
        public IEnumerable<TypeViewModel> Types { get; set; }
    }
}
