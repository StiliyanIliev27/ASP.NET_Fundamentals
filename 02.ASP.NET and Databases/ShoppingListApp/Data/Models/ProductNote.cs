using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Data.Models
{
    /// <summary>
    /// Shopping List ProductNote
    /// </summary>
    [Comment("Shopping List ProductNote")]
    public class ProductNote
    {
        /// <summary>
        /// ProductNote Identifier
        /// </summary>
        [Key]
        [Comment("ProductNote Identifier")]
        public int Id { get; set; }

        /// <summary>
        /// ProductNote Content
        /// </summary>
        [Required]
        [Comment("ProductNote Content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Product Identifier
        /// </summary>
        [Required]
        [ForeignKey(nameof(ProductId))]
        [Comment("Product Identifier")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
