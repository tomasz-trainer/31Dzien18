using System.ComponentModel.DataAnnotations;

namespace P06Shop.Shared
{
    //public class Product
    //{
    //    [Key]
    //    public int Code { get; set; }

    //    public int Id { get; set; }

    //    //nvarchar(50) 
    //    [MaxLength(50)]
    //    public string Title { get; set; }

    //    public string Description { get; set; }
    //}

    public class Product
    { 
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string Barcode { get; set; }

        [Range(0.01, 9999.99, ErrorMessage = "Price must be greater than 0 and less than 9999.99.")]
        public double Price { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
