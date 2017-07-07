using CBRFConverter;
using CBRFConverter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ValutesApi
{
    public static class DBMethods
    {
        static string connectionString = @"Data Source=106PC0051;Initial Catalog=ExchRates;Integrated Security=True";
        public static void CreateEnumValute(EnumValutes[] toLoad)
        {
            string sqlExpression = "sp_InsertEnum";
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                sqlConn.Open();
                foreach (EnumValutes tmp in toLoad)
                {
                    var sqlCmd = new SqlCommand(sqlExpression, sqlConn);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@Vcode", tmp.Vcode.Trim());
                    sqlCmd.Parameters.AddWithValue("@Vname", tmp.Vname.Trim());
                    sqlCmd.Parameters.AddWithValue("@VEngname", tmp.VEngname.Trim());
                    sqlCmd.Parameters.AddWithValue("@Vnom", tmp.Vnom.Trim());
                    sqlCmd.Parameters.AddWithValue("@VcommonCode", tmp.VcommonCode.Trim());
                    if (tmp.VnumCode != null)
                        sqlCmd.Parameters.AddWithValue("@VnumCode", tmp.VnumCode.Trim());
                    else
                        sqlCmd.Parameters.AddWithValue("@VnumCode", "");
                    if (tmp.VcharCode != null)
                        sqlCmd.Parameters.AddWithValue("@VcharCode", tmp.VcharCode.Trim());
                    else
                        sqlCmd.Parameters.AddWithValue("@VcharCode", "");
                    sqlCmd.ExecuteNonQuery();
                }
            }
        }
      
        public static bool AddCurse(string val, List<DayCursePairs> points)
        {
            if (Startup.codesList.ContainsKey(val))
            {
                string code = Startup.codesList[val];
                string sqlExpression = "sp_InsertCurs";
                using (SqlConnection sqlConn = new SqlConnection(connectionString))
                {
                    sqlConn.Open();
                    for (int i=0; i<points.Count; i++)
                    {
                        var sqlCmd = new SqlCommand(sqlExpression, sqlConn);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@code", Startup.codesList[val]);
                        sqlCmd.Parameters.AddWithValue("@date", points[i].Day);
                        sqlCmd.Parameters.AddWithValue("@curs", points[i].Curse);
                        sqlCmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            return false;
        }
        public static void DropEnumValute()
        {

        }
        public static void DropDynamic()
        {

        }
        public static void GetValuteCurse()
        {

        }
        public static void GetEnumValutes()
        {
            //название процедуры
            string sqlExpression = "sp_GetCurses";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                //указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);
                        string cdate = reader.GetString(1);
                        string curs = reader.GetString(2);
                        Console.WriteLine("{0} \t{1} \t{2}", code, cdate, curs);
                    }
                }
                reader.Dispose();
            }
        }
    }
}
