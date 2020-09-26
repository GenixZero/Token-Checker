using System;
using System.IO;
using System.Net;

namespace TokenChecker {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Made by GenixZero | https://github.com/genixzero");
            Console.WriteLine("");
            Console.WriteLine("What is the tokens file name? ");

            string fileName = Console.ReadLine();

            if (!File.Exists(fileName)) {
                Console.WriteLine();
                Console.WriteLine("That file does not exist");
                while (true) {
                    Console.ReadLine();
                }
            }

            Console.WriteLine();

            if (!File.Exists("working.txt")) {
                File.Create("working.txt");
            } else if (File.ReadAllText("working.txt").Length > 0) {
                Console.WriteLine("Do you want to erase all the text in working.txt? [Y/N]");
                if (Console.ReadLine().ToUpper().Equals("Y")) {
                    File.WriteAllText("working.txt", "");
                }
            }

            Console.WriteLine();
            string[] lines = File.ReadAllLines(fileName);

            foreach (string token in lines) {
                if (check(token)) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Working Token - " + token);

                    File.AppendAllText("working.txt", token + "\n");
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Token - " + token);
                }
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Finished Checking. Your working tokens are in working.txt");

            while (true) {
                Console.ReadLine();
            }
        }

        private static bool check(String token) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v8/users/@me");

            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 Safari/537.36";
            request.Headers.Add("authorization", token);

            try {
                request.GetResponse();
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}
