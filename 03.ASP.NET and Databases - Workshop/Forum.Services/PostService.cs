using Forum.Data;
using Forum.Data.Models;
using Forum.Services.Contracts;
using Forum.ViewModels.Post;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext context;
        public PostService(ForumDbContext context)
        {
            this.context = context;
        }

        public async Task AddPostAsync(PostFormModel model)
        {
            var post = new Post()
            {
                Content = model.Content,
                Title = model.Title
            };

            await context.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(string id)
        {
            Post? post = await context.Posts.FirstAsync(p => p.Id.ToString() == id);

            if (post == null)
            {
                throw new ArgumentException("Post not found!");
            }

            context.Posts.Remove(post);
            await context.SaveChangesAsync();
        }

        public async Task EditPostAsync(PostFormModel model)
        {
            var post = await context.Posts
                .FirstAsync(p => p.Id.ToString() == model.Id);

            if(post == null)
            {
                throw new Exception("Invalid post!");
            }

            post.Title = model.Title;
            post.Content = model.Content;

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostFormModel>> GetAllAsync()
        {
            return await context.Posts
                .Select(p => new PostFormModel()
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Content = p.Content
                })
                .ToListAsync();
        }

        public async Task<PostFormModel> GetByIdAsync(string id)
        {
            Post post = await context.Posts.FirstAsync(p => p.Id.ToString() == id);
 
            var model = new PostFormModel()
            {
                Title = post.Title,
                Content = post.Content
            };

            return model;
        }
    }
}
