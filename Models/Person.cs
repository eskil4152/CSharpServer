using System.ComponentModel.DataAnnotations.Schema;

public class Person {
    [Column("id")]
    public int id {get; set;}

    [Column("firstname")]
    public string firstName {get; set;}

    [Column("lastname")]
    public string lastName {get; set;}

    [Column("age")]
    public int age {get; set;}
}