using System;

namespace decryptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Focus::Decryptor";
            ConsoleColor white = ConsoleColor.White;
            ConsoleColor red = ConsoleColor.Red;
            ConsoleColor green = ConsoleColor.Green;
            Console.WriteLine("FocusCrypt [Version 1.3.2.1]");
            Console.WriteLine("https://www.github.com/focuscrypt/decryptor");
            Console.ForegroundColor = green;
            Console.WriteLine("Women's Rights Matters.");
            Console.WriteLine();
            Console.ForegroundColor = white;
        start:
            Console.Write("Encrypted Text:");
            string etext = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(etext))
            {
                Console.ForegroundColor = red;
                Console.WriteLine("Please enter any text.");
                Console.ForegroundColor = white;
                Console.WriteLine();
                goto start;
            }
            Console.WriteLine();
        second:
            Console.Write("Password:");
            string pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            if (string.IsNullOrWhiteSpace(pass))
            {
                Console.ForegroundColor = red;
                Console.WriteLine("Please enter a password!");
                Console.ForegroundColor = white;
                Console.WriteLine();
                goto second;
            }
            Console.WriteLine();
            Console.ForegroundColor = green;
            Console.WriteLine("Ok.");
            Console.ForegroundColor = white;
            Console.WriteLine("The decryption process has started. Please wait...");
            Console.WriteLine("The decryption process is complete. Result:");
            Console.WriteLine();
            Console.ForegroundColor = green;
            Console.WriteLine("-----");
            Console.ForegroundColor = white;
            Console.WriteLine(Focus.Decrypt(etext, pass));
            Console.ForegroundColor = green;
            Console.WriteLine("-----");
            Console.WriteLine();
            Console.ForegroundColor = white;
            Console.Write("Do you want to do another decryption?(y/n)");
            string c = Console.ReadLine();
            if (c == "y")
            {
                Console.WriteLine();
                goto start;
            }
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
