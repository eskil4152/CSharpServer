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
}