using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dbConnection = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QDB.db");
            var initDB = new DBInit(dbConnection);
            initDB.InitializeDatabase();
            StartWindow.CreateWelcomeScreen();
        }
            
        
    }
}
