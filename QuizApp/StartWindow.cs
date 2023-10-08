using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    internal class StartWindow
    {
        public static string playerName;
        //zentriert einen String UI helper
        public static string centeredString(string message)
        {
            int padding = Math.Max(0,(Console.WindowWidth - message.Length) / 2);
            return new string(' ', padding) + message;
        }
        //Tut was der Name sagt UI helper
        public static void printEmptyLines(int number)
        {
            for(int i = 0; i < number; i++)
            {
                Console.WriteLine();
            }
        }

        //prüft auf erlaubte Eingabe @param ein Array mit den erlaubten Eingaben, die zu überprüfende Eingabe
        public static Boolean isValidInput(string[] allowed, string input)
        {
            foreach (string s in allowed)
            {
                if (s.Equals(input))
                {
                    return true;
                }
            }
            return false;
        }
        public static Boolean isValidInput(string input, int maxLength)
        {
            if(input.Length > maxLength)
            {
                return false;
            }
            return true;
        }

        // Erstellt das Navigationsmenü
        public static void CreateMenue()
        {   
            Console.Clear();
            string[] validSelection = { "1", "2", "3", "4" };
            printEmptyLines(8);
            Console.WriteLine(centeredString("1: Start new Quiz!"));
            Console.WriteLine();
            Console.WriteLine(centeredString("2: Resume Quiz!"));
            Console.WriteLine();
            Console.WriteLine(centeredString("3: Highscores!"));
            Console.WriteLine();
            Console.WriteLine(centeredString("4: End Game!"));
            printEmptyLines(3);
            Console.WriteLine(centeredString("Please select the desired menu item by entering the corresponding number!"));
            string selection = Console.ReadLine();
            if(isValidInput(validSelection, selection))
            {
                switch (selection)
                {
                    case "1":
                        var newQuiz = new MCQuizLogic();
                        newQuiz.RunMCQuiz();
                        break;
                    case "2":
                        string[] getArray = DBMethods.GetSaveState(playerName);
                        if(getArray == null) { 
                            Console.WriteLine(centeredString("No save state for this player was found. Press any key to continue!"));
                            Console.ReadKey();
                            Console.Clear();
                            CreateMenue();
                            break;
                        }
                        Console.WriteLine(centeredString("Save state was found. Press any key to load!"));
                        Console.ReadKey();
                        bool audience;
                        if (getArray[1].Equals("true")){
                            audience = true;
                        }
                        else {  audience = false;}
                        bool fifty;
                        if (getArray[2].Equals("true"))
                        {
                            fifty = true;
                        }
                        else { fifty = false; }
                        bool skip;
                        if (getArray[3].Equals("true"))
                        {
                            skip = true;
                        }
                        else { skip = false; }
                        int level = int.Parse(getArray[4]);
                        var resumeQuiz = new MCQuizLogic(level + 1,fifty,audience,skip);
                        resumeQuiz.RunMCQuiz();
                        break;
                    case "3":
                        QuizUI.HighscorePage();
                        break;
                    case "4": ExitControl(CreateMenue);
                        break;

                }
            }
            else 
            { 
                Console.WriteLine(centeredString("Please enter a valid number! Press any key to try again") );
                Console.ReadKey();
                Console.Clear();
                CreateMenue();
            }
            

        }

        // Einstiegspunkt der App für den User
        // Ruft CreateMenue auf

        public static void CreateWelcomeScreen()
        {
           
                string quizLogo = @"
   _______ 
  /       \
 |         |
 |    Q    |
 |         |
  \_______/
";
            printEmptyLines(3);
            String welcomeMessage = "Welcome to Quiz Champion!";


            Console.WriteLine(centeredString(welcomeMessage));
            printEmptyLines(7);
            // Calculate the left padding to center-align the logo.
            int leftPadding = (Console.WindowWidth - quizLogo.IndexOf('\n') - 10) / 2;

            // Split the logo into lines and add left padding to each line.
            string[] logoLines = quizLogo.Split('\n');
            foreach (string line in logoLines)
            {
                Console.WriteLine(new string(' ', leftPadding) + line);
            }
            printEmptyLines(7);
            Console.WriteLine(centeredString("Please enter your player name!"));
            string entry = Console.ReadLine();
            if (isValidInput(entry, 25))
            {
                playerName = entry.ToLower().Trim();
            }
            else
            {
                CreateWelcomeScreen();
            }
            String Continue = "Press any key to continue "+ entry + "!";
            Console.WriteLine(centeredString(Continue));
            Console.ReadKey();
            Console.Clear();
            CreateMenue();
        }

        // Wirklich App verlassen @param die Funktion von der die ExitControl aufgerufen wird, oder 0 wenn keine übergeben werden soll
        // Wenn keine Funktion übergeben wird, cleared die Funktion nur die letzte Zeile und macht sonst weiter nix
        // Ansonsten wird die übergebene Funktion aufgerufen
        // Hilft vielleicht später noch
        public static void ExitControl(Action sourceFunction, int check = 1)
        {

            Console.WriteLine(centeredString("Are you sure you want to exit? Yes: Press (Y) No: Press any key!"));
            string input = Console.ReadLine().ToLower();
            if (input.Equals("y"))
            {
                Environment.Exit(0);
            }
            else 
            {
                if (check == 1)
                {
                    Console.Clear();
                    sourceFunction();
                }
                else
                {

                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
            }
        }
    }
}
