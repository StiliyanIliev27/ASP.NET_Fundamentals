using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Common;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        public Board()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.BoardNameMaxLength)]
        public string Name { get; set; } = null!;
        public ICollection<Task> Tasks { get; set; }
    }
}
