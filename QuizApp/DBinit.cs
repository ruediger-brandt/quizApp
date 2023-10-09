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
        

        //DB anlegen, Tabellen anlegen, Datensätze einfügen
        //Nur wenn es noch keine DB gibt
        public void InitializeDatabase()
        {

            if (!File.Exists(DbPath))
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
('What is the capital of France?', 'Paris', 'London', 'Berlin', 'Madrid', 'A', '50'),
('Which planet is known as the Red Planet?', 'Mars', 'Venus', 'Earth', 'Jupiter', 'A', '50'),
('What is the largest planet in our solar system?', 'Mars', 'Venus', 'Jupiter', 'Saturn', 'C', '50'),
('Which gas do plants absorb from the atmosphere during photosynthesis?', 'Carbon dioxide', 'Oxygen', 'Hydrogen', 'Nitrogen', 'A', '50'),
('Who is the author of the famous play ""Romeo and Juliet""?', 'Charles Dickens', 'William Shakespeare', 'Jane Austen', 'Mark Twain', 'B', '50'),
('Which element is represented by the chemical symbol ""H""?', 'Helium', 'Hydrogen', 'Hafnium', 'Hassium', 'B', '50'),
('What is the capital city of Japan?', 'Beijing', 'Seoul', 'Tokyo', 'Shanghai', 'C', '50'),
('Which of the following is a primary color?', 'Green', 'Purple', 'Orange', 'Red', 'D', '50'),
('What is the chemical symbol for gold?', 'Go', 'Gd', 'Gl', 'Au', 'D', '50'),
('Which of these countries is not in Europe?', 'Spain', 'Italy', 'Brazil', 'Germany', 'C', '50'),
('What is the largest mammal in the world?', 'African elephant', 'Giraffe', 'Blue whale', 'Hippopotamus', 'C', '50'),
('Which gas makes up the majority of Earth\s atmosphere?', 'Oxygen', 'Carbon dioxide', 'Nitrogen', 'Helium', 'C', '50'),
('What is the largest organ in the human body?', 'Liver', 'Brain', 'Heart', 'Skin', 'D', '50'),
('What is the chemical symbol for silver?', 'Ag', 'Sv', 'Si', 'Sr', 'A', '50'),
('What is the smallest planet in our solar system?', 'Mercury', 'Venus', 'Mars', 'Earth', 'A', '50'),
('Which famous scientist is known for his theory of general relativity?', 'Isaac Newton', 'Niels Bohr', 'Galileo Galilei', 'Albert Einstein', 'D', '50'),
('What is the chemical symbol for oxygen?', 'O', 'X', 'Ox', 'Og', 'A', '50'),
('Which planet is known as the ""Morning Star"" or ""Evening Star""?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'A', '50'),
('What is the tallest mountain in the world?', 'Mount Kilimanjaro', 'Mount Everest', 'Mount Fuji', 'Mount McKinley', 'B', '50'),
('What is the chemical symbol for carbon?', 'Co', 'Cn', 'Ca', 'C', 'D', '50'),
('What is the currency of Japan?', 'Yuan', 'Peso', 'Euro', 'Yen', 'D', '50'),
('What is the chemical symbol for helium?', 'He', 'Hl', 'Ha', 'Hn', 'A', '50'),
('Which planet is known as the ""Red Planet""?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'B', '50'),
('What is the chemical symbol for hydrogen?', 'Hg', 'Hd', 'Hn', 'H', 'D', '50'),
('What is the capital of Italy?', 'Milan', 'Rome', 'Naples', 'Venice', 'B', '50'),
('What is the chemical symbol for iron?', 'Ir', 'Ie', 'In', 'Fe', 'D', '50'),
('What is the largest ocean on Earth?', 'Atlantic Ocean', 'Arctic Ocean', 'Indian Ocean', 'Pacific Ocean', 'D', '50'),
('What is the chemical symbol for sodium?', 'Sa', 'Sn', 'Si', 'Na', 'D', '50'),
('Which gas do humans exhale when they breathe?', 'Carbon monoxide', 'Hydrogen', 'Oxygen', 'Nitrogen', 'C', '50'),
('What is the chemical symbol for calcium?', 'Ca', 'Ce', 'Co', 'Cl', 'A', '50'),
('What is the largest bird in the world?', 'Eagle', 'Penguin', 'Ostrich', 'Flamingo', 'C', '50'),
('What is the capital of Germany?', 'Berlin', 'Paris', 'Madrid', 'Rome', 'A', '100'),
('Which planet is known as the ""Blue Planet""?', 'Earth', 'Mars', 'Venus', 'Jupiter', 'A', '100'),
('What is the smallest prime number?', '1', '2', '3', '4', 'B', '100'),
('What is the chemical symbol for nitrogen?', 'N', 'Ni', 'Na', 'Ne', 'A', '100'),
('Who wrote the play ""Hamlet""?', 'Leo Tolstoy', 'F. Scott Fitzgerald', 'William Shakespeare', 'Mark Twain', 'C', '100'),
('What is the atomic number of hydrogen?', '1', '2', '3', '4', 'A', '100'),
('What is the capital of Canada?', 'Ottawa', 'Toronto', 'Vancouver', 'Montreal', 'A', '100'),
('What is the largest desert in the world?', 'Sahara Desert', 'Arabian Desert', 'Gobi Desert', 'Kalahari Desert', 'A', '100'),
('What is the chemical symbol for potassium?', 'K', 'P', 'Pt', 'Po', 'A', '100'),
('Which gas do plants release during photosynthesis?', 'Carbon dioxide', 'Oxygen', 'Hydrogen', 'Nitrogen', 'B', '100'),
('Who is the author of ""Pride and Prejudice""?', 'Charles Dickens', 'Jane Austen', 'Leo Tolstoy', 'George Orwell', 'B', '100'),
('What is the chemical symbol for neon?', 'No', 'Nn', 'Ne', 'Na', 'C', '100'),
('What is the largest ocean in the world?', 'Atlantic Ocean', 'Indian Ocean', 'Arctic Ocean', 'Pacific Ocean', 'D', '100'),
('What is the chemical symbol for sulfur?', 'S', 'So', 'Sl', 'Si', 'A', '100'),
('What is the capital of China?', 'Beijing', 'Shanghai', 'Hong Kong', 'Taipei', 'A', '100'),
('What is the chemical symbol for copper?', 'Cu', 'Co', 'Cp', 'Cn', 'A', '100'),
('Which gas is known as the ""laughing gas""?', 'Oxygen', 'Nitrogen', 'Carbon dioxide', 'Nitrous oxide', 'D', '100'),
('What is the chemical symbol for zinc?', 'Zn', 'Zi', 'Za', 'Zo', 'A', '100'),
('What is the largest planet in our solar system?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'C', '100'),
('What is the chemical symbol for tin?', 'Ti', 'To', 'Tn', 'Sn', 'D', '100'),
('What is the capital of Australia?', 'Sydney', 'Melbourne', 'Canberra', 'Brisbane', 'C', '100'),
('What is the chemical symbol for lead?', 'Le', 'La', 'Ld', 'Pb', 'D', '100'),
('Which gas do humans inhale when they breathe?', 'Carbon monoxide', 'Nitrogen', 'Oxygen', 'Hydrogen', 'C', '100'),
('What is the chemical symbol for silver?', 'Ag', 'Sv', 'Si', 'Sr', 'A', '100'),
('What is the smallest planet in our solar system?', 'Venus', 'Earth', 'Mars', 'Mercury', 'D', '100'),
('Who is the inventor of the telephone?', 'Alexander Graham Bell', 'Thomas Edison', 'Albert Einstein', 'Isaac Newton', 'A', '100'),
('What is the chemical symbol for gold?', 'Go', 'Gd', 'Gl', 'Au', 'D', '100'),
('Which planet is known as the ""Red Planet""?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'B', '100'),
('What is the chemical symbol for helium?', 'He', 'Hl', 'Ha', 'Hn', 'A', '100'),
('What is the capital of Italy?', 'Milan', 'Rome', 'Naples', 'Venice', 'B', '100'),
('What is the chemical symbol for iron?', 'Ir', 'Ie', 'In', 'Fe', 'D', '100'),
('What is the largest ocean on Earth?', 'Atlantic Ocean', 'Indian Ocean', 'Arctic Ocean', 'Pacific Ocean', 'D', '100'),
('What is the chemical symbol for sodium?', 'Sa', 'Sn', 'Si', 'Na', 'D', '100'),
('What is the capital of Brazil?', 'Rio de Janeiro', 'Brasília', 'São Paulo', 'Salvador', 'B', '100'),
('What is the chemical symbol for calcium?', 'Ca', 'Ce', 'Co', 'Cl', 'A', '100'),
('What is the capital of Russia?', 'St. Petersburg', 'Moscow', 'Kiev', 'Minsk', 'B', '100'),
('What is the chemical symbol for hydrogen?', 'Hg', 'Hd', 'Hn', 'H', 'D', '100'),
('What is the currency of Canada?', 'Pound', 'Dollar', 'Yen', 'Canadian dollar', 'D', '100'),
('What is the chemical symbol for phosphorus?', 'Pa', 'Pe', 'Po', 'P', 'D', '100'),
('What is the largest bird in the world?', 'Eagle', 'Penguin', 'Ostrich', 'Flamingo', 'C', '64000'),
('What is the capital of Argentina?', 'Buenos Aires', 'São Paulo', 'Montevideo', 'Cordoba', 'A', '64000'),
('What is the chemical symbol for mercury?', 'Mg', 'Me', 'Mn', 'Hg', 'D', '100'),
('What is the highest mountain in Africa?', 'Mount Kilimanjaro', 'Mount Everest', 'Mount Fuji', 'Mount McKinley', 'A', '64000'),
('What is the chemical symbol for aluminum?', 'Al', 'Am', 'Ai', 'An', 'A', '100'),
('What is the capital of Egypt?', 'Cairo', 'Alexandria', 'Luxor', 'Aswan', 'A', '100'),
('What is the chemical symbol for chlorine?', 'Cl', 'Cr', 'Ci', 'Cn', 'A', '100'),
('What is the largest mammal in the world?', 'African elephant', 'Giraffe', 'Blue whale', 'Hippopotamus', 'C', '100'),
('Who wrote the play ""Romeo and Juliet""?', 'Jane Austen', 'Charles Dickens', 'Mark Twain', 'William Shakespeare', 'D', '200'),
('What is the chemical symbol for oxygen?', 'O', 'Ox', 'Oi', 'Om', 'A', '200'),
('What is the capital of India?', 'Delhi', 'Mumbai', 'Bangalore', 'Kolkata', 'A', '200'),
('What is the chemical symbol for carbon?', 'C', 'Co', 'Ca', 'Ce', 'A', '200'),
('What is the largest moon of Saturn?', 'Ganymede', 'Titan', 'Europa', 'Io', 'B', '200'),
('Who painted the Mona Lisa?', 'Vincent van Gogh', 'Pablo Picasso', 'Leonardo da Vinci', 'Michelangelo', 'C', '200'),
('What is the chemical symbol for nitrogen?', 'N', 'Ni', 'Na', 'Ne', 'A', '200'),
('Which gas makes up the majority of Earths atmosphere?', 'Oxygen', 'Carbon dioxide', 'Nitrogen', 'Hydrogen', 'C', '200'),
('What is the capital of South Africa?', 'Cape Town', 'Johannesburg', 'Durban', 'Pretoria', 'D', '200'),
('What is the largest mammal on Earth?', 'African elephant', 'Blue whale', 'Giraffe', 'Hippopotamus', 'B', '64000'),
('Who wrote the novel ""1984""?', 'George Orwell', 'F. Scott Fitzgerald', 'Aldous Huxley', 'J.R.R. Tolkien', 'A', '64000'),
('What is the chemical symbol for helium?', 'He', 'Hl', 'Ha', 'Hn', 'A', '200'),
('Which planet is known as the ""Red Planet""?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'B', '64000'),
('What is the capital of Japan?', 'Tokyo', 'Osaka', 'Kyoto', 'Nagoya', 'A', '200'),
('What is the chemical symbol for gold?', 'Go', 'Gd', 'Gl', 'Au', 'D', '200'),
('Which gas do plants absorb during photosynthesis?', 'Oxygen', 'Carbon dioxide', 'Hydrogen', 'Nitrogen', 'B', '64000'),
('What is the capital of Spain?', 'Lisbon', 'Barcelona', 'Madrid', 'Valencia', 'C', '200'),
('What is the chemical symbol for potassium?', 'K', 'P', 'Pt', 'Po', 'A', '200'),
('What is the tallest mountain in the world?', 'Mount Kilimanjaro', 'Mount Everest', 'Mount Fuji', 'Mount McKinley', 'B', '64000'),
('What is the chemical symbol for silver?', 'Ag', 'Sv', 'Si', 'Sr', 'A', '200'),
('What is the capital of Canada?', 'Ottawa', 'Toronto', 'Vancouver', 'Montreal', 'A', '200'),
('What is the chemical symbol for tin?', 'Ti', 'To', 'Tn', 'Sn', 'D', '200'),
('What is the largest planet in our solar system?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'C', '200'),
('What is the chemical symbol for lead?', 'Le', 'La', 'Ld', 'Pb', 'D', '200'),
('What is the capital of Brazil?', 'Rio de Janeiro', 'Brasília', 'São Paulo', 'Salvador', 'B', '200'),
('What is the chemical symbol for calcium?', 'Ca', 'Ce', 'Co', 'Cl', 'A', '200'),
('What is the highest waterfall in the world?', 'Niagara Falls', 'Angel Falls', 'Victoria Falls', 'Iguazu Falls', 'B', '200'),
('What is the chemical symbol for hydrogen?', 'Hg', 'Hd', 'Hn', 'H', 'D', '200'),
('What is the currency of Russia?', 'Ruble', 'Euro', 'Dollar', 'Pound', 'A', '200'),
('What is the chemical symbol for phosphorus?', 'Pa', 'Pe', 'Po', 'P', 'D', '200'),
('What is the largest reptile in the world?', 'Cobra', 'Tortoise', 'Crocodile', 'Komodo dragon', 'C', '200'),
('What is the capital of Egypt?', 'Cairo', 'Alexandria', 'Luxor', 'Aswan', 'A', '200'),
('What is the chemical symbol for chlorine?', 'Cl', 'Cr', 'Ci', 'Cn', 'A', '200'),
('What is the longest river in the world?', 'Amazon River', 'Nile River', 'Mississippi River', 'Yangtze River', 'B', '200'),
('What is the capital of Turkey?', 'Ankara', 'Istanbul', 'Izmir', 'Antalya', 'A', '200'),
('What is the chemical symbol for uranium?', 'Un', 'Ur', 'Ua', 'U', 'D', '200'),
('Who is the author of ""The Catcher in the Rye""?', 'J.D. Salinger', 'F. Scott Fitzgerald', 'Harper Lee', 'George Orwell', 'A', '300'),
('What is the largest organ in the human body?', 'Heart', 'Liver', 'Lungs', 'Skin', 'D', '300'),
('What is the boiling point of water in Fahrenheit?', '212°F', '100°C', '273K', '32°F', 'D', '300'),
('Who is the main character in ""To Kill a Mockingbird""?', 'Atticus Finch', 'Scout Finch', 'Jem Finch', 'Boo Radley', 'A', '300'),
('What is the largest planet in our solar system?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'C', '300'),
('What is the freezing point of water in Fahrenheit?', '32°F', '0°C', '273K', '212°F', 'A', '300'),
('What is the largest mammal on Earth?', 'African elephant', 'Blue whale', 'Giraffe', 'Hippopotamus', 'B', '300'),
('Who is the author of ""War and Peace""?', 'Charles Dickens', 'Fyodor Dostoevsky', 'Leo Tolstoy', 'Jane Austen', 'C', '300'),
('What is the capital of Australia?', 'Sydney', 'Melbourne', 'Canberra', 'Brisbane', 'C', '300'),
('What is the square root of 144?', '9', '10', '11', '12', 'A', '300'),
('Who is known as the ""Father of Physics""?', 'Albert Einstein', 'Isaac Newton', 'Galileo Galilei', 'Niels Bohr', 'B', '300'),
('What is the largest desert in the world?', 'Sahara Desert', 'Arabian Desert', 'Gobi Desert', 'Kalahari Desert', 'A', '300'),
('Who is the author of ""Moby-Dick""?', 'Herman Melville', 'Mark Twain', 'Edgar Allan Poe', 'Nathaniel Hawthorne', 'A', '300'),
('What is the chemical symbol for iron?', 'Ir', 'Ie', 'In', 'Fe', 'D', '300'),
('What is the highest mountain in North America?', 'Mount Kilimanjaro', 'Mount Everest', 'Mount McKinley', 'Mount Fuji', 'C', '300'),
('Who painted the Sistine Chapel ceiling?', 'Vincent van Gogh', 'Pablo Picasso', 'Leonardo da Vinci', 'Michelangelo', 'D', '300'),
('What is the largest moon of Saturn?', 'Ganymede', 'Titan', 'Europa', 'Io', 'B', '300'),
('What is the square root of 121?', '9', '10', '11', '12', 'C', '300'),
('What is the smallest planet in our solar system?', 'Venus', 'Earth', 'Mars', 'Mercury', 'D', '300'),
('Who wrote the play ""Hamlet""?', 'Leo Tolstoy', 'F. Scott Fitzgerald', 'William Shakespeare', 'Mark Twain', 'C', '300'),
('What is the chemical symbol for potassium?', 'K', 'P', 'Pt', 'Po', 'A', '300'),
('What is the capital of China?', 'Beijing', 'Shanghai', 'Hong Kong', 'Taipei', 'A', '300'),
('What is the largest ocean in the world?', 'Atlantic Ocean', 'Indian Ocean', 'Arctic Ocean', 'Pacific Ocean', 'D', '300'),
('Who is the author of ""Pride and Prejudice""?', 'Charles Dickens', 'Jane Austen', 'Leo Tolstoy', 'George Orwell', 'B', '300'),
('What is the square root of 169?', '9', '10', '11', '13', 'D', '300'),
('Who wrote the play ""Macbeth""?', 'Leo Tolstoy', 'F. Scott Fitzgerald', 'William Shakespeare', 'Mark Twain', 'C', '300'),
('What is the atomic number of carbon?', '1', '2', '3', '6', 'D', '300'),
('What is the capital of Canada?', 'Ottawa', 'Toronto', 'Vancouver', 'Montreal', 'A', '300'),
('What is the square root of 225?', '9', '10', '15', '20', 'C', '300'),
('Who is the author of ""The Great Gatsby""?', 'Jane Austen', 'F. Scott Fitzgerald', 'Harper Lee', 'George Orwell', 'B', '300'),
('What is the chemical symbol for neon?', 'No', 'Nn', 'Ne', 'Na', 'C', '300'),
('What is the square root of 196?', '9', '12', '14', '16', 'B', '300'),
('Who is the author of ""The Odyssey""?', 'Homer', 'Virgil', 'Dante', 'Shakespeare', 'A', '300'),
('What is the atomic number of oxygen?', '7', '8', '9', '10', 'B', '300'),
('What is the square root of 256?', '14', '15', '16', '17', 'C', '300'),
('Who wrote the novel ""Dracula""?', 'J.R.R. Tolkien', 'H.G. Wells', 'Bram Stoker', 'Mary Shelley', 'C', '300'),
('What is the chemical symbol for hydrogen?', 'Hg', 'Hd', 'Hn', 'H', 'D', '300'),
('What is the square root of 289?', '12', '15', '17', '19', 'B', '300'),
('Who is the author of ""Frankenstein""?', 'J.R.R. Tolkien', 'H.G. Wells', 'Bram Stoker', 'Mary Shelley', 'D', '300'),
('What is the atomic number of nitrogen?', '5', '6', '7', '8', 'C', '300'),
('What is the square root of 324?', '16', '18', '20', '22', 'A', '300'),
('Who is the author of ""The Grapes of Wrath""?', 'John Steinbeck', 'F. Scott Fitzgerald', 'Mark Twain', 'Ernest Hemingway', 'A', '300'),
('What is the chemical symbol for carbon dioxide?', 'CO2', 'C2', 'CO', 'C', 'A', '300'),
('What is the square root of 361?', '17', '19', '21', '23', 'A', '300'),
('Who is the author of ""The Shining""?', 'Stephen King', 'Edgar Allan Poe', 'H.P. Lovecraft', 'Bram Stoker', 'A', '300'),
('What is the atomic number of hydrogen?', '1', '2', '3', '4', 'A', '300'),
('What is the square root of 400?', '18', '20', '22', '24', 'A', '300'),
('Who wrote the play ""Romeo and Juliet""?', 'Jane Austen', 'Charles Dickens', 'Mark Twain', 'William Shakespeare', 'D', '300'),
('What is the chemical symbol for calcium?', 'Ca', 'Ce', 'Co', 'Cl', 'A', '300'),
('What is the square root of 441?', '19', '21', '23', '25', 'A', '300'),
('What is the smallest prime number?', '1', '2', '3', '4', 'B', '300'),
('Who is the author of ""The Hobbit""?', 'J.R.R. Tolkien', 'C.S. Lewis', 'J.K. Rowling', 'George Orwell', 'A', '300'),
('What is the atomic number of helium?', '1', '2', '3', '4', 'B', '300'),
('What is the square root of 484?', '20', '22', '24', '26', 'A', '300'),
('Who wrote the play ""Othello""?', 'Leo Tolstoy', 'F. Scott Fitzgerald', 'William Shakespeare', 'Mark Twain', 'C', '300'),
('What is the chemical symbol for silver?', 'Ag', 'Sv', 'Si', 'Sr', 'A', '300'),
('What is the square root of 529?', '21', '23', '25', '27', 'A', '300'),
('What is the atomic number of carbon?', '5', '6', '7', '8', 'B', '300'),
('Who is the author of ""One Hundred Years of Solitude""?', 'Gabriel García Márquez', 'Isabel Allende', 'Mario Vargas Llosa', 'Pablo Neruda', 'A', '300'),
('What is the largest planet in our solar system?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'C', '300'),
('What is the square root of 576?', '22', '24', '26', '28', 'A', '300'),
('What is the chemical symbol for neon?', 'No', 'Nn', 'Ne', 'Na', 'C', '300'),
('What is the atomic number of oxygen?', '7', '8', '9', '10', 'B', '300'),
('Who wrote the novel ""The Lord of the Rings""?', 'J.R.R. Tolkien', 'C.S. Lewis', 'J.K. Rowling', 'George Orwell', 'A', '300'),
('What is the square root of 625?', '23', '25', '27', '29', 'B', '300'),
('What is the largest moon of Saturn?', 'Ganymede', 'Titan', 'Europa', 'Io', 'B', '300'),
('What is the atomic number of nitrogen?', '5', '6', '7', '8', 'C', '300'),
('Who painted the Mona Lisa?', 'Vincent van Gogh', 'Pablo Picasso', 'Leonardo da Vinci', 'Michelangelo', 'C', '300'),
('What is the square root of 676?', '24', '26', '28', '30', 'A', '300'),
('Who is the main character in ""To Kill a Mockingbird""?', 'Atticus Finch', 'Scout Finch', 'Jem Finch', 'Boo Radley', 'A', '300'),
('What is the atomic number of helium?', '1', '2', '3', '4', 'B', '300'),
('What is the square root of 729?', '27', '30', '33', '36', 'A', '300'),
('Who wrote the novel ""Crime and Punishment""?', 'Leo Tolstoy', 'Fyodor Dostoevsky', 'Charles Dickens', 'Herman Melville', 'B', '300'),
('What is the boiling point of water in Fahrenheit?', '212°F', '100°C', '273K', '32°F', 'D', '300'),
('What is the atomic number of hydrogen?', '1', '2', '3', '4', 'A', '300'),
('What is the square root of 784?', '28', '30', '32', '34', 'A', '300'),
('Who is the author of ""The Picture of Dorian Gray""?', 'F. Scott Fitzgerald', 'Oscar Wilde', 'Jane Austen', 'Mark Twain', 'B', '300'),
('What is the capital of India?', 'Delhi', 'Mumbai', 'Bangalore', 'Kolkata', 'A', '300'),
('What is the atomic number of carbon?', '5', '6', '7', '8', 'B', '300'),
('What is the square root of 841?', '29', '31', '33', '35', 'A', '300'),
('Who is the author of ""The Road""?', 'J.K. Rowling', 'George Orwell', 'Ernest Hemingway', 'Cormac McCarthy', 'D', '300'),
('What is the largest desert in the world?', 'Sahara Desert', 'Arabian Desert', 'Gobi Desert', 'Kalahari Desert', 'A', '300'),
('What is the chemical symbol for sulfur?', 'S', 'So', 'Sl', 'Si', 'A', '300'),
('What is the square root of 900?', '30', '32', '34', '36', 'A', '300'),
('What is the largest planet in our solar system?', 'Venus', 'Mars', 'Jupiter', 'Saturn', 'C', '500'),
('What is the square root of 625?', '23', '25', '27', '29', 'B', '500'),
('What is the capital of India?', 'Delhi', 'Mumbai', 'Bangalore', 'Kolkata', 'A', '500'),
('What is the boiling point of water in Fahrenheit?', '212°F', '100°C', '273K', '32°F', 'D', '500'),
('What is the atomic number of hydrogen?', '1', '2', '3', '4', 'A', '500'),
('Who painted the Mona Lisa?', 'Vincent van Gogh', 'Pablo Picasso', 'Leonardo da Vinci', 'Michelangelo', 'C', '500'),
('What is the square root of 676?', '24', '26', '28', '30', 'A', '500'),
('What is the atomic number of nitrogen?', '5', '6', '7', '8', 'C', '500'),
('What is the largest moon of Saturn?', 'Ganymede', 'Titan', 'Europa', 'Io', 'B', '500'),
('What is the square root of 784?', '28', '30', '32', '34', 'A', '500'),
('What is the chemical symbol for sulfur?', 'S', 'So', 'Sl', 'Si', 'A', '500'),
('Who is the main character in ""To Kill a Mockingbird""?', 'Atticus Finch', 'Scout Finch', 'Jem Finch', 'Boo Radley', 'A', '500'),
('What is the square root of 900?', '30', '32', '34', '36', 'A', '500'),
('What is the largest desert in the world?', 'Sahara Desert', 'Arabian Desert', 'Gobi Desert', 'Kalahari Desert', 'A', '500'),
('What is the atomic number of carbon?', '5', '6', '7', '8', 'B', '500'),
('Who is the author of ""The Road""?', 'J.K. Rowling', 'George Orwell', 'Ernest Hemingway', 'Cormac McCarthy', 'D', '500'),
('What is the square root of 841?', '29', '31', '33', '35', 'A', '500'),
('What is the capital of Australia?', 'Melbourne', 'Sydney', 'Canberra', 'Brisbane', 'C', '500'),
('What is the atomic number of oxygen?', '7', '8', '9', '10', 'B', '500'),
('Who is the main character in ""The Catcher in the Rye""?', 'Holden Caulfield', 'Jay Gatsby', 'Atticus Finch', 'Scout Finch', 'A', '500'),
('What is the square root of 961?', '31', '33', '35', '37', 'A', '500'),
('What is the highest mountain on Earth?', 'Mount Kilimanjaro', 'Mount Everest', 'Mount Fuji', 'Mount McKinley', 'B', '500'),
('What is the chemical symbol for gold?', 'Go', 'Au', 'Ag', 'Gd', 'B', '500'),
('Who wrote the play ""Hamlet""?', 'Jane Austen', 'Charles Dickens', 'Mark Twain', 'William Shakespeare', 'D', '500'),
('What is the square root of 1024?', '32', '34', '36', '38', 'A', '500'),
('What is the atomic number of helium?', '1', '2', '3', '4', 'B', '500'),
('Who is the author of ""Moby-Dick""?', 'Herman Melville', 'F. Scott Fitzgerald', 'J.D. Salinger', 'Ernest Hemingway', 'A', '500'),
('What is the chemical symbol for copper?', 'Cu', 'Co', 'Cp', 'Cr', 'A', '500'),
('What is the square root of 1156?', '34', '36', '38', '40', 'A', '500'),
('What is the capital of Brazil?', 'Sao Paulo', 'Brasilia', 'Rio de Janeiro', 'Salvador', 'B', '500'),
('What is the atomic number of lithium?', '2', '3', '4', '5', 'B', '500'),
('Who won the FIFA World Cup in 2018?', 'Germany', 'Argentina', 'France', 'Brazil', 'C', '1000'),
('Which country has won the most Olympic gold medals in history?', 'United States', 'Russia', 'China', 'Germany', 'A', '1000'),
('In which sport is a ""slam dunk"" a common term?', 'Basketball', 'Soccer', 'Tennis', 'Golf', 'A', '1000'),
('Who is considered the greatest boxer of all time with a record of 50-0?', 'Mike Tyson', 'Floyd Mayweather Jr.', 'Muhammad Ali', 'George Foreman', 'B', '1000'),
('Which tennis player has the most Grand Slam titles in the Open Era?', 'Rafael Nadal', 'Roger Federer', 'Novak Djokovic', 'Serena Williams', 'C', '1000'),
('Which NFL quarterback has the most Super Bowl wins?', 'Tom Brady', 'Peyton Manning', 'Joe Montana', 'Brett Favre', 'A', '1000'),
('Which country hosted the 2016 Summer Olympics?', 'United States', 'Australia', 'Russia', 'Brazil', 'D', '1000'),
('Who holds the record for the most home runs in a single MLB season?', 'Babe Ruth', 'Barry Bonds', 'Mark McGwire', 'Sammy Sosa', 'B', '1000'),
('Which boxer was known as ""The Greatest"" and ""The Louisville Lip?""', 'Mike Tyson', 'Evander Holyfield', 'George Foreman', 'Muhammad Ali', 'D', '1000'),
('Which country won the most medals in the 2016 Rio Olympics?', 'United States', 'China', 'Russia', 'Great Britain', 'A', '1000'),
('If you could have any mythical creature as a pet, what would it be?', 'Dragon', 'Unicorn', 'Phoenix', 'Kraken', 'B', '2000'),
('Whats your go-to dance move when nobodys watching?', 'The Moonwalk', 'The Robot', 'The Carlton', 'The Macarena', 'A', '2000'),
('If you could travel back in time and witness any historical event, which one would it be?', 'The signing of the Declaration of Independence', 'The moon landing', 'The fall of the Berlin Wall', 'The Renaissance', 'B', '2000'),
('Whats the weirdest food combination you enjoy?', 'Peanut butter and pickles', 'French fries and ice cream', 'Sriracha on watermelon', 'Pizza with pineapple', 'C', '2000'),
('If you were a superhero, what would your costume and name be?', 'Costume: Silver spandex suit, Name: The Silver Streak', 'Costume: Neon tights, Name: Neon Ninja', 'Costume: Cape and goggles, Name: Captain Capable', 'Costume: Lab coat and safety glasses, Name: Dr. Discovery', 'D', '2000'),
('If you could have any animals ability, which one would it be?', 'Echolocation like a bat', 'Camouflage like a chameleon', 'Regeneration like a starfish', 'Flight like an eagle', 'B', '2000'),
('Whats the most bizarre talent you have?', 'Can touch your nose with your tongue', 'Can do a perfect Gollum impression', 'Can juggle flaming torches', 'Can recite the alphabet backward', 'A', '2000'),
('If you could swap lives with any famous person for a day, who would it be?', 'Elon Musk', 'Beyoncé', 'Neil Armstrong', 'Oprah Winfrey', 'B', '2000'),
('Whats your favorite flavor of ice cream, even if its weird?', 'Balsamic vinegar', 'Lavender', 'Avocado', 'Saffron', 'A', '2000'),
('If you could have a conversation with any animal, which one would it be?', 'Dolphins', 'Elephants', 'Wolves', 'Octopuses', 'C', '2000'),
('Whats your guilty pleasure song?', 'Never Gonna Give You Up by Rick Astley', 'Barbie Girl by Aqua', 'Mambo No. 5 by Lou Bega', 'Achy Breaky Heart by Billy Ray Cyrus', 'A', '2000'),
('If you could be a character in any video game, which one would it be?', 'Super Mario', 'Master Chief (Halo)', 'Link (The Legend of Zelda)', ' Lara Croft (Tomb Raider)', 'C', '2000'),
('Whats the strangest dream youve ever had?', 'Flying on a giant pizza', 'Being chased by a talking carrot', 'Conversing with a time-traveling hamster', 'Attending a disco with Shakespeare', 'B', '2000'),
('If you could live in any fictional universe, where would it be?', 'Willy Wonkas Chocolate Factory', 'The Starship Enterprise (Star Trek)', 'Hogwarts School of Witchcraft and Wizardry (Harry Potter)', 'Middle-earth (The Lord of the Rings)', 'C', '2000'),
('Whats the most unusual item on your bucket list?', 'Swim with sharks', 'Visit all the worlds deserts', 'Skydiving over the Grand Canyon', 'Participate in a hot dog eating contest', 'D', '2000'),
('What type of dog is ''Handsome Dan'', the mascot of Yale University?', 'Yorkshire Terrier', 'Boxer', 'Pug', 'Bulldog', 'B', '4000'),
('Which of these is a type of monster found in Minecraft?', 'Werewolf', 'Vampire', 'Minotaur', 'Skeleton', 'D', '4000'),
('Which franchise does the creature ''Slowpoke'' originate from?', 'Dragon Ball', 'Sonic The Hedgehog', 'Yugioh', 'Pokemon', 'D', '4000'),
('Which of these is NOT an Australian state or territory?', 'New South Wales', 'Victoria', 'Queensland', 'Alberta', 'D', '4000'),

('Which one of the following actors did not voice a character in ''Saints Row: The Third''?', 'Sasha Grey', 'Burt Reynolds', 'Hulk Hogan', 'Ron Jeremy', 'D', '4000'),

('What is the capital of the US state Nevada?', 'Las Vegas', 'Henderson', 'Reno', 'Carson City', 'D', '4000'),

('Who is the lead singer of Silverchair?', 'Ben Gillies', 'Chris Joannou', 'George Costanza', 'Daniel Johns', 'D', '4000'),

('When was ''Garry''s Mod'' released?', 'November 13, 2004', 'December 13, 2004', 'November 12, 2004', 'December 24, 2004', 'D', '4000'),

('What is the capital of the State of Washington, United States?', 'Washington D.C.', 'Seattle', 'Yukon', 'Olympia', 'D', '4000'),

('What is the romanized Japanese word for ''university''?', 'Toshokan', 'Jimusho', 'Shokudou', 'Daigaku', 'D', '4000'),

('Who is the founder of Team Fortress 2''s fictional company ''Mann Co''?', 'Cave Johnson', 'Wallace Breem', 'Saxton Hale', 'Zepheniah Mann', 'D', '4000'),

('What year did the game ''Overwatch'' enter closed beta?', '2013', '2011', '2016', '2015', 'D', '4000'),

('What is the name of Broadway''s first ''long-run'' musical?', 'Wicked', 'Hamilton', 'The Book of Mormon', 'The Elves', 'D', '4000'),

('Who voice acted the character Hiccup in the movie ''How to Train Your Dragon''?', 'Jack Brauchel', 'John Powell', 'Gerard Butler', 'Jay Baruchel', 'D', '4000'),

('Which of these names was an actual codename for a cancelled Microsoft project?', 'Enceladus', 'Pollux', 'Saturn', 'Neptune', 'D', '4000'),

('The 2005 video game ''Call of Duty 2: Big Red One'' is not available on PC.', 'False', '', '', 'True', 'True', '4000'),

('Which is the largest freshwater lake in the world?', 'Caspian Sea', 'Lake Michigan', 'Lake Huron', 'Lake Superior', 'D', '4000'),

('Which element has the highest melting point?', 'Tungsten', 'Platinum', 'Osmium', 'Carbon', 'D', '64000'),

('In standard Monopoly, what''s the rent if you land on Park Place with no houses?', '$30', '$50', '$45', '$35', 'D', '64000'),

('What engine is in the Lexus SC400?', '2JZ-GTE', '7M-GTE', '5M-GE', '1UZ-FE', 'D', '64000'),

('The Lego Group was founded in 1932.', 'False', '', '', 'True', 'True', '64000'),

('Nidhogg is a mythical creature from what mythology?', 'Egyptian', 'Greek', 'Hindu', 'Norse', 'D', '64000'),

('The book ''The Little Prince'' was written by...', 'Miguel de Cervantes Saavedra', 'Jane Austen', 'F. Scott Fitzgerald', 'Antoine de Saint-Exupéry', 'D', '4000'),

('When was the Garfield comic first published?', '1982', '1973', '1988', '1978', 'D', '64000'),

('The band STRFKR was also briefly known as Pyramiddd.', 'False', '', '', 'True', 'D', '64000'),

('Which song on Daft Punk''s ''Random Access Memories'' features Pharrell Williams?', 'Doin'' It Right', 'Instant Crush', 'The Game of Love', 'Get Lucky', 'D', '64000'),

('In Star Trek: The Next Generation, what is the name of Data''s cat?', 'Mittens', 'Tom', 'Kitty', 'Spot', 'D', '4000'),

('The heroine of ''Humanity Has Declined'' is a mediator between humans and what?', 'Elves', 'The Earth', 'Animals', 'Fairies', 'D', '4000'),

('What does the Latin phrase ''Veni, vidi, vici'' translate into English?', 'See no evil, hear no evil, speak no evil', 'Life, liberty, and happiness', 'Past, present, and future', 'I came, I saw, I conquered', 'D', '4000'),

('Who voices the character ''Vernon Cherry'' in ''Red Dead Redemption''?', 'Tara Strong', 'Troy Baker', 'Rob Wiethoff', 'Casey Mongillo', 'D', '4000'),

('Muscle fiber is constructed of bundles small long organelles called what?', 'Epimysium', 'Myofiaments', 'Myocardium', 'Myofibrils', 'D', '4000'),

('In the ''Call Of Duty: Zombies'' map ''Origins'', how many steps are there to upgrade a Staff?', '7', '5', '3', '4', 'D', '4000'),

('In Battlestar Galactica (2004), what is the name of the President of the Twelve Colonies?', 'William Adama', 'Tricia Helfer', 'Harry Stills', 'Laura Roslin', 'D', '4000'),

('For the film ''Raiders of The Lost Ark'', what was Harrison Ford sick with during the filming of the Cairo chase?', 'Anemia', 'Constipation', 'Acid Reflux', 'Dysentery', 'D', '4000'),

('Which gaming series includes ''The Diabolical Box'' and ''The Curious Village''?', 'Persona', 'Etrian Odyssey', 'Sam & Max', 'Professor Layton', 'D', '4000'),

('Which item of clothing is usually worn by a Scotsman at a wedding?', 'Skirt', 'Dress', 'Rhobes', 'Kilt', 'D', '4000'),

('Which actor from The Young Ones also played Lord Flashheart in one episode of Blackadder II?', 'Adrian Edmondson', 'Nigel Planer', 'Christopher Ryan', 'Rik Mayall', 'D', '4000'),

('According to the RIAA: Which artist has sold the most albums by the million?', 'Elvis Presley', 'Michael Jackson', 'Pink Floyd', 'The Beatles', 'D', '4000'),

('How many furlongs are there in a mile?', 'Two', 'Four', 'Six', 'Eight', 'D', '4000'),

('Which member of the British pop group ''The Spice Girls'' was known as Ginger Spice?', 'Melanie Brown', 'Emma Bunton', 'Victoria Beckham', 'Geri Halliwell', 'D', '4000'),

('In ''Avatar: The Last Airbender'' and ''The Legend of Korra'', Lavabending is a specialized bending technique of Firebending.', 'True', '', '', 'False', 'False', '4000'),

('The Harry Potter series of books, combined, are over 1,000,000 words in length.', 'False', '', '', 'True', 'True', '4000'),

('In World of Warcraft the default UI color that signifies Druid is what?', 'Brown', 'Green', 'Blue', 'Orange', 'D', '4000'),

('What animal did Queen Pasiphae sleep with before she gave birth to the Minotaur in Greek Mythology?', 'Pig', 'Ox', 'Horse', 'Bull', 'D', '4000'),

('Which of the following actors does not play a role in the movie ''The Usual Suspects?''', 'Kevin Spacey', 'Benicio Del Toro', 'Gabriel Byrne', 'Steve Buscemi', 'D', '4000'),

('What was the date of original airing of the pilot episode of My Little Pony: Friendship is Magic?', 'November 6th, 2010', 'April 14th, 1984', 'May 18th, 2015', 'October 10th, 2010', 'D', '4000'),

('About how much money did it cost for Tommy Wiseau to make his masterpiece ''The Room'' (2003)?', '$20,000', '$1 Million', '$10 Million', '$6 Million', 'D', '4000'),

('Which soccer player is featured on the cover of EA Sport''s FIFA 18?', 'Lionel Messi', 'Neymar', 'Harry Kane', 'Cristiano Ronaldo', 'D', '4000'),
('Which singer provided the voice of Metroid''s Mother Brain in the animated series ''Captain N: The Game Master''?', 'Freddie Mercury', 'Janet Jackson', 'Joan Jett', 'Levi Stubbs', 'D', '4000'),
('Which of the following was not one of ''The Magnificent Seven''?', 'Steve McQueen', 'Charles Bronson', 'Robert Vaughn', 'Clint Eastwood', 'D', '8000'),
('A carnivorous animal eats flesh, what does a nucivorous animal eat?', 'Nothing', 'Fruit', 'Seaweed', 'Nuts', 'D', '8000'),
('According to the BBPA, what is the most common pub name in the UK?', 'Royal Oak', 'White Hart', 'King''s Head', 'Red Lion', 'D', '8000'),
('In the Nintendo DS game ''Ghost Trick: Phantom Detective'', what is the name of the hitman seen at the start of the game?', 'One Step Ahead Tengo', 'Missile', 'Cabanela', 'Nearsighted Jeego', 'D', '8000'),
('Which English guitarist has the nickname ''Slowhand''?', 'Mark Knopfler', 'Jeff Beck', 'Jimmy Page', 'Eric Clapton', 'D', '8000'),
('This element, when overcome with extreme heat and pressure, creates diamonds.', 'Nitrogen', 'Oxygen', 'Hydrogen', 'Carbon', 'D', '8000'),
('What position does Harry Potter play in Quidditch?', 'Beater', 'Chaser', 'Keeper', 'Seeker', 'D', '8000'),
('How many dots are on a single die?', '24', '15', '18', '21', 'D', '8000'),
('What year did Albrecht Dürer create the painting ''The Young Hare''?', '1702', '1402', '1602', '1502', 'D', '8000'),
('What is the chemical formula for ammonia?', 'CO2', 'NO3', 'CH4', 'NH3', 'D', '8000'),
('Who voices for Ruby in the animated series RWBY?', 'Tara Strong', 'Jessica Nigri', 'Hayden Panettiere', 'Lindsay Jones', 'D', '8000'),
('What is the name of Sid''s dog in ''Toy Story''?', 'Buster', 'Whiskers', 'Mr. Jones', 'Scud', 'D', '8000'),
('What is the name of the main character in the webcomic Gunnerkrigg Court by Tom Siddell?', 'Bismuth', 'Mercury', 'Cobalt', 'Antimony', 'D', '8000'),

('What do you call a baby bat?', 'Cub', 'Chick', 'Kid', 'Pup', 'D', '8000'),

('Which character does voice actress Tara Strong NOT voice?', 'Twilight Sparkle', 'Timmy Turner', 'Harley Quinn', 'Bubbles (2016)', 'D', '8000'),

('''The Big Bang Theory'' was first theorized by a priest of what religious ideology?', 'Christian', 'Jewish', 'Islamic', 'Catholic', 'D', '8000'),

('Johnny Cash did a cover of this song written by the lead singer of Nine Inch Nails, Trent Reznor.', 'Closer', 'A Warm Place', 'Big Man with a Gun', 'Hurt', 'D', '8000'),

('Which of these is NOT a possible drink to be made in the game ''VA-11 HALL-A: Cyberpunk Bartender Action''?', 'Sour Appletini', 'Fringe Weaver', 'Piano Man', 'Bad Touch', 'D', '8000'),

('When was Chapter 1 of the Source Engine mod ''Underhell'' released?', 'March 3rd, 2011', 'September 12th, 2013', 'October 2nd, 2012', 'September 1st, 2013', 'D', '8000'),

('What engine is in the Lexus SC400?', '2JZ-GTE', '7M-GTE', '5M-GE', '1UZ-FE', 'D', '8000'),

('In ''The Melancholy of Haruhi Suzumiya'' series, the SOS Brigade club leader is unknowingly treated as a(n) __ by her peers.', 'Alien', 'Time Traveler', 'Esper', 'God', 'D', '8000'),

('Gwyneth Paltrow has a daughter named...?', 'Lily', 'French', 'Dakota', 'Apple', 'D', '8000'),

('Who is the Egyptian god of reproduction and lettuce?', 'Menu', 'Mut', 'Meret', 'Min', 'D', '8000'),

('Which Overwatch character says the line ''Heroes never die!''?', 'Reaper', 'Sonic', 'Ana', 'Mercy', 'D', '8000'),

('Amsterdam Centraal station is twinned with what station?', 'Frankfurt (Main) Hauptbahnhof', 'Paris Gare du Nord', 'Brussels Midi', 'London Liverpool Street', 'D', '8000'),

('What is the capital of India?', 'Bejing', 'Montreal', 'Tithi', 'New Delhi', 'D', '8000'),

('Which iconic Disneyland attraction was closed in 2017 to be remodeled as a ''Guardians of the Galaxy'' themed ride?', 'The Haunted Mansion', 'Pirates of the Caribbean', 'Peter Pan''s Flight', 'Twilight Zone Tower of Terror', 'D', '8000'),

('In Dota 2, what is Earthshaker''s real name?', 'Banehallow Ambry', 'Carl', 'Barathrum', 'Raigor Stonehoof', 'D', '8000'),

('What was the #1 selling game on Steam by revenue in 2016?', 'Grand Theft Auto V', 'Counter Strike: Global Offensive', 'Dark Souls III', 'Sid Meier''s Civilization VI', 'D', '8000'),

('In which year did ''Caravan Palace'' release their first album?', '2000', '2015', '2004', '2008', 'D', '8000'),

('Which band released the album ''Sonic Highways'' in 2014?', 'Coldplay', 'Nickelback', 'The Flaming Lips', 'Foo Fighters', 'D', '8000'),

('What is the name of the supercomputer located in the control room in ''Jurassic Park'' (1993)?', 'Cray X-MP', 'Cray XK7', 'IBM Blue Gene/Q', 'Thinking Machines CM-5', 'D', '8000'),

('What vulnerability ranked #1 on the OWASP Top 10 in 2013?', 'Broken Authentication', 'Cross-Site Scripting', 'Insecure Direct Object References', 'Injection', 'D', '8000'),

('What was the name commonly given to the ancient trade routes that connected the East and West of Eurasia?', 'Spice Road', 'Clay Road', 'Salt Road', 'Silk Road', 'D', '8000'),

('In ''Kingdom Hearts'', what is the name of Sora''s home world?', 'Agrabah', 'Land of Departure', 'Disney Town', 'Destiny Islands', 'D', '8000'),

('What is the northernmost human settlement with year-round inhabitants?', 'Nagurskoye, Russia', 'McMurdo Station, Antarctica', 'Honningsvåg, Norway', 'Alert, Canada', 'D', '8000'),

('In ''PUBATTLEGROUNDS'' which ammo type does the M24 use?', '5.56mm', '9mm', '.300 Magnum', '7.62mm', 'D', '8000'),

('Which genre is the Touhou Project associated with?', 'Turn-Based Strategy', 'MMORPG', 'Building', 'Shoot ''em up (bullet-hell) & Fighting', 'D', '8000'),

('Which of the following games has the most playable characters?', 'Mortal Kombat: Armageddon', 'Marvel Vs. Capcom 2', 'Dragon Ball Z: Budokai Tenkaichi 3', 'Timesplitters: Future Perfect', 'D', '8000'),

('Which song made by MAN WITH A MISSION was used as the opening for the anime ''Log Horizon''?', '""Dead End in Tokyo""', '""Raise Your Flag""', '""Out of Control""', '""Database""', 'D', '8000'),

('Which artist curated the official soundtrack for ''The Hunger Games: Mockingjay - Part 1''?', 'Kanye West', 'Tove Lo', 'Charli XCX', 'Lorde', 'D', '8000'),

('''Strangereal'' is a fictitious Earth-like world for which game series?', 'Jet Set Radio', 'Deus Ex', 'Crimson Skies', 'Ace Combat', 'D', '8000'),

('The coat of arms of the King of Spain contains the arms from the monarchs of Castille, Leon, Aragon and which other former Iberian kingdom?', 'Galicia', 'Granada', 'Catalonia', 'Navarre', 'D', '8000'),

('Which of the following actors has only been in a Quentin Tarantino directed film once?', 'Christoph Waltz', 'Harvey Keitel', 'Samuel L. Jackson', 'John Travolta', 'D', '8000'),

('Frank Lloyd Wright was the architect behind what famous building?', 'Villa Savoye', 'Sydney Opera House', 'The Space Needle', 'The Guggenheim', 'D', '8000'),

('Which of these cars is NOT considered one of the 5 Modern Supercars by Ferrari?', 'Enzo Ferrari', 'F40', '288 GTO', 'Testarossa', 'D', '8000'),

('In the Mad Max franchise, what type of car is the Pursuit Special driven by Max?', 'Holden Monaro', 'Chrysler Valiant Charger', 'Pontiac Firebird', 'Ford Falcon', 'D', '8000'),

('What''s the race of Invincible''s father?', 'Kryptonian', 'Kree', 'Irken', 'Viltrumite', 'D', '8000'),

('In The Lies of Locke Lamora, what title is Locke known by in the criminal world?', 'The Rose of the Marrows', 'The Thorn of Emberlain', 'The Thorn of the Marrows', 'The Thorn of Camorr', 'D', '8000'),

('Marvel vs Capcom 2: New age of Heroes was released in what year?', '2001', '2003', '1998', '2000', 'D', '8000'),
('What''s the name of the main protagonist in the ''Legend of Zelda'' franchise?', 'Mario', 'Zelda', 'Pit', 'Link', 'D', '16000'),
('Which company developed the video game ''Borderlands''?', '2K Games', 'Activision', 'Rockstar Games', 'Gearbox Software', 'D', '16000'),
('Which company did Gabe Newell work at before founding Valve Corporation?', 'Apple', 'Google', 'Yahoo', 'Microsoft', 'D', '16000'),
('In Dragon Ball Z, who was the first character to go Super Saiyan 2?', 'Goku', 'Vegeta', 'Trunks', 'Gohan', 'D', '16000'),
('What are the first 6 digits of the number ''Pi''?', '3.14169', '3.12423', '3.25812', '3.14159', 'D', '16000'),
('In the Harry Potter universe, what is Cornelius Fudge''s middle name?', 'James', 'Harold', 'Christopher', 'Oswald', 'D', '16000'),
('Who was the 40th President of the USA?', 'Jimmy Carter', 'Bill Clinton', 'Richard Nixon', 'Ronald Reagan', 'D', '16000'),
('Hippocampus is the Latin name for which marine creature?', 'Dolphin', 'Whale', 'Octopus', 'Seahorse', 'D', '16000'),

('In what year was the original Sonic the Hedgehog game released?', '1989', '1993', '1995', '1991', 'D', '16000'),

('Which of the following Assyrian kings did NOT rule during the Neo-Assyrian Empire?', 'Shalmaneser V', 'Esharhaddon', 'Ashur-nasir-pal II', 'Shamshi-Adad III', 'D', '16000'),

('List the following Iranic empires in chronological order:', 'Median, Achaemenid, Parthian, Sassanid', 'Median, Achaemenid, Sassanid, Parthian', 'Achaemenid, Median, Parthian, Sassanid', 'Achaemenid, Median, Sassanid, Parthian', 'D', '16000'),

('Which nation hosted the FIFA World Cup in 2006?', 'United Kingdom', 'Brazil', 'South Africa', 'Germany', 'D', '16000'),

('When was the first ''Half-Life'' released?', '2004', '1999', '1997', '1998', 'D', '16000'),

('Who voices Max Payne in the 2001 game ''Max Payne''?', 'Sam Lake', 'Troy Baker', 'Hideo Kojima', 'James McCaffrey', 'D', '16000'),

('Folic acid is the synthetic form of which vitamin?', 'Vitamin A', 'Vitamin C', 'Vitamin D', 'Vitamin B', 'D', '16000'),

('What is the stage name of English female rapper Mathangi Arulpragasam, who is known for the song ''Paper Planes''?', 'K.I.A.', 'C.I.A.', 'A.I.A.', 'M.I.A.', 'D', '16000'),

('What is the German word for ''spoon''?', 'Gabel', 'Messer', 'Essstäbchen', 'Löffel', 'D', '16000'),

('Who created the 2011 Video Game ''Minecraft''?', 'Jens Bergensten', 'Daniel Rosenfeld', 'Carl Manneh', 'Markus Persson', 'D', '16000'),

('Winch of these names are not a character of JoJo''s Bizarre Adventure?', 'Jean-Pierre Polnareff', 'George Joestar', 'Risotto Nero', 'JoJo Kikasu', 'D', '16000'),

('How long was the World Record Speed Run of Valve Software''s ''Half-Life'' that was done in 2014.', '45 Minutes, 32 Seconds', '5 Minutes, 50 Seconds', '12 Minutes, 59 Seconds', '20 Minutes, 41 Seconds', 'D', '16000'),

('How many seasons did ''Stargate SG-1'' have?', '3', '7', '12', '10', 'D', '16000'),

('Fucking is a village in which country?', 'Germany', 'Switzerland', 'Czech Republic', 'Austria', 'D', '16000'),

('In the show ''Tengen Toppa Gurren Lagann'' what is the name of the character who force everyone to live underground?', 'Kingloname', 'Lord Genome', 'King Loname', 'Lordgenome', 'D', '16000'),

('Which of these Pokémon cannot learn Surf?', 'Linoone', 'Tauros', 'Nidoking', 'Arbok', 'D', '16000'),

('What is the second-largest city in Lithuania?', 'Panevėžys', 'Vilnius', 'Klaipėda', 'Kaunas', 'D', '16000'),

('In the Mass Effect trilogy, who is the main protagonist?', 'Mordin', 'Garrus', 'Thane', 'Shepard', 'D', '16000'),

('Which restaurant''s mascot is a clown?', 'Whataburger', 'Burger King', 'Sonic', 'McDonald''s', 'D', '16000'),

('British actor David Morrissey stars as which role in ''The Walking Dead''?', 'Negan', 'Rick Grimes', 'Daryl Dixon', 'The Governor', 'D', '16000'),

('Which of the following is not a character in the Street Fighter series?', 'Laura Matsuda', 'Sakura Kasugano', 'Ibuki', 'Mai Shiranui', 'D', '16000'),

('Which of the following originated as a manga?', 'Cowboy Bebop', 'High School DxD', 'Gurren Lagann', 'Akira', 'D', '16000'),

('In ''SpongeBob SquarePants'', what is the name of Sandy Cheek''s place of residence?', '""The Dome""', 'Sandy''s Bubble', 'Auquatic Reseach Centre', 'Sandy''s Treedome', 'D', '16000'),

('Which of these characters from ''SpongeBob SquarePants'' is not a squid?', 'Orville', 'Squidward', 'Squidette', 'Gary', 'D', '16000'),

('Which of these songs is not on the ''untitled'' album by Led Zeppelin?', 'Stairway to Heaven', 'Black Dog', 'Rock and Roll', 'Celebration Day', 'D', '16000'),

('These two countries held a commonwealth from the 16th to 18th century.', 'Hutu and Rwanda', 'North Korea and South Korea', 'Bangladesh and Bhutan', 'Poland and Lithuania', 'D', '16000'),

('Which ones of these Mario Kart games was made for the Gameboy Advance?', 'Mario Kart: Double Dash', 'Mario Kart 64', 'Super Mario Kart', 'Mario Kart: Super Circuit', 'D', '16000'),

('What was the original name of New York City?', 'New London', 'New Paris', 'New Rome', 'New Amsterdam', 'D', '16000'),

('In ''Highschool DxD'', Koneko Toujou is from what race?', 'Kitsune', 'Human', 'Kappa', 'Nekomata', 'D', '16000'),

('On which mission did the Space Shuttle Columbia break up upon re-entry?', 'STS-51-L', 'STS-61-C', 'STS-109', 'STS-107', 'D', '16000'),

('In the Super Smash Bros. series, which character was the first one to return to the series after being absent from a previous game?', 'Mewtwo', 'Lucas', 'Roy', 'Dr. Mario', 'D', '16000'),

('Which gaming series includes ''The Diabolical Box'' and ''The Curious Village''?', 'Persona', 'Etrian Odyssey', 'Sam & Max', 'Professor Layton', 'D', '16000'),

('Which animation studio produced ''Log Horizon''?', 'Sunrise', 'Xebec', 'Production I.G', 'Satelite', 'D', '16000'),

('In the Magic: The Gathering universe, the Fallen Empires expansion takes place on which continent?', 'Otaria', 'Terisiare', 'Shiv', 'Sarpadia', 'D', '16000'),

('What is the name of Eragon''s dragon in ''Eragon''?', 'Glaedr', 'Thorn', 'Arya', 'Saphira', 'D', '16000'),

('Which car is NOT featured in ''Need for Speed: Hot Pursuit 2''?', 'Ford Crown Victoria', 'BMW Z8', 'McLaren F1', 'Toyota MR2', 'D', '16000'),

('How many countries share a land border with Luxembourg?', '4', '2', '5', '3', 'D', '16000'),

('What was the name given to Japanese military dictators who ruled the country through the 12th and 19th Century?', 'Ninja', 'Samurai', 'Shinobi', 'Shogun', 'D', '16000'),

('What was the reason for the banning of episode 35 of the ''Pokémon Original Series'' Anime?', 'Flashing Images', 'Jynx', 'Strong Violence', 'Gun Usage', 'D', '16000'),

('What is the name of NASA’s most famous space telescope?', 'Big Eye', 'Death Star', 'Millenium Falcon', 'Hubble Space Telescope', 'D', '16000'),

('What does Bart sell his soul for in The Simpsons episode ''Bart Sells His Soul''?', 'A Copy of Bonestorm 2', '$100', 'A Giant Gobstopper', '$5', 'D', '16000'),

('Which of the following actors has only been in a Quentin Tarantino directed film once?', 'Christoph Waltz', 'Harvey Keitel', 'Samuel L. Jackson', 'John Travolta', 'D', '16000'),
('Painter Piet Mondrian (1872 - 1944) was a part of what movement?', 'Impressionism', 'Neoplasticism', 'Precisionism', 'Cubism', 'B', '32000'),

('Which 1958 movie starred Kirk Douglas and Tony Curtis as half-brothers Einar and Eric?', 'The Long Ships', 'The Vikings', 'Spartacus', 'Prince Valiant', 'B', '32000'),

('In the book ""The Martian"", how long was Mark Watney trapped on Mars (in Sols)?', '765 Days', '549 Days', '401 Days', '324 Days', 'B', '32000'),

('Rolex is a company that specializes in what type of product?', 'Computers', 'Watches', 'Cars', 'Sports equipment', 'B', '32000'),

('In which countrys version of Half-Life are the HECU Marines replaced with robots?', 'France', 'Germany', 'China', 'Japan', 'B', '32000'),

('Which of his six wives was Henry VIII married to the longest?', 'Anne Boleyn', 'Catherine of Aragon', 'Jane Seymour', 'Catherine Parr', 'B', '32000'),

('What is the main ship used by Commander Shepard in the Mass Effect Franchise called?', 'Infinity', 'Endeavour', 'Normandy', 'Osiris', 'C', '32000'),

('In which African country was the 2006 film Blood Diamond mostly set in?', 'Liberia', 'Sierra Leone', 'Burkina Faso', 'Central African Republic', 'B', '32000'),

('What genre of EDM is the Dutch DJ, musician, and remixer Armin van Buuren most well-known for?', 'House', 'Trance', 'Dubstep', 'Drum and Bass', 'B', '32000'),

('Which Native American tribe/nation requires at least one half blood quantum (equivalent to one parent) to be eligible for membership?', 'Kiowa Tribe of Oklahoma', 'Yomba Shoshone Tribe', 'Standing Rock Sioux Tribe', 'Pawnee Nation of Oklahoma', 'B', '32000'),

('What year did Albrecht Dürer create the painting ""The Young Hare""?', '1702', '1502', '1402', '1602', 'B', '32000'),

('What is the name of Finnish DJ Darudes hit single released in October 1999?', 'Dust Devil', 'Sandstorm', 'Sirocco', 'Khamsin', 'B', '32000'),

('In which order do you need to hit some Deku Scrubs to open the first boss door in ""Ocarina of Time""?', '1, 2, 3', '2, 3, 1', '1, 3, 2', '2, 1, 3', 'B', '32000'),

('What was the destination of the missing flight MH370?', 'Kuala Lumpur', 'Beijing', 'Singapore', 'Tokyo', 'B', '32000'),

('The term ""battery"" to describe an electrical storage device was coined by?', 'Nikola Tesla', 'Benjamin Franklin', 'Luigi Galvani', 'Alessandro Volta', 'B', '32000'),

('Who is the lead singer of the British pop rock band Coldplay?', 'Jonny Buckland', 'Chris Martin', 'Guy Berryman', 'Will Champion', 'B', '32000'),

('What is the nickname of the US state of California?', 'Sunshine State', 'Golden State', 'Bay State', 'Treasure State', 'B', '32000'),

('Who painted ""Swans Reflecting Elephants"", ""Sleep"", and ""The Persistence of Memory""?', 'Jackson Pollock', 'Salvador Dali', 'Vincent van Gogh', 'Edgar Degas', 'B', '32000'),

('In Team Fortress 2, which class wields a shotgun?', 'Everyone Listed', 'Heavy', 'Soldier', 'Engineer', 'B', '32000'),

('About how much money did it cost for Tommy Wiseau to make his masterpiece ""The Room"" (2003)?', '$20,000', '$6 Million', '$1 Million', '$10 Million', 'B', '32000'),

('Which English football team is nicknamed The Tigers?', 'Cardiff City', 'Hull City', 'Bristol City', 'Manchester City', 'B', '32000'),

('How many stars are there to collect in Super Mario 64?', '60', '120', '80', '100', 'B', '32000'),
    ('What is the capital of France?', 'Paris', 'London', 'Berlin', 'Madrid', 'A', '125000'),
    ('Which chemical element has the symbol ""Fe""?', 'Iron', 'Gold', 'Silver', 'Copper', 'A', '125000'),
    ('Who wrote the famous play ""Romeo and Juliet""?', 'Charles Dickens', 'Jane Austen', 'William Shakespeare', 'Mark Twain', 'C', '125000'),
    ('What is the capital of Australia?', 'Melbourne', 'Sydney', 'Canberra', 'Perth', 'C', '125000'),
    ('In which year did Christopher Columbus discover America?', '1492', '1776', '1812', '1620', 'A', '125000'),
    ('What is the largest planet in our solar system?', 'Earth', 'Mars', 'Jupiter', 'Saturn', 'C', '500000'),
    ('How many bones are there in the adult human body?', '106', '206', '306', '406', 'B', '500000'),
    ('Who is the author of ""To Kill a Mockingbird""?', 'F. Scott Fitzgerald', 'John Steinbeck', 'Harper Lee', 'Ernest Hemingway', 'C', '500000'),
    ('What is the capital of Japan?', 'Beijing', 'Seoul', 'Shanghai', 'Tokyo', 'D', '500000'),
    ('What is the boiling point of water in Celsius?', '-100', '0', '100', '200', 'C', '500000'),
    ('What is the chemical symbol for gold?', 'Au', 'Ag', 'Fe', 'Cu', 'A', '1000000'),
    ('Who is known as the father of modern physics?', 'Albert Einstein', 'Isaac Newton', 'Galileo Galilei', 'Stephen Hawking', 'A', '1000000'),
    ('What is the largest desert in the world?', 'Sahara', 'Gobi', 'Atacama', 'Antarctica', 'A', '1000000'),
    ('Which planet is known as the Red Planet?', 'Mars', 'Venus', 'Jupiter', 'Saturn', 'A', '1000000'),
    ('Who wrote the play ""Hamlet""?', 'William Shakespeare', 'George Orwell', 'Charles Dickens', 'Mark Twain', 'A', '1000000');














                
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

                    string createTableQueryHighscores = @"
            CREATE TABLE IF NOT EXISTS Highscores (
                score_id INTEGER PRIMARY KEY AUTOINCREMENT,
                player_name TEXT NOT NULL,
                score INTEGER NOT NULL
            )";

                    using (SQLiteCommand createTableScores = new SQLiteCommand(createTableQueryHighscores, connection))
                    {
                        createTableScores.ExecuteNonQuery();
                    }

                    string insertBaseHighscores = @"
            INSERT INTO Highscores (player_name ,score ) 
            VALUES ('QuizChecker24',3),
             ('DerQuizmeister',1000),
             ('Gurke',500),
             ('Mastermind',125000),
             ('Noobinator',399),
             ('Unwissender',100),
             ('NotEinstein',99),
             ('Bodenlos',10),
             ('BoatyMcBoatface',5),
             ('Kasalla',33),
             ('Per Anhalter',42),
             ('Jane Doe',111),
             ('Stroh Dumm',1),
             ('Sheldon',500000)
            ";

                    using (SQLiteCommand writeASave = new SQLiteCommand(insertBaseHighscores, connection))
                    {
                        writeASave.ExecuteNonQuery();
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
