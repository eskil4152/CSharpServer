using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class PersonFunctions {

    private readonly ApplicationDbContext dbContext;
    private readonly JwtManagerRepository jwtManagerRepository;

    public PersonFunctions(ApplicationDbContext context, JwtManagerRepository jwtManagerRepository) {
        dbContext = context;
        this.jwtManagerRepository = jwtManagerRepository;
    }

    public List<Person> GetAllPeople() {
        return dbContext.people.ToList();
    }

    public List<Person> GetPersonByFirstName(string var) {
        return dbContext.people
            .Where(person => person.FirstName == var)
            .ToList();
    }

    public List<Person> GetPersonByLastName(string var) {

        return dbContext.people
            .Where(person => person.LastName == var)
            .ToList();
    }

    public List<Person> GetPersonByFullName(string firstName, string lastName) {

        return dbContext.people
            .Where(person => person.FirstName == firstName && person.LastName == lastName)
            .ToList();
    }

    public List<Person> GetPersonById(int id)
    {
        return dbContext.people
            .Where(person => person.Id == id)
            .ToList();
    }

    public int AddPerson(Person person, string token) {
        bool isAuthorized = jwtManagerRepository.Authorize(token, 3);

        if (isAuthorized) {
            dbContext.people
                .Add(person);

            dbContext.SaveChanges();

            return 200;
        }

        return 401;
    }
}