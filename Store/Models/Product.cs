using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_product { get; set; }
    public string NameProduct { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    [Required]
    [ForeignKey("ProductCategory")]
    public int id_category { get; set; }
    public ProductCategories ProductCategory { get; set; }
}