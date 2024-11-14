using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BestStoreMvc.Models
{
    public class Product
    {
       
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Brand { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        [Precision(16,2)]
        public string Price { get; set; }
        [Required]
        public string Description { get; set; }
        [MaxLength(200)]
        public string ImageFileName { get; set; }
        public DateTime CreatedAt{ get; set; }
        public int CityId { get; set; }
        public City City { get; set; }


    }
}
