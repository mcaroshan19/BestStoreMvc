
using System.ComponentModel.DataAnnotations;

namespace BestStoreMvc.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Brand { get; set; }
        [Required, MaxLength(100)]
        public string Category { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Description { get; set; }
        //new changes 
        public int CityId { get; set; }

        public IFormFile? ImageFile { get; set; }
      
    }
}
