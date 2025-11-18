using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class ActionLogs
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_log { get; set; }
    public DateOnly action_date { get; set; }

    [Required]
    [ForeignKey("User")]
    public int id_user { get; set; }
    public User User { get; set; }
    
    [Required]
    [ForeignKey("UserAction")]
    public int id_action { get; set; }
    public UserAction UserAction { get; set; }
}