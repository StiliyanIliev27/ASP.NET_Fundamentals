using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Message
{
    public class MessageViewModel
    {
        [Display(Name = "Sender Name")]
        [Required(ErrorMessage = "Field {0} is required!")]
        public string Sender { get; set; } = null!;

        [Display(Name = "Message")]
        [Required(ErrorMessage = "Field {0} is required!")]
        public string MessageText { get; set; } = null!;
    }
}
