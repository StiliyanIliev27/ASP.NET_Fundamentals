using Homies.Common;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models.Event;
using Homies.Models.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext context;
        public EventController(HomiesDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {           
            var events = await context.Events
                .Select(e => new EventsAllViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(e.DateTimeFormat),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                }).ToListAsync();
            
            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await context.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            if (!e.EventsParticipants.Any(ep => ep.HelperId == userId))
            {
                e.EventsParticipants.Add(new EventParticipant()
                {
                    EventId = e.Id,
                    HelperId = userId
                });

                await context.SaveChangesAsync();
            }

            return RedirectToAction("Joined");
        }

        public async Task<IActionResult> Leave(int id)
        {
            var e = await context.Events
                .Where(e => e.Id == id)
                .Include (e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if(e == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var ep = e.EventsParticipants
                .FirstOrDefault(ep => ep.HelperId == userId);

            if(ep == null)
            {
                return BadRequest();
            }

            context.EventsParticipants.Remove(ep);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await context.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .AsNoTracking()
                .Select(e => new EventsAllViewModel()
                {
                    Id = e.Event.Id,
                    Name = e.Event.Name,
                    Start = e.Event.Start.ToString(e.Event.DateTimeFormat),
                    Type = e.Event.Type.Name,
                    Organiser = e.Event.Organiser.UserName
                }).ToListAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new EventFormModel();
            model.Types = GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if(!DateTime.TryParseExact(model.Start, EntityValidationsConstants.DateTimeFormat
                , CultureInfo.InvariantCulture
                ,DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.Start)
                    , $"Invalid data! The date time format must be: {EntityValidationsConstants.DateTimeFormat}");
            }

            if (!DateTime.TryParseExact(model.End, EntityValidationsConstants.DateTimeFormat
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(nameof(model.End)
                    , $"Invalid data! The date time format must be: {EntityValidationsConstants.DateTimeFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = GetTypes();
                
                return View(model);
            }

            Event ev = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                CreatedOn = DateTime.Now,
                OrganiserId = GetUserId(),
                TypeId = model.TypeId,
                Start = start,
                End = end,
            };

            await context.Events.AddAsync(ev);
            await context.SaveChangesAsync();
           
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await context.Events
                .Where(e => e.Id == id)
                .Select(e => new EventFormModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString(EntityValidationsConstants.DateTimeFormat),
                    End = e.End.ToString(EntityValidationsConstants.DateTimeFormat),
                    TypeId = e.TypeId,
                }).FirstAsync();

            model.Types = GetTypes();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormModel model, int id)
        {
            var ev = await context.Events.FirstAsync(e => e.Id == id);

            if (ev == null)
            {
                return BadRequest();
            }

            if(ev.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(model.Start, EntityValidationsConstants.DateTimeFormat
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None, out start))
            {
                ModelState.AddModelError(nameof(model.Start)
                    , $"Invalid data! The date time format must be: {EntityValidationsConstants.DateTimeFormat}");
            }

            if (!DateTime.TryParseExact(model.End, EntityValidationsConstants.DateTimeFormat
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None, out end))
            {
                ModelState.AddModelError(nameof(model.End)
                    , $"Invalid data! The date time format must be: {EntityValidationsConstants.DateTimeFormat}");
            }

            if (!ModelState.IsValid)
            {
                model.Types = GetTypes();

                return View(model);
            }        

            ev.Name = model.Name;
            ev.Description = model.Description;
            ev.Start = start;
            ev.End = end;
            ev.TypeId = model.TypeId;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await context.Events
                .Where(e => e.Id == id)
                .AsNoTracking()
                .Select(e => new EventDetailsViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString(EntityValidationsConstants.DateTimeFormat),
                    End = e.End.ToString(EntityValidationsConstants.DateTimeFormat),
                    Organiser = e.Organiser.UserName,
                    CreatedOn = e.CreatedOn.ToString(EntityValidationsConstants.DateTimeFormat),
                    Type = e.Type.Name
                }).FirstOrDefaultAsync();               

            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty; 
        }

        private IEnumerable<TypeViewModel> GetTypes()
        {
            var types = context.Types
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToList();

            return types;
        }
    }
}
