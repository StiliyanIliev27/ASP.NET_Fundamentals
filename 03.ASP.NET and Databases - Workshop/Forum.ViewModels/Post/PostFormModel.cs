

using static Forum.Common.Validations.EntityValidations.Post;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Post
{
    public class PostFormModel
    {
        [Required]
        public string Id { get; set; } = null!;
        
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;
    }
}
