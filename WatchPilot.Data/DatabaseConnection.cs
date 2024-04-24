using System.Data.SqlTypes;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
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

        public List<T> ExecuteQueries<T>(string Query, Func<SqlDataReader, T> mapFunction)
        {
            List<T> result = new List<T>();

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
                                while (dataReader.Read())
                                {
                                    T obj = mapFunction(dataReader);
                                    result.Add(obj);

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



        public int ExecuteNonQuery(string Query, SqlParameter[] parameters)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlTransaction transaction = cnn.BeginTransaction())
                {
                    int rows = 0;
                    try
                    {
                        using (SqlCommand command = new SqlCommand(Query, cnn, transaction))
                        {
                            command.Parameters.AddRange(parameters);
                            rows =command.ExecuteNonQuery();
                        }

                        
                        transaction.Commit();
                        return rows;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back. Error: " + ex.Message);
                        return rows;
                    }

                    
                }
            }
        }



        public int ExecuteScalar(string Query, SqlParameter[] parameters)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlTransaction transaction = cnn.BeginTransaction())
                {
                    int identity = 0;
                    try
                    {
                        using (SqlCommand command = new SqlCommand(Query, cnn, transaction))
                        {
                            command.Parameters.AddRange(parameters);
                            object result = command.ExecuteScalar();
                            if (result != DBNull.Value)
                            {
                                identity = Convert.ToInt32(result);
                            }
                        }


                        transaction.Commit();
                        
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back. Error: " + ex.Message);
                        throw new Exception("Error in ExecuteScalar");
                    }

                    return identity;
                }
            }
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
