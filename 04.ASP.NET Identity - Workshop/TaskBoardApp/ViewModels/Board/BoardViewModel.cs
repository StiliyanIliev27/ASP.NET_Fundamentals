using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Common;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Models.Board
{
    public class BoardViewModel
    {
        public BoardViewModel()
        {
            Tasks = new HashSet<TaskViewModel>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(EntityValidationConstants.BoardNameMinLength)]
        [MaxLength(EntityValidationConstants.BoardNameMaxLength)]
        public string Name { get; set; } = null!;
        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}
