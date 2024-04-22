using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data.Models
{
    public class AdBuyer
    {
        [Required]
        [ForeignKey(nameof(BuyerId))]
        public string BuyerId { get; set; }
        public IdentityUser Buyer { get; set; }

        [Required]
        [ForeignKey(nameof(AdId))]
        public int AdId { get; set; }
        public Ad Ad { get; set; }
    }
}
