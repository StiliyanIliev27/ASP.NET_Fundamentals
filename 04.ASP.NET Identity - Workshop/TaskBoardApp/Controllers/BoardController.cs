using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Services.Interfaces;

namespace TaskBoardApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var boards = await boardService.ViewAllAsync();
            
            return View(boards);
        }
    }
}
