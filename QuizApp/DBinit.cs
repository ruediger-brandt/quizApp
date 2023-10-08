using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace QuizApp
{
    internal class DBInit
    {
        public DBInit(string dbPath)
        {
            DbPath = dbPath;
            connectionString = $"Data Source={DbPath};Version=3;";
        }
        public string DbPath { get; set; }
        private string connectionString;
        

        //Gefällt mir so eigentlich nicht
        //Vielleicht CSV import bleibt aber erstmal so
        //DB anlegen, Tabellen anlegen, Datensätze einfügen
        //Nur wenn es noch keine DB gibt
        public void InitializeDatabase()
        {

            if (!DatabaseExists(connectionString))
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS questions (
                question_id INTEGER PRIMARY KEY AUTOINCREMENT,
                question_text TEXT NOT NULL,
                answer_a TEXT NOT NULL,
                answer_b TEXT NOT NULL,
                answer_c TEXT NOT NULL,
                answer_d TEXT NOT NULL,
                correct_answer CHAR(1) NOT NULL,
                difficulty INTEGER NOT NULL
            )";

                    string fillTableQuery = @"
                INSERT INTO questions (question_text, answer_a, answer_b, answer_c, answer_d, correct_answer, difficulty)
                VALUES
                ('What is the capital of France?', 'Paris', 'London', 'Berlin', 'Madrid', 'A', 1),
                ('Which planet is known as the Red Planet?', 'Mars', 'Venus', 'Earth', 'Jupiter', 'A', 1)
                
                ";

                    using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
                    {
                        createTableCommand.ExecuteNonQuery();
                    }
                    using(SQLiteCommand fillDatabase = new SQLiteCommand(fillTableQuery, connection))
                    { 
                        fillDatabase.ExecuteNonQuery();
                    }

                    string createTableQuerySaves = @"
            CREATE TABLE IF NOT EXISTS saveStates (
                save_id INTEGER PRIMARY KEY AUTOINCREMENT,
                player_name TEXT NOT NULL,
                audience_joker BOOLEAN NOT NULL,
                fifty_joker BOOLEAN NOT NULL,
                skip_joker BOOLEAN NOT NULL,
                level INTEGER NOT NULL
            )";

                    using (SQLiteCommand createTableSaves = new SQLiteCommand(createTableQuerySaves, connection))
                    {
                        createTableSaves.ExecuteNonQuery();
                    }


                
                connection.Close();
                }
            }
        }

        private bool DatabaseExists(string dbPath)
        {
            return File.Exists(dbPath); 
        }

    }
}
