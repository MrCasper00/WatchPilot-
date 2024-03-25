using System.Data.SqlTypes;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;

namespace WatchPilot.Data
{
    public class DatabaseConnection
    {
        private string connectionString = "Server=mssqlstud.fhict.local;Database=dbi532263;User Id=dbi532263;Password=BrownGreen78!;Max Pool Size=200;TrustServerCertificate=true";

        public T ExecuteQuery<T>(string Query, Func<SqlDataReader, T> mapFunction)
        {
            T result = default(T); // Initialize the result with the default value of the specified type

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlTransaction transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(Query, cnn, transaction))
                        {
                            using (SqlDataReader dataReader = command.ExecuteReader())
                            {
                                if (dataReader.Read())
                                {
                                    // Map the data from the dataReader to the specified type
                                    T obj = mapFunction(dataReader);
                                    result = (obj);
                                    
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back. Error: " + ex.Message);
                    }
                }
            }

            return result;
        }








        //public void ExecuteQuery(string Query)
        //{
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        cnn.Open();
        //        using (SqlTransaction transaction = cnn.BeginTransaction())
        //        {
        //            try
        //            {
        //                using (SqlCommand command = new SqlCommand(Query, cnn, transaction))
        //                {
        //                    using (SqlDataReader dataReader = command.ExecuteReader())
        //                    {
        //                        while (dataReader.Read())
        //                        {
        //                            Console.WriteLine(dataReader[0] + " -- " + dataReader[1]);
        //                        }
        //                    }
        //                }
        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                Console.WriteLine("Transaction rolled back. Error: " + ex.Message);
        //            }
        //        }
        //    }
        //}

        //public void ExecuteTransaction(string[] queries)
        //{
        //    using (SqlConnection cnn = new SqlConnection(connectionString))
        //    {
        //        cnn.Open();
        //        using (SqlTransaction transaction = cnn.BeginTransaction())
        //        {
        //            try
        //            {
        //                foreach (string query in queries)
        //                {
        //                    using (SqlCommand command = new SqlCommand(query, cnn, transaction))
        //                    {
        //                        command.ExecuteNonQuery();
        //                    }
        //                }
        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                Console.WriteLine("Transaction rolled back. Error: " + ex.Message);
        //            }
        //        }
        //    }
        //}

    }
}
