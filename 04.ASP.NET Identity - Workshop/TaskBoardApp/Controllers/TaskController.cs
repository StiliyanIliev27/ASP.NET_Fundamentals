using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Common;
using TaskBoardApp.Extensions;
using TaskBoardApp.Models.Task;
using TaskBoardApp.Services.Interfaces;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly IBoardService boardService;
        private readonly ITaskService taskService;

        public TaskController(IBoardService boardService, ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel model = new TaskFormModel()
            {
                Boards = await boardService.AllForSelectAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Boards = await boardService.AllForSelectAsync();
                return View(model);
            }

            bool boardExists = await boardService.ExistsByIdAsync(model.BoardId);
            if(!boardExists)
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist.");
                model.Boards = await boardService.AllForSelectAsync();
                return View(model);
            }

            string userId = User.GetId();

            await taskService.AddAsync(model, userId);

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await taskService.GetForDetailsByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Board");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await taskService.GetForEditByIdAsync(id);
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskFormModel model, int id)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            
            await taskService.EditAsync(model, id);

            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await taskService.GetForDeleteByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskFormModel model, int id)
        {
            await taskService.DeleteAsync(id);

            return RedirectToAction("All", "Board");
        }
    }
}
