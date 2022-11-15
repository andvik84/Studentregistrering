using Studentregistrering.Models;

namespace Studentregistrering.UserExperience
{
    internal class Menu<T> where T : IMenuOption
    {
        public List<T> Choices { get; set; } = new List<T>();
        private List<T> originalChoices = null!;
        public int Choice { get; set; }
        public string? Header { get; set; }

        public Menu()
        {
        }

        public Menu(List<T> choices, string header = "Studentregister")
        {
            Choices = choices;
            originalChoices = choices;
            Header = header;
        }

        public void UpdateMenuOptions(List<T> choices)
        {
            Choices = choices;
        }

        public bool Exit { get; set; } = false;

        public bool DisplayMenu(int choice = 0, bool searchable = false)
        {
            if (Exit)
                return false;

            Choice = choice;
            ConsoleKeyInfo consoleKey;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Header);

                Console.WriteLine("---------------------------------");
                for (int i = 0; i < Choices.Count; i++)
                {
                    if (i == Choice)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($" >{Choices[i].MenuText}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine($"  {Choices[i].MenuText}");
                    }
                }
                Console.WriteLine("---------------------------------");
                if (typeof(T) == typeof(Student))
                    Console.WriteLine("A-Z för att söka efternamn \nsom börjar på bokstaven.");
                Console.WriteLine("Esc för att gå tillbaka");
                consoleKey = Console.ReadKey();


                if (consoleKey.Key == ConsoleKey.DownArrow && Choice + 1 < Choices.Count)
                {
                    Choice++;
                }

                if (consoleKey.Key == ConsoleKey.UpArrow && Choice - 1 >= 0)
                {
                    Choice--;
                }


                if (consoleKey.Key == ConsoleKey.Escape)
                {
                    return false;
                }

                if (consoleKey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    return true;
                }

                if (searchable && consoleKey.Key >= ConsoleKey.A && consoleKey.Key <= ConsoleKey.Z)
                {
                    Choices = originalChoices.Where(c => (c as IMenuOption).SearchableText.StartsWith(consoleKey.KeyChar.ToString().ToUpper())).ToList();
                }
            }
        }
    }
}
