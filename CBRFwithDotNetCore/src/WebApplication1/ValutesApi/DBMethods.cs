using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ValutesApi
{
    public static class DBMethods
    {
        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ExchRates;Integrated Security=True";
        public static void CreateValute()
        {

        }
        public static void GetEnumValutes()
        {
            // название процедуры
            //string sqlExpression = "sp_GetCurses";

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(sqlExpression, connection);
            //    // указываем, что команда представляет хранимую процедуру
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    var reader = command.ExecuteReader();

            //    if (reader.HasRows)
            //    {
            //        Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

            //        while (reader.Read())
            //        {
            //            string id = reader.GetString(0);
            //            string name = reader.GetString(1);
            //            string age = reader.GetString(2);
            //            Console.WriteLine("{0} \t{1} \t{2}", id, name, age);
            //        }
            //    }
            //    reader.Dispose();
            //}
        }

    }
}
