using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Store.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_user { get; set; }
    public string FullName { get; set; }

    public string Email { get; set; } = string.Empty;
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly UpdatedAt { get; set; }

    [Required]
    [ForeignKey("Role")]
    public int id_role { get; set; }
    public Role Role { get; set; }
}