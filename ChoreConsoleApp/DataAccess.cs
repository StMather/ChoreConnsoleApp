using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ChoreConsoleApp
{

    class DataAccess
    {

        public string ConnectionString()
        {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DESKTOP-QVJQMKJ";
                builder.UserID = "Admin";
                builder.Password = "LeapPa55word";
                builder.InitialCatalog = "master";
                return builder.ConnectionString;

        }
        public void CreateDatabase(SqlConnection connection)
        {
            Console.Write("Creating Database ChoresDB...");
            String sql = "DROP DATABASE IF EXISTS [ChoresDB]; CREATE DATABASE [ChoresDB]";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Done.");
            }

        }
        public void CreateChoreTable(SqlConnection connection)
        {
            Console.Write("Creating Table Chores...");
            string sql = "USE ChoresDB; "+
                "CREATE TABLE Chores (ChoreId INT IDENTITY(1,1) NOT NULL PRIMARY KEY, "+
                " ChoreName NVARCHAR(MAX), "+
                " ChoreAssignment NVARCHAR(MAX) "+
                "); ";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Done.");
            }

        }
        public int AddChore(SqlConnection connection, string choreName, string choreAssignment)
        {
            Console.WriteLine($"Adding {choreName} to {choreAssignment}'s chores...");

           string sql = "USE ChoresDB INSERT INTO Chores (ChoreName, ChoreAssignment) " +
                $"VALUES ('{choreName}', '{choreAssignment}');";
            using (SqlCommand command = new SqlCommand(sql, connection))
            { 
                return command.ExecuteNonQuery();
            }

        }
        public int UpdateChore(SqlConnection connection, int choreToUpdate, string choreAssignment)
        {
            Console.WriteLine($"Assigning {choreToUpdate} to {choreAssignment}");
            string sql = $"USE ChoresDB UPDATE Chores SET ChoreAssignment = '{choreAssignment}' WHERE ChoreId = '{choreToUpdate}'";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                return command.ExecuteNonQuery();
            }
            
        }
        public int DeleteChore(SqlConnection connection, int choreToDelete)//////fix and using
        {
            Console.WriteLine($"Deleting {choreToDelete}");
            string sql = $"USE ChoresDB Delete FROM Chores WHERE ChoreId = '{choreToDelete}';";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                return command.ExecuteNonQuery();
            }
            
        }
        public List<string> GetChores(SqlConnection connection)/////fix and using
        {
            string sql = "USE ChoresDB SELECT ChoreId, ChoreName, ChoreAssignment FROM Chores;";
            var choresToReturn = new List<string>();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        choresToReturn.Add($"{reader.GetInt32(0)}.{reader.GetString(1)} assigned to: {reader.GetString(2)}");
                    }
                }
            }
            return choresToReturn;
            
         
        }
    }
}
