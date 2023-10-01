using System.ComponentModel.DataAnnotations.Schema;

public class User {
    [Column("id")]
    public Guid id {get; set;}

    [Column("usernumber")]
    public int usernumber {get; set;}

    [Column("username")]
    public string username {get; set;}

    [Column("password")]
    public string password {get; set;}

    [Column("authoritylevel")]
    public int authoritylevel {get; set;}
}