using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace Store.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_order { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryType { get; set; }
    public string DeliveryAddress { get; set; }
    
    [Required]
    [ForeignKey("OrderStatus")]
    public int id_status { get; set; }
    public OrderStatus OrderStatus { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public int id_user { get; set; }
    public User User { get; set; }
}