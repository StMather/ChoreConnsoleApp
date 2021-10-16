using System;
using System.Data.SqlClient;

namespace ChoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //MVP
            //When the application runs, create a new database after dropping a previous database with the same name if needed. (You choose the name of the database).
            try
            {
                //move to demo??
                //Build conection String
                DataAccess sqlAccess = new DataAccess();
                string accessString = sqlAccess.ConnectionString();
                Demo.RunDemo(accessString, sqlAccess);


                //user input
                UserInput.InputLoop(accessString, sqlAccess);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
