using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class UserAction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_action { get; set; }
    public string action { get; set; }
    public string decription { get; set; }
}