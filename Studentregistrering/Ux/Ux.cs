using Microsoft.EntityFrameworkCore;
using Studentregistrering.Data;
using Studentregistrering.Models;
using System.Runtime.InteropServices;

namespace Studentregistrering.UserExperience
{
    internal class Ux
    {
        /// <summary>
        /// Redigera en student och dess kurser
        /// </summary>
        /// <param name="student">Ett student-object</param>
        /// <returns>Null = Avsluta utan att spara. Annars ett redigerat studentobject</returns>
        internal static Student EditStudent(Student student, List<Course> allCourses)
        {
            string firstName = student.FirstName;
            string lastName = student.LastName;
            string city = student.City;
            var courses = student.Courses ?? new List<Course>();

            var editStudentOptions = new List<MenuOption> {
                                new MenuOption("Förnamn: ".PadRight(15) + firstName),
                                new MenuOption("Efternamn: ".PadRight(15) + lastName),
                                new MenuOption("Stad: ".PadRight(15) + city),
                                new MenuOption("Klasser: ".PadRight(15) + string.Join(", ", courses.Select(c => c.Name))),
                                new MenuOption("Spara och avsluta"),
                                new MenuOption("Ta bort student"),
                                new MenuOption("Avsluta utan att spara")
                            };

            var studentMenu = new Menu<MenuOption>(editStudentOptions, "Ändra student");
            while (studentMenu.DisplayMenu(studentMenu.Choice))
            {
                switch (studentMenu.Choice)
                {
                    case 0:
                        firstName = Helpers.ReadString("Förnamn: ", clear: true);
                        break;
                    case 1:
                        lastName = Helpers.ReadString("Efternamn: ", clear: true);
                        break;
                    case 2:
                        city = Helpers.ReadString("Stad: ", clear: true);
                        break;
                    case 3:
                        EditStudentCourses(student, allCourses);
                        break;
                    case 4:
                        student.FirstName = firstName;
                        student.LastName = lastName;
                        student.City = city;
                        student.Courses = courses;
                        return student;

                    case 5:
                        Console.Clear();
                        Console.WriteLine($"Är det säkert att du vill ta bort {firstName} {lastName}?");
                        if(Helpers.ReadString("Skriv DELETE: ") == "DELETE")
                        {
                            using ( var dbCtx = new StudentDbContext())
                            {
                                dbCtx.Students.Remove(student);
                                dbCtx.SaveChanges();
                            }
                            return null;
                        }
                        break;
                            
                    case 6:
                        return null!;
                }

                studentMenu.UpdateMenuOptions(new List<MenuOption> {
                                new MenuOption("Förnamn: ".PadRight(15) + firstName),
                                new MenuOption("Efternamn: ".PadRight(15) + lastName),
                                new MenuOption("Stad: ".PadRight(15) + city),
                                new MenuOption("Klasser: ".PadRight(15) + string.Join(", ", courses.Select(c => c.Name))),
                                new MenuOption("Spara och avsluta"),
                                new MenuOption("Avsluta utan att spara")
                            });
            }

            return null!;
        }

        /// <summary>
        /// Visa och toggla en students kurser
        /// </summary>
        /// <param name="student"></param>
        private static void EditStudentCourses(Student student, List<Course> allCourses)
        {
            while (true)
            {
                var courseMenu = new CourseMenu(student, allCourses);
                courseMenu.Header = $"Kurser för {student.FirstName} {student.LastName}";

                if (courseMenu.DisplayMenu())
                {
                    var currentCourse = courseMenu.SortedCourses[courseMenu.Choice] ?? new Course();

                    if (student.Courses.Any(c => c == currentCourse))
                    {
                        student.Courses.Remove(currentCourse);
                    }
                    else
                    {
                        student.Courses.Add(currentCourse);
                    }

                }
                else
                {
                    return;
                }


            }
        }

        /// <summary>
        /// Visa lista på alla courses -> Visa alla studenter i en course -> Redigera student
        /// </summary>
        public static void ShowCourses(StudentDbContext dbCtx)
        {
            var courses = dbCtx.Courses
                   .Include(c => c.Students)
                   .ToList();
            

            var courseMenu = new Menu<Course>(courses, "Klasser");

            if (courseMenu.DisplayMenu())
            {
                var currentCourse = courseMenu.Choices[courseMenu.Choice];

                var students = courses
                    .Where(c => c.CourseId == currentCourse.CourseId)
                    .First()
                    .Students;

                var courseStudentMenu = new Menu<Student>(students, $"Studenter i klassen {currentCourse.Name}");
                if (courseStudentMenu.DisplayMenu(searchable: true))
                {
                    Console.Clear();

                    var studentToEdit = courseStudentMenu.Choices[courseStudentMenu.Choice];
                    var updatedStudent = Ux.EditStudent(studentToEdit, dbCtx.Courses.ToList());
                }


            }
        }
    }
}
