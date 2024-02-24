using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;
using TaskBoardApp.Services.Interfaces;

namespace TaskBoardApp.Services
{
    public class BoardService : IBoardService
    {
        private readonly TaskBoardDbContext context;

        public BoardService(TaskBoardDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<BoardSelectViewModel>> AllForSelectAsync()
        {
            var allBoards = await context.Boards
                .Select(b => new BoardSelectViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,             
                })
                .ToListAsync();

            return allBoards;
        }
        public async Task<bool> ExistsByIdAsync(int id)
        {
            var result = await context.Boards
                .AnyAsync(b => b.Id == id);

            return result;
        }

        public async Task<IEnumerable<BoardViewModel>> ViewAllAsync()
        {
            var allBoards = await context.Boards
                .Select(b => new BoardViewModel()
                {
                    Name = b.Name,
                    Tasks = b.Tasks
                        .Select(t => new TaskViewModel()
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Owner = t.Owner.UserName
                        })
                }).ToArrayAsync();

            return allBoards;
        }
    }
}
