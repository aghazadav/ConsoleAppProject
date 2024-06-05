public class User
{
    public string FirstName { get; }
    public string LastName { get; }
    public DateTime BirthDate { get; }
    public string Email { get; }
    public string Password { get; }
    public bool HasJob { get; }
    public decimal Balance { get; set; }
    public decimal CreditLimit { get; set; }

    public User(string firstName, string lastName, DateTime birthDate, string email, string password, bool hasJob)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Email = email;
        Password = password;
        HasJob = hasJob;
        Balance = 0;
        CreditLimit = 1000; 
    }
}
