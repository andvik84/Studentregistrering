using Studentregistrering.UserExperience;

namespace Studentregistrering.Models
{
    internal class Course : IMenuOption
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
        public virtual List<Student> Students { get; set; } = new();

        public string SearchableText => Name;
        public string MenuText => Name;
    }
}
