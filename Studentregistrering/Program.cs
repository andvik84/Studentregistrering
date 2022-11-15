using Microsoft.EntityFrameworkCore;
using Studentregistrering.Data;
using Studentregistrering.Models;
using Studentregistrering.UserExperience;

namespace Studentregistrering
{
    internal class Program
    {
        static void Main()
        {
            var dbCtx = new StudentDbContext();

            var mainMenu = new Menu<MenuOption>(
                    new List<MenuOption> {
                    new MenuOption("Registrera ny student"),
                    new MenuOption("Visa alla studenter"),
                    new MenuOption("Visa courses"),
                    new MenuOption("Skapa en ny course"),
                    new MenuOption("Avsluta") },
                    "Det supersnygga studentregistret!");
            while (mainMenu.DisplayMenu())
            {
                switch (mainMenu.Choice)
                {
                    case 0: // Registrera ny student 
                        var newStudent = new Student();
                        Ux.EditStudent(newStudent, dbCtx.Courses.ToList());
                        if (newStudent != null)
                        {
                            dbCtx.Students.Add(newStudent);
                            dbCtx.SaveChanges();
                        }
                        break;

                    case 1: // Lista alla studenter

                        var students = (dbCtx.Students
                            .Include(s => s.Courses))
                            .OrderBy(s => s.LastName)
                            .ThenBy(s => s.FirstName)
                            .ToList();

                        Console.WindowHeight = students.Count + 10;
                        var studentListMenu = new Menu<Student>(students, "Studentlista");
                        if (studentListMenu.DisplayMenu(searchable: true))
                        {
                            Console.Clear();

                            // Redigera student
                            var studentToEdit = studentListMenu.Choices[studentListMenu.Choice];
                            Ux.EditStudent(studentToEdit, dbCtx.Courses.ToList());
                            dbCtx.SaveChanges();
                            
                        }
                        break;

                    case 2: // Visa klasser
                        Ux.ShowCourses(dbCtx);
                        break;

                    case 3: // Skapa ny klass
                        Console.Clear();
                        Console.WriteLine("Skapa en ny course");

                        var newCourse = new Course()
                        {
                            Name = Helpers.ReadString("Namn på course: "),
                            StartDate = Helpers.ReadDateTime("Start-datum [YYYY-MM-DD]: "),
                            EndDate = Helpers.ReadDateTime("Slut-datum [YYYY-MM-DD]: "),
                        };

                        dbCtx.Courses.Add(newCourse);
                        dbCtx.SaveChanges();
                        break;

                    case 4: // Avsluta
                        mainMenu.Exit = true;
                        break;
                }
            }
            Console.WriteLine("Bye!");
        }
    }
}