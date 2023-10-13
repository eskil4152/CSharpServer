using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSharpServer.Models;

public class User
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Required]
    [Column("username")]
    public string Username { get; set; }

    [Required]
    [Column("password")]
    public string Password { get; set; }

    [Required]
    [ForeignKey("RoleId")]
    public virtual Role Role { get; set; }

    [Column("role_id")]
    public long RoleId { get; set; }
}