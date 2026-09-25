using System.ComponentModel.DataAnnotations;

namespace P05Shop.API.DTO
{
    public class CityDto
    {
        [Required(ErrorMessage = "City name is required.")]
        public string Name { get; set; }
        public string Country { get; set; }


       
    }
}
