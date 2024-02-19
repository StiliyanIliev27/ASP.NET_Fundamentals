namespace Forum.Data
{
    using Forum.Data.Configuration;
    using Forum.Data.Models;
    using Forum.Data.Seeding;
    using Microsoft.EntityFrameworkCore;
    public class ForumDbContext : DbContext
    {       
        public ForumDbContext(DbContextOptions<ForumDbContext> opt)
            : base(opt)
        {
            
        }
       
        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());            

            base.OnModelCreating(modelBuilder);
        }
    }
}
