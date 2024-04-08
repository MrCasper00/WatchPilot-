using System.Data.SqlTypes;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WatchPilot.Data
{
    public class DatabaseConnection
    {
        private string connectionString = "";
        public DatabaseConnection(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }


        public T ExecuteQuery<T>(string Query, Func<SqlDataReader, T> mapFunction)
        {
            T result = default(T); 

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
