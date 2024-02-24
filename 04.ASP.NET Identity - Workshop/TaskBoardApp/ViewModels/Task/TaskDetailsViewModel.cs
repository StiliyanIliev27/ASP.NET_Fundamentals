using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.ViewModels.Task
{
    public class TaskDetailsViewModel : TaskViewModel
    {
        public string CreatedOn { get; set; } = null!;
        public string Board { get; set;} = null!;
    }
}
