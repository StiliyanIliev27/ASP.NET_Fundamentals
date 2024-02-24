using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskBoardApp.Common;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EntityValidationConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(EntityValidationConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(BoardId))]
        public int BoardId { get; set; }
        public Board? Board { get; set; }

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public string OwnerId { get; set; } = null!;
        public IdentityUser Owner { get; set; } = null!;
    }
}
