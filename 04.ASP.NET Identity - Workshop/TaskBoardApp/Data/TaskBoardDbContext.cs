using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Models;

namespace TaskBoardApp.Data
{
    public class TaskBoardDbContext : IdentityDbContext
    {
        private IdentityUser TestUser { get; set; }
        private Board OpenBoard { get; set; }
        private Board InProgressBoard { get; set; }
        private Board DoneBoard { get; set; }
        public TaskBoardDbContext(DbContextOptions<TaskBoardDbContext> options)
            : base(options)
        {
        }
        public DbSet<Board> Boards { get; set; } = null!;
        public DbSet<Models.Task> Tasks { get; set; } = null!;

        public ICollection<Models.Task> SeedTasks()
        {
            ICollection<Models.Task> tasks = new HashSet<Models.Task>();
            Models.Task currentTask;

            currentTask = new Models.Task()
            {
                Id = 1,
                Title = "Improve CSS styles",
                Description = "Implement better styling for all public pages",
                CreatedOn = DateTime.Now.AddDays(-200),
                OwnerId = TestUser.Id,
                BoardId = OpenBoard.Id
            };
            tasks.Add(currentTask);

            currentTask = new Models.Task()
            {
                Id = 2,
                Title = "Android Client App",
                Description = "Create Android client app for the TaskBoard RESTful API",
                CreatedOn = DateTime.Now.AddMonths(-5),
                OwnerId = TestUser.Id,
                BoardId = OpenBoard.Id
            };
            tasks.Add(currentTask);

            currentTask = new Models.Task()
            {
                Id = 3,
                Title = "Desktop Client App",
                Description = "Create Windows Forms appclient for the TaskBoard RESTful API",
                CreatedOn = DateTime.Now.AddMonths(-1),
                OwnerId = TestUser.Id,
                BoardId = InProgressBoard.Id
            };
            tasks.Add(currentTask);

            currentTask = new Models.Task()
            {
                Id = 4,
                Title = "Create Tasks",
                Description = "Implement [Create Task] page for adding new tasks",
                CreatedOn = DateTime.Now.AddYears(-1),
                OwnerId = TestUser.Id,
                BoardId = DoneBoard.Id
            };
            tasks.Add(currentTask);

            return tasks;
        }

        public void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            TestUser = new IdentityUser()
            {
                UserName = "steliiliev920@gmail.com",
                NormalizedUserName = "STELIILIEV920@GMAIL.COM"
            };

            TestUser.PasswordHash = hasher
                .HashPassword(TestUser, "271105stelito");
        }

        public void SeedBoards()
        {
            OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };

            InProgressBoard = new Board()
            {
                Id = 2,
                Name = "In Progress"
            };

            DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedUsers();
            builder
                .Entity<IdentityUser>()
                    .HasData(TestUser);

            SeedBoards();
            builder
                .Entity<Board>()
                    .HasData(OpenBoard, InProgressBoard, DoneBoard);
            builder
                .Entity<Models.Task>()
                .HasData(SeedTasks());

            base.OnModelCreating(builder);
        }
    }
}
