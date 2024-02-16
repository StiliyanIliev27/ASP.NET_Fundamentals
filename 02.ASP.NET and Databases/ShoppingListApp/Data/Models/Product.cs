using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Data.Models
{
    /// <summary>
    /// Shopping List Product
    /// </summary>
    [Comment("Shopping List Product")]
    public class Product
    {
        /// <summary>
        /// Product constructor
        /// </summary>
        public Product()
        {
            ProductNotes = new HashSet<ProductNote>();
        }

        /// <summary>
        /// Product Identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Comment("Product Identifier")]
        public int Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Comment("Product Name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ProductNotes Collection
        /// </summary>
        [Comment("ProductNotes Collection")]
        public IEnumerable<ProductNote> ProductNotes { get; set; }
    }
}
