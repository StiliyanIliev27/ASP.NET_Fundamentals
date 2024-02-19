namespace Forum.Web.Controllers
{
    using Forum.Data.Models;
    using Forum.Services.Contracts;
    using Forum.ViewModels.Post;
    using Microsoft.AspNetCore.Mvc;
    public class PostController : Controller
    {
        private readonly IPostService postService;
       
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await postService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await postService.AddPostAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await postService.GetByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await postService.EditPostAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await postService.DeletePostAsync(id);
            
            return RedirectToAction(nameof(All));
        }
    }
}
