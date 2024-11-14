

using System.ComponentModel.DataAnnotations.Schema;

namespace BestStoreMvc.Models 
{
    public class ManagerDto
    {



        public string Name { get; set; }
        public string Category { get; set; }
        public string Freshness { get; set; } 
        public IFormFile? FileName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Comments { get; set; }

        public string Email { get; set; }
        [Column(TypeName = "bigint")]
        public long PhoneNumber { get; set; }
    }
}
