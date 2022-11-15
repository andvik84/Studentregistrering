using Studentregistrering.Data;
using Studentregistrering.Models;

namespace Studentregistrering.UserExperience
{
    internal class CourseMenu : Menu<MenuOption>
    {

        public List<Course> SortedCourses { get; set; } = new();

        public CourseMenu(Student student, List<Course> allCourses)
        {
            foreach (var course in student.Courses ?? new())
            {
                SortedCourses.Add(course);
                Choices.Add(new MenuOption($"[ {course.MenuText} ]"));
            }
            foreach (var course in allCourses.ToList().ExceptBy(SortedCourses.Select(c => c.Name), ac => ac.Name))
            {
                SortedCourses.Add(course);
                Choices.Add(new MenuOption($"  {course.MenuText}"));
            }
        }
    }
}
