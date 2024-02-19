

using Forum.Data.Models;

namespace Forum.Data.Seeding
{
    class PostSeeder
    {
        internal Post[] GeneratePost()
        {
            ICollection<Post> posts = new HashSet<Post>();
            Post currentPost;

            currentPost = new Post()
            {
                Title = "My first post",
                Content = "Just deployed a sleek new feature on my ASP.NET forum app - check it out and share your thoughts!"
            };
            posts.Add(currentPost);

            currentPost = new Post()
            {
                Title = "My second post",
                Content = "What's your go-to ASP.NET tip? Let's exchange some coding wisdom in our vibrant community!"
            };
            posts.Add(currentPost);

            currentPost = new Post()
            {
                Title = "My third post",
                Content = "Facing a challenge in my ASP.NET project - seeking advice from fellow developers. Any insights appreciated!"
            };
            posts.Add(currentPost);

            currentPost = new Post()
            {
                Title = "My fourth post",
                Content = "Exciting news! Our ASP.NET forum now supports real-time notifications. Stay connected with the latest discussions effortlessly!"
            };
            posts.Add(currentPost);

            return posts.ToArray();
        }
    }
}
