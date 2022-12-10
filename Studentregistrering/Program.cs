using Microsoft.EntityFrameworkCore;
using Studentregistrering.DataAccess.Data;
using Studentregistrering.UserExperience;

namespace Studentregistrering
{
    internal class Program
    {
        static void Main()
        {
            var dbCtx = new StudentDbContext();
            var studentRepo = new StudentRepository(dbCtx);
            var courseRepo = new CourseRepository(dbCtx);

            var mainMenu = new Menu<MenuOption>(
                    new List<MenuOption> {
                    new MenuOption("Registrera ny student"),
                    new MenuOption("Visa alla studenter"),
                    new MenuOption("Visa courses"),
                    new MenuOption("Skapa en ny course"),
                    new MenuOption("Avsluta") },
                    "Det supersnygga studentregistret!");

            Console.WindowHeight = 70;

            while (mainMenu.DisplayMenu())
            {
                switch (mainMenu.Choice)
                {
                    case 0: // Registrera ny student 
                        var newStudent = new Student();
                        Ux.EditStudent(newStudent, courseRepo.GetAll(), studentRepo);
                        if (newStudent != null)
                        {
                            studentRepo.Add(newStudent);
                        }
                        break;

                    case 1: // Lista alla studenter

                        var students = studentRepo.GetAll();

                        
                        var studentListMenu = new Menu<Student>(students, "Studentlista");
                        if (studentListMenu.DisplayMenu(searchable: true))
                        {
                            Console.Clear();

                            // Redigera student
                            var studentToEdit = studentListMenu.Choices[studentListMenu.Choice];
                            var editedStudent = Ux.EditStudent(studentToEdit, courseRepo.GetAll(), studentRepo); 
                            if(editedStudent != null)
                            {
                                studentRepo.Update(editedStudent);
                            }
                        }
                        break;

                    case 2: // Visa klasser
                        Ux.ShowCourses(courseRepo, studentRepo);
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

                        courseRepo.Add(newCourse);
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