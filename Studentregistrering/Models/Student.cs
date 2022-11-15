using Studentregistrering.UserExperience;

namespace Studentregistrering.Models
{
    internal class Student : IMenuOption
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public virtual List<Course> Courses { get; set; } = new();

        public string SearchableText => LastName;
        public string MenuText => $"{FirstName.PadRight(15)} {LastName}"; 
        
    }
}