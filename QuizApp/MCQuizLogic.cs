using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    internal class MCQuizLogic
    {
        // Parameter um savestate herzustellen
        public MCQuizLogic(int level = 0, bool fifty = true, bool audience = true, bool extraTime = true )
        {
            Level = level;
            Fifty = fifty;
            Audience = audience;
            ExtraTime = extraTime;
        }
// Wir tuhen mal so als hätten wir wirklich unterschiedlich schwere Fragen in der DB und die Fragen sind nicht ziemlich willkürlich
// auf Schwierigkeiten verteilt und sinnlos
        public static readonly string[] difficulty = { "50", "100", "200", "300", "500", "1000", "2000", "4000", "8000", "16000", "32000", "64000", "125000", "500000", "1000000" };
        public int Level { get; set; }
        public bool Fifty { get; set; }
        public bool Audience { get; set; }
        public bool ExtraTime { get; set; }
        public void RunMCQuiz()
        {
            // Fetched den Fragenkatalog. Erzeugt für jede Frage ein Objekt und speichert es für später in einer Hash Map
            // Dict initialisieren mit einer Question und nicht einem generic object...Sonst sucht man eine Stunde an allen möglichen Stellen 
            // nach Fehlern!
            Dictionary<string, Question> qObjects = new Dictionary<string, Question>();
            foreach (string x in difficulty)
            {
                string[] i = DBMethods.GetQuestionForMCQuiz(x);
                qObjects[x] = new Question(i[0], i[1], i[2], i[3], i[4], i[5], i[6]);
            }
            var quizUI = new QuizUI();
            // Quiz Startbildschirm laden
            Console.Clear();
            quizUI.CreateQuizWelcomeScreen();
            // Quizfrage laden
            // Das zu dem entsprechendem Level passende QuizFragen Objekt wird der UI übergeben
            while (Level < 15)
            {
                if (quizUI.CreateQuestionScreen(qObjects[difficulty[Level]]))
                {
                    quizUI.CorrectAnswerScreen(qObjects[difficulty[Level]]);
                    Level++;
                }
                else
                {
                    DBMethods.AddHighscore(StartWindow.playerName, difficulty[Math.Max(0, Level - 1)]);
                    quizUI.WrongAnswerScreen(qObjects[difficulty[Level]]);
                    // write Highscore
                    //Auswahl new game/end/main menu/highscores
                }
            }
            DBMethods.AddHighscore(StartWindow.playerName, "1000000");
            quizUI.WinScreen();
            // write Highscore
        }       
    }    
}
