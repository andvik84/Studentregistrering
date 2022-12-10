namespace Studentregistrering
{
    public static class Helpers
    {
        public static int ReadInt(string inputText, bool clear = false)
        {
            string error = string.Empty;

            while (true)
            {
                if (error != string.Empty)
                {
                    Console.WriteLine(error);
                    Console.ReadKey(true);
                }
                if (clear)
                    Console.Clear();
                Console.WriteLine(inputText);
                if (Int32.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    error = "Not a valid integer, try again";
                }
            }
        }

        public static DateTime ReadDateTime(string inputText, bool clear = false)
        {
            string error = string.Empty;

            while (true)
            {
                if (error != string.Empty)
                {
                    Console.WriteLine(error);
                    Console.ReadKey(true);
                }
                if (clear)
                    Console.Clear();
                Console.WriteLine(inputText);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime result))
                {
                    return result;
                }
                else
                {
                    error = "Not a valid datetime, try again";
                }
            }
        }

        public static string ReadString(string inputText, bool clear = false)
        {
            string error = string.Empty;
            string result;

            while (true)
            {
                if (error != string.Empty)
                {
                    Console.WriteLine(error);
                    Console.ReadKey(true);
                }
                if (clear)
                    Console.Clear();
                Console.Write(inputText);
                result = Console.ReadLine() ?? string.Empty;
                if (!String.IsNullOrWhiteSpace(result))
                {
                    return result;
                }
                else
                    error = "Not a valid string, try again.";
            }
        }

        
    }
}
