using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

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

    public int AddPerson(Person person, string token) {
        bool isAuthorized = jwtManagerRepository.CheckTokenAuthorization(token, 4);

        if (isAuthorized) {
            person.id = Guid.NewGuid();
            dbContext.people
                .Add(person);

            dbContext.SaveChanges();

            return 200;
        }

        return 401;
    }
}