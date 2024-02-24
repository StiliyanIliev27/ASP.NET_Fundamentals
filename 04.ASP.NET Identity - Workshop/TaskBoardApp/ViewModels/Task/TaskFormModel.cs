using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Common;
using TaskBoardApp.Models.Board;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(EntityValidationConstants.TaskTitleMaxLength,
            MinimumLength = EntityValidationConstants.TaskTitleMinLength,
            ErrorMessage = "Title should be at least {2} characters long.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(EntityValidationConstants.TaskDescriptionMaxLength,
            MinimumLength = EntityValidationConstants.TaskDescriptionMinLength,
            ErrorMessage = "Description should be at least {2} characters long.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Board")]
        public int BoardId { get; set; }
        public IEnumerable<BoardSelectViewModel>? Boards { get; set; }
    }
}
