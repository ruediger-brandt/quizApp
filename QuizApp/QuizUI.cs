using QuizApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class QuizUI : MCQuizLogic
    // jetzt neu mit Instanzmethoden und Properties um passing von Unmengen an Parametern zu vermeiden
{
    public string[] ValidEntries { get; set; }

    private static string TakeInputandCallVerify(string[] validEntries)
    {
        string entry = Console.ReadLine().ToLower();
        if (StartWindow.isValidInput(validEntries, entry))
        {
            return entry;
        }
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.WriteLine(StartWindow.centeredString("Please enter a valid number! Press any key to try again"));
        Console.ReadKey();
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
        entry = TakeInputandCallVerify(validEntries);
        return entry;
    }

    public void CreateQuizWelcomeScreen()
    {
        StartWindow.printEmptyLines(7);
        Console.WriteLine(StartWindow.centeredString("Welcome to Quizmaster!"));
        StartWindow.printEmptyLines(6);
        Console.WriteLine(StartWindow.centeredString("Prepare your brain for the ultimative quizzing experience!"));
        StartWindow.printEmptyLines(6);
        Console.WriteLine(StartWindow.centeredString("Press any key to start the quiz!"));
        Console.ReadKey();
    }

    public bool CreateQuestionScreen(Question qObject)
    {
        string [] validEntries = { "a", "b", "c", "d", "5", "6", "7" };
        ValidEntries = validEntries;
        int count = 0;
        string Joker = "";
        if (Audience) { Joker += "AUDIENCE(Enter 5)" + "\t"; count++; }
        else { ValidEntries[4] = "---------------"; }
        if (Fifty) { Joker += "FIFTYFIFTY(Enter 6)" + "\t"; count++; }
        else { ValidEntries[5] = "---------------"; }
        if (ExtraTime) { Joker += "SKIP QUESTION(Enter 7)" + "\t"; count++; }
        else { ValidEntries[6] = "---------------"; }
        if (count % 2 == 0) { Joker = Joker + "\t"; }
        Console.Clear();
        StartWindow.printEmptyLines(1);
        Console.WriteLine(StartWindow.centeredString("----------Level: " + qObject.Difficulty + " Euro Question----------"));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString(qObject.QuestionText));
        StartWindow.printEmptyLines(5);
        Console.WriteLine(new string(' ', 12) + "Choice A: " + qObject.AnswerA);
        Console.WriteLine();
        Console.WriteLine(new string(' ', 12) + "Choice B: " + qObject.AnswerB);
        Console.WriteLine();
        Console.WriteLine(new string(' ', 12) + "Choice C: " + qObject.AnswerC);
        Console.WriteLine();
        Console.WriteLine(new string(' ', 12) + "Choice D: " + qObject.AnswerD);
        StartWindow.printEmptyLines(2);
        Console.WriteLine(StartWindow.centeredString("Available Jokers!:"));
        Console.WriteLine(StartWindow.centeredString(Joker));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString("Please enter your Choice. Decide wisely!"));
        string entry = TakeInputandCallVerify(validEntries);
        entry = JokerSwitch(entry, qObject);

        if (qObject.CorrectAnswer.ToLower().Equals(entry.ToLower()))
        {
            return true;
        }
        return false;
    }

    public string JokerSwitch(string entry, Question qObject)
    {
        switch (entry)
        {
            case "5":
                return AudienceJoker(qObject);

            case "6":
                return FiftyFifty(qObject);
            case "7":
                return SkipQuestion(qObject);
            default:
                return entry;
        }
    }

    public string AudienceJoker(Question qObject)
    {
        // Audience Joker Start
        // Wir erfinden irgendwelche Abstimmungsergebnisse, die mit zunehmender Schwierigkeit bescheuerter werden
        // Wir nehmen an das Frage 1 alle wissen und deswegen alle die richtige Lösung abstimmen
        // Das sieht aber zu schön aus deswegen bauen wir nen Fehler ein der auch mit jeder Frage größer wird und mischen
        // noch ne Zufallszahl rein
        // funktioniert nicht so gut
        double factor = (double)Level / 10 + 0.2;
        Audience = false;
        ValidEntries[4] = "---------------";
        var votes = new Dictionary<string, double>();
        string[] availOpt = { "A", "B", "C", "D" };
        string[] done = new string[4];
        votes["A"] = 0;
        votes["B"] = 0;
        votes["C"] = 0;
        votes["D"] = 0;
        votes[qObject.CorrectAnswer] = 100;
        Random randomizer = new Random();
        double randFactor = randomizer.Next(-10, 1) * factor;
        double firstResult = Math.Round(Math.Max(0, Math.Min(votes[qObject.CorrectAnswer] + randFactor, 100)), 0);
        votes[qObject.CorrectAnswer] = firstResult;
        done[0] = qObject.CorrectAnswer;
        availOpt = availOpt.Except(done).ToArray();
        int nextVote = randomizer.Next(0, 3);
        randFactor = randomizer.Next(-10, 10) * factor;
        double secondResult = Math.Round(Math.Max(0, Math.Min(((100 - firstResult) / 3) + randFactor, 100 - firstResult)), 0);
        votes[availOpt[nextVote]] = secondResult;
        done[1] = availOpt[nextVote];
        availOpt = availOpt.Except(done).ToArray();
        nextVote = randomizer.Next(0, 2);
        randFactor = randomizer.Next(-10, 10) * factor;
        double thirdResult = Math.Round(Math.Max(0, Math.Min(((100 - firstResult - secondResult) / 2) + randFactor, 100 - firstResult - secondResult)), 0);
        votes[availOpt[nextVote]] = thirdResult;
        done[2] = availOpt[nextVote];
        availOpt = availOpt.Except(done).ToArray();
        votes[availOpt[0]] = 100 - firstResult - secondResult - thirdResult;
        // votes erfinden done
        // UI anpassen
        int count = 0;
        string Joker = "";
        if (Audience) { Joker += "AUDIENCE(Enter 5)" + "\t"; count++; }
        else { ValidEntries[4] = "---------------"; }
        if (Fifty) { Joker += "FIFTYFIFTY(Enter 6)" + "\t"; count++; }
        else { ValidEntries[5] = "---------------"; }
        if (ExtraTime) { Joker += "SKIP QUESTION(Enter 7)" + "\t"; count++; }
        else { ValidEntries[6] = "---------------"; }
        if (count % 2 == 0) { Joker = Joker + "\t"; }
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.WriteLine(StartWindow.centeredString("Results of the audience survey:"));
        Console.WriteLine(StartWindow.centeredString("Choice A: " + votes["A"] + "%" + "Choice B: " + votes["B"] + "%" + "Choice C: " + votes["C"] + "%" + "Choice D: " + votes["D"] + "%"));
        StartWindow.printEmptyLines(1);
        Console.WriteLine(StartWindow.centeredString("Please enter your Choice. Decide wisely!"));
        string entry = TakeInputandCallVerify(ValidEntries);
        return JokerSwitch(entry, qObject);
    }

    public string FiftyFifty(Question qObject)
    {
        //50/50 start ne Menge Magic um (pseudo)zufällig eine Antwort außer der richtigen auszuwählen die angezeigt wird
        //gleichzeitig aber dann die Möglichkeit wegzunehmen nicht mehr vorhandene Items auszuwählen.
        //
        Fifty = false;
        ValidEntries[5] = "---------------";
        string[] Answers = { qObject.AnswerA, qObject.AnswerB, qObject.AnswerC, qObject.AnswerD };
        int[] avail = new int[3];
        string[] build = { "Choice A: ", "Choice B: ", "Choice C: ", "Choice D: " };
        int correct = 999;
        Random random = new Random();
        int randomNumber = random.Next(0, 3);
        int count = 0;
        string Joker = "";
        if (Audience) { Joker += "AUDIENCE(Enter 5)" + "\t"; count++; }
        else { ValidEntries[4] = "---------------"; }
        if (Fifty) { Joker += "FIFTYFIFTY(Enter 6)" + "\t"; count++; }
        else { ValidEntries[5] = "---------------"; }
        if (ExtraTime) { Joker += "SKIP QUESTION(Enter 7)" + "\t"; count++; }
        else { ValidEntries[6] = "---------------"; }
        // Versuch den Quatsch ordentlich zu formatieren, ganz ok...nicht 100%
        if (count % 2 == 0) { Joker = Joker + "\t"; }
        switch (qObject.CorrectAnswer)
        {
            case "A":
                avail[0] = 1;
                avail[1] = 2;
                avail[2] = 3;
                correct = 0;
                break;
            case "B":
                avail[0] = 0;
                avail[1] = 2;
                avail[2] = 3;
                correct = 1;
                break;
            case "C":
                avail[0] = 0;
                avail[1] = 1;
                avail[2] = 3;
                correct = 2;
                break;
            case "D":
                avail[0] = 0;
                avail[1] = 1;
                avail[2] = 2;
                correct = 3;
                break;
        }
        int[] availRand = { correct, avail[randomNumber] };
        int[] notAvail = avail.Except(availRand).ToArray();
        ValidEntries[notAvail[0]] = "---------------";
        ValidEntries[notAvail[1]] = "---------------";
        int nRand = random.Next(0, 2);
        string[] unsortedRandomized = { new string(' ', 12) + build[availRand[nRand]] + Answers[availRand[nRand]], new string(' ', 12) + build[availRand[1 - nRand]] + Answers[availRand[1 - nRand]] };
        Array.Sort(unsortedRandomized);
        // ab hier Screen mit neuen Parametern aufbauen
        Console.Clear();
        StartWindow.printEmptyLines(1);
        Console.WriteLine(StartWindow.centeredString("----------Level: " + qObject.Difficulty + " Euro Question----------"));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString(qObject.QuestionText));
        StartWindow.printEmptyLines(7);
        Console.WriteLine(unsortedRandomized[0]);
        Console.WriteLine();
        Console.WriteLine(unsortedRandomized[1]);
        StartWindow.printEmptyLines(7);
        Console.WriteLine(StartWindow.centeredString("Available Jokers!:"));
        Console.WriteLine(StartWindow.centeredString(Joker));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString("Please enter your Choice. Decide wisely!"));
        string entry = TakeInputandCallVerify(ValidEntries);
        return JokerSwitch(entry, qObject);
    }

    public string SkipQuestion(Question qObject)
    {
        ExtraTime = false;
        ValidEntries[6] = "---------------";
        return qObject.CorrectAnswer;
    }

    public void CorrectAnswerScreen(Question qObject)
    {
        string[] validSelection = { "1", "2", "3"};
        Console.Clear();
        StartWindow.printEmptyLines(4);
        Console.WriteLine(StartWindow.centeredString("Question:"));
        Console.WriteLine(StartWindow.centeredString(qObject.QuestionText));
        Console.WriteLine();
        Console.WriteLine(StartWindow.centeredString("Correct Answer:"));
        Console.WriteLine(StartWindow.centeredString(qObject.CorrectAnswer));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString("Congratulation this was the correct Answer!"));
        Console.WriteLine(StartWindow.centeredString("Congratulation " + StartWindow.playerName + " you have won " + qObject.Difficulty + " Euros!"));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString("How would you like to continue? Choose the corresponding number!"));
        Console.WriteLine(StartWindow.centeredString("Continue (Press 1) \t Save Game(Press 2)\t End Game(3)"));
        string selection = TakeInputandCallVerify(validSelection);
        switch (selection)
            {
                case "1":
                    break;
                case "2":
                DBMethods.WriteSaveState(StartWindow.playerName, Audience, Fifty, ExtraTime, Level);
                Console.WriteLine(StartWindow.centeredString("Saved! Press any key"));
                Console.ReadKey();
                Console.Clear();
                CorrectAnswerScreen(qObject);
                break;
                case "3":
                    StartWindow.ExitControl(doNothing,0);
                    CorrectAnswerScreen(qObject);
                break;   
            }
    }

    public void WrongAnswerScreen(Question qObject)
    {
        string[] validSelection = { "1", "2", "3" };
        Console.Clear();
        StartWindow.printEmptyLines(4);
        Console.WriteLine(StartWindow.centeredString("Question:"));
        Console.WriteLine(StartWindow.centeredString(qObject.QuestionText));
        Console.WriteLine();
        Console.WriteLine(StartWindow.centeredString("Correct Answer:"));
        Console.WriteLine(StartWindow.centeredString(qObject.CorrectAnswer));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString("Sorry this was not the correct Answer!"));
        Console.WriteLine(StartWindow.centeredString("Congratulation "+ StartWindow.playerName+" you have won " + MCQuizLogic.difficulty[Level - 1] + " Euros!"));
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString("How would you like to continue? Choose the corresponding number!"));
        Console.WriteLine(StartWindow.centeredString("To Main Menue (Press 1) \t View Highscores(Press 2)\t End Game(3)"));
        string selection = TakeInputandCallVerify(validSelection);
        switch (selection)
        {
            case "1":
                StartWindow.CreateMenue();
                break;
            case "2":
                HighscorePage();
                break;
            case "3":
                StartWindow.ExitControl(doNothing, 0);
                WrongAnswerScreen(qObject);
                break;
        }
    }

    public static void HighscorePage()
    {
        Console.Clear();
        string[] validSelection = { "1", "2", "3" };
        string[][] scores = DBMethods.GetHighscoreTable();
        StartWindow.printEmptyLines(5);
        Console.WriteLine(StartWindow.centeredString("Highscores:"));
        for (int i = 0; i < scores.Length; i++)
        {
            Console.WriteLine(StartWindow.centeredString(scores[i][0] + ": \t\t" + scores[i][1]));
        }
        StartWindow.printEmptyLines(3);
        Console.WriteLine(StartWindow.centeredString("How would you like to continue? Choose the corresponding number!"));
        Console.WriteLine(StartWindow.centeredString("To Main Menue (Press 1) \t Stay (Press 2)\t End Game(3)"));
        string selection = TakeInputandCallVerify(validSelection);
        switch (selection)
        {
            case "1":
                StartWindow.CreateMenue();
                break;
            case "2":
                HighscorePage();
                break;
            case "3":
                StartWindow.ExitControl(doNothing, 0);
                break;

        }
    }
    public void WinScreen()
    {
        Console.Clear();
        Console.WriteLine("Party");
        Console.WriteLine(StartWindow.centeredString("Party Message!"));
        Console.ReadKey();
    }

    public static void doNothing() {}
}
