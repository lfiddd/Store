using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class BasketItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_basket_item { get; set; }
    public int ProdCount { get; set; }
    
    [Required]
    [ForeignKey("Basket")]
    public int id_basket { get; set; }
    public Basket Basket { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int id_product { get; set; }
    public Product Product { get; set; }
}