using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Column("authorizationlevel")]
    public int AuthorizationLevel { get; set; } = 1;
}