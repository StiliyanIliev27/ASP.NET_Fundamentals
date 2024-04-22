using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Common;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models.Category;
using SeminarHub.Models.Seminar;
using System.Globalization;
using System.Security.Claims;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext context;

        public SeminarController(SeminarHubDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await context.Seminars
                .Select(s => new SeminarAllViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    Organizer = s.Organizer.UserName,
                    DateAndTime = s.DateAndTime.ToString(EntityValidationConstants.DateTimeFormat)
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormModel();
            model.Categories = await GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormModel model)
        {
            DateTime dateOfSeminar = DateTime.Now;

            if(!DateTime.TryParseExact(model.DateAndTime, EntityValidationConstants.DateTimeFormat
                , CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfSeminar))
            {
                ModelState.AddModelError(nameof(model.DateAndTime)
                   , $"Invalid data! The date time format must be: {EntityValidationConstants.DateTimeFormat}");
            }

            if (!GetCategoriesAsync().Result.Any(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            string organizerId = GetUserId();

            var seminar = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                DateAndTime = dateOfSeminar,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId = organizerId
            };

            await context.Seminars.AddAsync(seminar);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string organiserId = GetUserId();

            var seminars = await context.SeminarsParticipants
                .Where(sp => sp.ParticipantId == organiserId)
                .Select(s => new SeminarJoinedViewModel()
                {
                    Id = s.Seminar.Id,
                    Topic = s.Seminar.Topic,
                    Lecturer = s.Seminar.Lecturer,
                    DateAndTime = s.Seminar.DateAndTime.ToString(EntityValidationConstants.DateTimeFormat),
                    Organizer = s.Participant.NormalizedUserName
                })
                .ToListAsync();

            return View(seminars);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var semindarToAdd = await context.Seminars
                .FindAsync(id);

            if(semindarToAdd == null)
            {
                return BadRequest();
            }

            string organizerId = GetUserId();

            if (!context.SeminarsParticipants.Any(a => a.ParticipantId == organizerId && a.SeminarId == id))
            {
                var entry = new SeminarParticipant()
                {
                    ParticipantId = organizerId,
                    SeminarId = semindarToAdd.Id
                };

                await context.SeminarsParticipants.AddAsync(entry);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Joined));
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var seminarToRemove = await context.Seminars
                .Where(s => s.Id == id).FirstOrDefaultAsync();

            if(seminarToRemove == null)
            {
                return BadRequest();
            }

            string organizerId = GetUserId();

            var ap = await context.SeminarsParticipants
                .FirstOrDefaultAsync(ap => ap.ParticipantId == organizerId && ap.SeminarId == id);

            if(ap == null)
            {
                return BadRequest();
            }

            context.SeminarsParticipants.Remove(ap);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Joined));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var s = await context.Seminars
                .Where(s => s.Id == id).FirstOrDefaultAsync();

            if(s == null)
            {
                return BadRequest();
            }

            string organizerId = GetUserId();

            if(s.OrganizerId != organizerId)
            {
                return Unauthorized();
            }

            var model = new SeminarFormModel()
            {
                Topic = s.Topic,
                Lecturer = s.Lecturer,
                Details = s.Details,
                DateAndTime = s.DateAndTime.ToString(EntityValidationConstants.DateTimeFormat),
                Duration = s.Duration,
                CategoryId = s.CategoryId,
            };

            model.Categories = await GetCategoriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormModel model, int id)
        {
            DateTime dateOfSeminar = DateTime.Now;

            var seminar = await context.Seminars
                .FirstOrDefaultAsync(s => s.Id == id);

            if(seminar == null)
            {
                return BadRequest();
            }

            string organizerId = GetUserId();

            if(seminar.OrganizerId != organizerId)
            {
                return Unauthorized();
            }

            if (!DateTime.TryParseExact(model.DateAndTime, EntityValidationConstants.DateTimeFormat
                , CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfSeminar))
            {
                ModelState.AddModelError(nameof(model.DateAndTime)
                   , $"Invalid data! The date time format must be: {EntityValidationConstants.DateTimeFormat}");
            }

            if (!GetCategoriesAsync().Result.Any(c => c.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            seminar.Topic = model.Topic;
            seminar.Lecturer = model.Lecturer;
            seminar.Details = model.Details;
            seminar.DateAndTime = dateOfSeminar;
            seminar.Duration = model.Duration;
            seminar.CategoryId = model.CategoryId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Seminars
                .Where(s => s.Id == id)
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString("dd.MM.yyyy HH:mm"),
                    Duration = s.Duration,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    Details = s.Details,
                    Organizer = s.Organizer.UserName
                })
                .FirstOrDefaultAsync();

            if(model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var s = await context.Seminars
                .Where(s => s.Id == id).FirstOrDefaultAsync();

            if(s == null)
            {
                return BadRequest();
            }

            string organizerId = GetUserId();

            if (s.OrganizerId != organizerId)
            {
                return Unauthorized();
            }

            var model = new SeminarDeleteViewModel()
            {
                Id = s.Id,
                Topic = s.Topic,
                DateAndTime = s.DateAndTime
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminarToDelete = await context.Seminars
                .Where(s => s.Id == id).FirstOrDefaultAsync();

            if(seminarToDelete == null)
            {
                return BadRequest();
            }

            //The foreign key problem can occur in many-to-many table relationships unless this condition is provided.
            if (context.SeminarsParticipants.Any(sp => sp.ParticipantId != GetUserId()))
            {
                var collectionOfForeignUsers = await context.SeminarsParticipants
                    .Where(sp => sp.ParticipantId != GetUserId() && sp.Seminar.Id == seminarToDelete.Id)
                    .ToListAsync();

                foreach(var user in collectionOfForeignUsers)
                {
                    context.SeminarsParticipants.Remove(user);                   
                }

                await context.SaveChangesAsync();
            }

            context.Seminars.Remove(seminarToDelete);
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
