namespace WebAPI.Domain.Entities;

public class Passenger
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Gender { get; set; }

    public Passenger()
    {
    }
    public Passenger(string email, string firstName, string lastName, bool gender)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
    }
}