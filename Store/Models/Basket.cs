using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Basket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_basket { get; set; }
    public int ProdCount { get; set; }
    public decimal ResultPrice { get; set; }
    public bool IsOrdered { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public int id_user { get; set; }
    public User User { get; set; }
    
    [Required]
    [ForeignKey("Product")]
    public int id_product { get; set; }
    public Product Product { get; set; }
    
    [Required]
    [ForeignKey("Order")]
    public int id_order { get; set; }
    public Order Order { get; set; }
}