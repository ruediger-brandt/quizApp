using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    internal class Question

        // Klasse zum erzeugen von Fragen Objekten zur späteren Nutzung
        // Sollte eigentlich direkt zur Tabellenstruktur passen sodass dem Konstruktur ziemlich direkt
        // die DB Query Ergebnisse übergeben werden können
    {
        public Question(string questionText, string answerA, string answerB, string answerC, string answerD, string correctAnswer, string difficulty)
        {
            QuestionText = questionText;
            AnswerA = answerA;
            AnswerB = answerB;
            AnswerC = answerC;
            AnswerD = answerD;
            CorrectAnswer = correctAnswer;
            Difficulty = difficulty;
        }
        // Constructor overloading vielleicht später für eine Quizvariation mit Freitextantworten
        public Question(string questionText, string correctAnswerLong) 
        { 
            QuestionText = questionText;
            CorrectAnswerLong = correctAnswerLong;
        }

        public string AnswerA { get; }
        public string AnswerB { get; }
        public string AnswerC { get;} 
        public string AnswerD { get; }
        public string CorrectAnswer { get;}
        public string Difficulty { get; } 
        public string QuestionText { get; }
        public string CorrectAnswerLong { get; }
    }
}
