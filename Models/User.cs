using System.ComponentModel.DataAnnotations.Schema;

public class User {
    [Column("id")]
    public Guid Id {get; set;} = Guid.NewGuid();

    [Column("username")]
    public required string Username {get; set;} = "";

    [Column("password")]
    public required string Password {get; set;} = "";

    [Column("authoritylevel")]
    public int Authoritylevel {get; set;} = 0;
}