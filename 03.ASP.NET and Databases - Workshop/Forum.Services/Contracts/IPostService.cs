using Forum.ViewModels.Post;

namespace Forum.Services.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostFormModel>> GetAllAsync();
        Task AddPostAsync(PostFormModel model);
        Task EditPostAsync(PostFormModel model);    
        Task DeletePostAsync(string id);
        Task<PostFormModel> GetByIdAsync(string id);
    }
}
