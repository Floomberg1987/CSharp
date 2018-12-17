using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            EFDB();
            ADONET();
            Console.ReadLine();
        }
        static void EFDB()
        {
            using (AdventureWorks2017Entities db = new AdventureWorks2017Entities())
            {
                var stores = db.Stores;
                if (stores != null && stores.Any())
                {
                    foreach (var s in stores)
                    {
                        Console.Write(s.BusinessEntityID + "\t");
                    }
                }

                Console.WriteLine("\n\n\nThere are totally {0} stores", stores.Count());

            }
        }
        static void ADONET()
        {
            string conString = ConfigurationManager.ConnectionStrings["conString"].ToString();
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlConnection conn = new SqlConnection(conString);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string selectSQL = "select * from INFORMATION_SCHEMA.tables";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = selectSQL;
                cmd.ExecuteNonQuery();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            Console.Write(ds.Tables[0].Rows[i][j] + "\t");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
