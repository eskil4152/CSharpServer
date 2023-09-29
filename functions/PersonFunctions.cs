using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

public class PersonFunctions {

    private readonly ApplicationDbContext dbContext;
    public PersonFunctions(ApplicationDbContext context) {
        dbContext = context;
    }

    public List<Person> GetAllPeople() {
        return dbContext.people.ToList();
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

    public List<Person> GetPersonByFullName(string firstName, string lastName) {

        return dbContext.people
            .Where(person => person.firstName == firstName && person.lastName == lastName)
            .ToList();
    }

    public Person? AddPerson(string firstname, string lastname, int age)
    {
        return null;
    }
}