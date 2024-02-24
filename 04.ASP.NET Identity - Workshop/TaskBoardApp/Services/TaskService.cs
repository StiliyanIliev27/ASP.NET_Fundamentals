using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;
using TaskBoardApp.Services.Interfaces;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskBoardDbContext context;

        public TaskService(TaskBoardDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(TaskFormModel model, string ownerId)
        {
            Data.Models.Task task = new Data.Models.Task()
            {
                Title = model.Title,
                Description = model.Description,
                BoardId = model.BoardId,
                CreatedOn = DateTime.Now,
                OwnerId = ownerId,
            };

            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();
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

        public async Task DeleteAsync(int id)
        {
            var task = await context.Tasks.FirstAsync(b => b.Id == id);

            if(task == null)
            {
                throw new ArgumentException("Task not found!");
            }

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(TaskFormModel model, int id)
        {
            var task = await context.Tasks.FirstAsync(t => t.Id == id);

            if(task == null)
            {
                throw new ArgumentException("Task not found!");
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;

            await context.SaveChangesAsync();
        }

        public async Task<TaskFormModel> GetForEditByIdAsync(int id)
        {
            var task = await context.Tasks
                .FirstAsync(t => t.Id == id);
            
            if(task == null)
            {
                throw new ArgumentException("Task not found!");
            }

            var model = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = await AllForSelectAsync()
            };

            return model;
        }

        public async Task<TaskDetailsViewModel> GetForDetailsByIdAsync(int id)
        {
            var model = await context.Tasks
                .Select(t => new TaskDetailsViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Owner = t.Owner.UserName,
                    CreatedOn = t.CreatedOn.ToString("f"),
                    Board = t.Board.Name
                }).FirstAsync(t => t.Id == id);

            return model;
        }

        public async Task<TaskViewModel> GetForDeleteByIdAsync(int id)
        {
            var task = await context.Tasks.FirstAsync(t => t.Id == id);

            if (task == null)
            {
                throw new ArgumentException("Task not found!");
            }

            var model = new TaskViewModel()
            {
                Description = task.Description
            };

            return model;
        }
    }
}
