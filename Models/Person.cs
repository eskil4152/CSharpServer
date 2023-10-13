using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Person {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id {get; set;}

    [Column("firstname")]
    public string FirstName {get; set;}

    [Column("lastname")]
    public string LastName {get; set;}

    [Column("age")]
    public int Age {get; set;}
}