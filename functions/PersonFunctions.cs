public record Person(string FirstName, string LastName, int Age);

public class PersonFunctions {
    private static readonly List<Person> peopleList = new() {
        new Person("Ole", "Pedersen", 38),
        new Person("Silje", "Pedersen", 36),
        new Person("Tim", "Pedersen", 15)
    };

    public static List<Person> GetPeople() {
        return peopleList;
    }

    public static Person? GetPerson(int i) {
        if (peopleList.Count >= i) {
            return peopleList[i];
        } else {
            return null;
        }
    }
}