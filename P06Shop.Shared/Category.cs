using System.ComponentModel.DataAnnotations;

namespace P06Shop.Shared
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;
    }
}
