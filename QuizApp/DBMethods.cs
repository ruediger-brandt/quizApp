using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    internal class DBMethods
    {
        //finde ich so nicht besonders gut den hier nochmal reinzuschreiben
        private static readonly string con = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QDB.db");
        private static string connectionString = $"Data Source={con};Version=3;";
        public static string[] GetQuestionForMCQuiz(string difficulty)
        {
            string getQuestionQuery = "SELECT question_text, answer_a, answer_b, answer_c, answer_d, correct_answer, difficulty ";
            getQuestionQuery += "FROM Questions WHERE difficulty = ";
            getQuestionQuery += difficulty;
            getQuestionQuery += " ORDER BY RANDOM() LIMIT 1";
            try {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand getAQuestion = new SQLiteCommand(getQuestionQuery, connection))
                    {
                        using (SQLiteDataReader reader = getAQuestion.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string questionText = reader["question_text"].ToString();
                                string answerA = reader["answer_a"].ToString();
                                string answerB = reader["answer_b"].ToString();
                                string answerC = reader["answer_c"].ToString();
                                string answerD = reader["answer_d"].ToString();
                                string correctAnswer = reader["correct_answer"].ToString();
                                string questionDifficulty = reader["difficulty"].ToString();

                                string[] questionData = { questionText, answerA, answerB, answerC, answerD, correctAnswer, questionDifficulty };
                                return questionData;
                            }
                            return null;
                        }
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public static void WriteSaveState(string playerName, bool audience, bool fifty, bool skip, int level)
        {
            string getQuestionQuery = "INSERT INTO saveStates (player_name, audience_joker, fifty_joker, skip_joker, level) ";
            getQuestionQuery += "VALUES (' ";
            getQuestionQuery += (playerName + "','" + audience + "','" + fifty + "','" + skip + "','" + level + "') ");
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand writeASave = new SQLiteCommand(getQuestionQuery, connection))
                {
                    writeASave.ExecuteNonQuery();
                }
            connection.Close();
            }        
        }
        public static string[] GetSaveState(string playerName)
        {
            string getQuestionQuery = "SELECT player_name, audience_joker, fifty_joker, skip_joker, level ";
            getQuestionQuery += "FROM saveStates WHERE player_name LIKE '%";
            getQuestionQuery += playerName;
            getQuestionQuery += "%' ORDER BY save_id DESC LIMIT 1";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand getASave = new SQLiteCommand(getQuestionQuery, connection))
                {
                    using (SQLiteDataReader reader = getASave.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string questionText = reader["player_name"].ToString();
                            string answerA = reader["audience_joker"].ToString();
                            string answerB = reader["fifty_joker"].ToString();
                            string answerC = reader["skip_joker"].ToString();
                            string answerD = reader["level"].ToString();

                            string[] questionData = { questionText, answerA, answerB, answerC, answerD};
                            return questionData;
                        }
                        return null;
                    }
                }
            }
        }

        public static void AddHighscore(string playerName, string score) 
        {
            string getQuestionQuery = "INSERT INTO Highscores (player_name, score) ";
            getQuestionQuery += "VALUES (' ";
            getQuestionQuery += (playerName + "'," + int.Parse(score) + ") ");
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand writeASave = new SQLiteCommand(getQuestionQuery, connection))
                {
                    writeASave.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static string[][] GetHighscoreTable()
        {
            string getQuestionQuery = "SELECT player_name, score ";
            getQuestionQuery += "FROM Highscores ";
            getQuestionQuery += " ORDER BY score DESC LIMIT 15";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand getASave = new SQLiteCommand(getQuestionQuery, connection))
                {
                    using (SQLiteDataReader reader = getASave.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<string[]> highscoreList = new List<string[]>();

                            while (reader.Read())
                            {
                                string[] rowData = new string[]
                                {
                            reader["player_name"].ToString(),
                            reader["score"].ToString()
                                };

                                highscoreList.Add(rowData);
                            }

                            return highscoreList.ToArray();
                        }
                        return null;
                    }
                }
            }
        }
    }
}
