using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Console;

namespace ChoreConsoleApp
{
    class UserInput
    {
        public static void InputLoop(string accessString, DataAccess sqlAccess)
        {
            using (SqlConnection connection = new SqlConnection(accessString))
            {
                connection.Open();
                bool loop = true;
                int input = 0;
                while (loop)
                {
                    Clear();
                    Console.WriteLine("What would you like to do with the Chores database?");
                    Console.WriteLine("1. Add a Chore.");
                    Console.WriteLine("2. Update a Chore.");
                    Console.WriteLine("3. Delete a Chore.");
                    Console.WriteLine("4. Show the Chores.");
                    Console.WriteLine("5. Exit.");
                    Int32.TryParse(Console.ReadLine(), out input);//returns true or false

                    switch (input)
                    {
                        case 1:
                            //Add chore
                            UserAddChore(connection, sqlAccess);
                            Hold();
                            break;
                        case 2:
                            //Update Chore
                            UserUpdateChore(connection, sqlAccess);
                            Hold();
                            break;
                        case 3:
                            //Delete Chore
                            UserDeleteChore(connection, sqlAccess);
                            Hold();
                            break;
                        case 4:
                            //Print Chores
                            PrintChores(connection, sqlAccess);
                            Hold();
                            break;
                        case 5:
                            //Exit
                            loop = false;
                            break;
                    }
                    
                    
                }
            }
        }
        private static void UserAddChore(SqlConnection connection, DataAccess sqlAccess)
        {
            
            Clear();
            Console.Write("Enter the Chore you would like to add:");
            string choreToEnter = Console.ReadLine();
            Console.Write("Enter the name of the person the Chore is assigned to: ");
            string nameToEnter = Console.ReadLine();
            int rowsAffected = sqlAccess.AddChore(connection, choreToEnter, nameToEnter);
            Console.WriteLine(rowsAffected + " row(s) Added");
        }
        private static void UserUpdateChore(SqlConnection connection, DataAccess sqlAccess)
        {
            Clear();
            PrintChores(connection, sqlAccess);
            int index;
            Console.Write("Enter the index of the Chore to update:");
            Int32.TryParse(Console.ReadLine(), out index);//returns true or false
            Console.Write("Enter the name of the person the Chore is assigned to: ");
            string nameToEnter = Console.ReadLine();
            int rowsAffected = sqlAccess.UpdateChore(connection, index, nameToEnter);
            Console.WriteLine(rowsAffected + " row(s) Updated");
        }
        private static void UserDeleteChore(SqlConnection connection, DataAccess sqlAccess)
        {
            Clear();
            PrintChores(connection, sqlAccess);
            int index;
            Console.Write("Enter the index of the Chore to update:");
            Int32.TryParse(Console.ReadLine(), out index);//returns true or false
            int rowsAffected = sqlAccess.DeleteChore(connection, index);
            Console.WriteLine(rowsAffected + " row(s) Updated");
        }
        private static void Hold()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
        private static void PrintChores(SqlConnection connection, DataAccess sqlAccess)
        {
            var choresToPrint = sqlAccess.GetChores(connection);
            //print
            foreach (var chore in choresToPrint)
            {
                Console.WriteLine(chore);
            }
        }
    }
}
