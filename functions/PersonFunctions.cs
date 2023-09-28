using Microsoft.AspNetCore.Mvc;

public class PersonFunctions {

    private readonly ApplicationDbContext dbContext;
    public PersonFunctions(ApplicationDbContext context) {
        dbContext = context;
    }

    public List<Person> GetAllPeople() {
        return dbContext.people.ToList();
    }

    public Person? GetOnePerson(int id){
        return dbContext.people.FirstOrDefault(person => person.id == id);
    }

    public List<Person> GetPersonByFirstName(string var) {
        return dbContext.people
            .Where(person => person.firstName == var)
            .ToList();
    }

    public List<Person> GetPersonByLastName(string var) {
        return dbContext.people
            .Where(person => person.lastName == var)
            .ToList();
    }

    public Person? AddPerson(string firstname, string lastname, int age)
    {
        return null;
    }
}