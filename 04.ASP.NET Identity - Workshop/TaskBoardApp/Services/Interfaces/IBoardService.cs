using TaskBoardApp.Models.Board;

namespace TaskBoardApp.Services.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> ViewAllAsync();
        Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync();
        Task<bool> ExistsByIdAsync(int id);
    }
}
