using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ChoreConsoleApp
{
    class Demo
    {
        public static void RunDemo(string accessString, DataAccess sqlAccess)
        {
            using (SqlConnection connection = new SqlConnection(accessString))
            {
                connection.Open();
                //When the application runs, a new table should be created with three columns: 1) ChoreId 2) ChoreName 3) ChoreAssigment
                sqlAccess.CreateDatabase(connection);
                sqlAccess.CreateChoreTable(connection);
                //When the application runs, three new chores should should be inserted into the database.
                int rowsAffected;

                rowsAffected = sqlAccess.AddChore(connection, "Dishes", "Steven");
                Console.WriteLine(rowsAffected + " row(s) inserted");

                rowsAffected = sqlAccess.AddChore(connection, "Vaccume", "Kyndra");
                Console.WriteLine(rowsAffected + " row(s) inserted");

                rowsAffected = sqlAccess.AddChore(connection, "Mow Lawn", "Steven");
                Console.WriteLine(rowsAffected + " row(s) inserted");
                //When the application runs, a chore should be updated
                rowsAffected = sqlAccess.UpdateChore(connection, 1, "Zed");
                Console.WriteLine(rowsAffected + " row(s) updated");
                //When the application runs, a chore should be deleted
                rowsAffected = sqlAccess.DeleteChore(connection, 2);
                Console.WriteLine(rowsAffected + " row(s) deleted");
                //When the application runs, all chores should be displayed

                var choresToPrint = sqlAccess.GetChores(connection);
                //print
                foreach (var chore in choresToPrint)
                {
                    Console.WriteLine(chore);
                }

                //The programming functionality should not live in the Main method.Attempt to break to code into methods
            }
        }
    
    }
}
