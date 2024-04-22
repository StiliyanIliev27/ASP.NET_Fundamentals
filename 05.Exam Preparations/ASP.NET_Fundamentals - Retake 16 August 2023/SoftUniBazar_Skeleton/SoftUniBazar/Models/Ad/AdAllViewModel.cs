namespace SoftUniBazar.Models.Ad
{
    public class AdAllViewModel
    { 
        public int Id { get; set; }
      
        public string Name { get; set; } = null!;
      
        public string Description { get; set; } = null!;
       
        public decimal Price { get; set; }
       
        public string OwnerId { get; set; } = null!;
      
        public string Owner { get; set; } = null!;
       
        public string ImageUrl { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;
     
        public int CategoryId { get; set; }
       
        public string Category { get; set; } = null!;
    }
}
