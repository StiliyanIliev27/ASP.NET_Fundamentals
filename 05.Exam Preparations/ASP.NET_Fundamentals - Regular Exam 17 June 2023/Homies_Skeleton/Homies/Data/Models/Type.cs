using Homies.Common;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    public class Type
    {
        public Type()
        {
            Events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationsConstants.TypeNameMaxLength)]
        public string Name { get; set; } = null!;
        public ICollection<Event> Events { get; set; }
    }
}
