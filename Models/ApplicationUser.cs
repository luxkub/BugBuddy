using Microsoft.AspNetCore.Identity;

namespace BugBuddy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ApplicationUser() { } // Default constructor

        public ApplicationUser(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}