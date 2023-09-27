public record Person(int id, string FirstName, string LastName, int Age);

public class PersonFunctions {
    private static readonly List<Person> peopleList = new() {
        new Person(1, "Ole", "Pedersen", 38),
        new Person(2, "Silje", "Pedersen", 36),
        new Person(3, "Tim", "Pedersen", 15)
    };

    public static List<Person> GetAllPeople() {
        return peopleList;
    }

    public static Person? GetOnePerson(int id) {
        return peopleList.SingleOrDefault(res => res.id == id);
    }
}