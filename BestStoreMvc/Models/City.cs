namespace BestStoreMvc.Models
{
    public class City
    {

        public int CityId { get; set; }
        public string CityName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
