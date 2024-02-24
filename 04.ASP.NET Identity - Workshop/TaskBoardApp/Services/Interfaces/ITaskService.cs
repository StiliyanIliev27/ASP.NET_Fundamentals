using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services.Interfaces
{
    public interface ITaskService
    {
        Task AddAsync(TaskFormModel model, string ownerId);
        Task<TaskDetailsViewModel> GetForDetailsByIdAsync(int id);
        Task EditAsync(TaskFormModel model, int id);
        Task DeleteAsync(int id);
        Task<TaskFormModel> GetForEditByIdAsync(int id);
        Task<TaskViewModel> GetForDeleteByIdAsync(int id);  
        Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync();
    }
}
