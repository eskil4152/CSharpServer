using System.ComponentModel.DataAnnotations.Schema;

public class Person {
    [Column("id")]
    public Guid id {get; set;}

    [Column("firstname")]
    public required string FirstName {get; set;}

    [Column("lastname")]
    public required string LastName {get; set;}

    [Column("age")]
    public required int Age {get; set;}
}