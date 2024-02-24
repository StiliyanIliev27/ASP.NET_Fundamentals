using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Common;

namespace TaskBoardApp.Models.Task
{
    public class TaskViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(EntityValidationConstants.TaskTitleMinLength)]
        [MaxLength(EntityValidationConstants.TaskTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(EntityValidationConstants.TaskDescriptionMinLength)]
        [MaxLength(EntityValidationConstants.TaskDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string Owner { get; set; } = null!;
    }
}
