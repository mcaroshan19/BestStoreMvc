using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestStoreMvc.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }
        public string Freshness { get; set; }
        public string ImagefileName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(100)]
        public string Comments { get; set; }
        [Required]
        public string Email { get; set; }
        [Column(TypeName = "bigint")]
        public long PhoneNumber { get; set; }

    }
}
