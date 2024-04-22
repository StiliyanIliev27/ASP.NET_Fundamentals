using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Common;
using SoftUniBazar.Data;
using SoftUniBazar.Data.Models;
using SoftUniBazar.Models.Ad;
using SoftUniBazar.Models.Category;
using System.Security.Claims;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext context;

        public AdController(BazarDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Ads.Select(a => new AdAllViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                ImageUrl = a.ImageUrl,
                CreatedOn = a.CreatedOn.ToString(EntityValidationConstants.DateTimeFormatViewModel),
                Category = a.Category.Name,
                Description = a.Description,
                Price = a.Price,
                Owner = a.Owner.UserName
            }).ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = GetUserId();

            var userAds = await context.AdsBuyers
                .Where(ab => ab.BuyerId == userId)
                .Select(ab => new AdAllViewModel()
                {
                    Id = ab.Ad.Id,
                    Name = ab.Ad.Name,
                    ImageUrl = ab.Ad.ImageUrl,
                    CreatedOn = ab.Ad.CreatedOn.ToString(EntityValidationConstants.DateTimeFormatViewModel),
                    Category = ab.Ad.Category.Name,
                    Description = ab.Ad.Description,
                    Price = ab.Ad.Price,
                    Owner = ab.Ad.Owner.UserName
                })
                .ToListAsync();

            return View(userAds);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var adToAdd = await context.Ads
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if(adToAdd == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if(!context.AdsBuyers.Any(a => a.BuyerId == userId && a.AdId == id))
            {
                var entry = new AdBuyer()
                {
                    AdId = adToAdd.Id,
                    BuyerId = userId
                };

                await context.AdsBuyers.AddAsync(entry);
                await context.SaveChangesAsync();                
            }

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var adToRemove = await context.Ads
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if( adToRemove == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ab = await context.AdsBuyers
                .FirstOrDefaultAsync(ab => ab.BuyerId == userId && ab.AdId == id);

            if(ab == null)
            {
                return BadRequest();
            }

            context.AdsBuyers.Remove(ab);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdFormModel();
            model.Categories = await GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdFormModel model)
        {
            if(!GetCategoriesAsync().Result.Any(c => c.Id == model.Id))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }
            
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            var ad = new Ad()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                OwnerId = GetUserId(),
                ImageUrl = model.ImageUrl,
                CreatedOn = DateTime.Now,
                CategoryId = model.CategoryId,
            };

            await context.Ads.AddAsync(ad);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Ads
                .Where(a => a.Id == id)
                .Select(a => new AdFormModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    ImageUrl = a.ImageUrl,
                    CategoryId = a.CategoryId,
                }).FirstAsync();

            model.Categories = await GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdFormModel model, int id)
        {
            var ad = await context.Ads
                .FirstOrDefaultAsync(a => a.Id == id);

            if(ad == null)
            {
                return BadRequest();
            }

            if(ad.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            if (!GetCategoriesAsync().Result.Any(c => c.Id == model.Id))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();

                return View(model);
            }

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();

            return categories;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
