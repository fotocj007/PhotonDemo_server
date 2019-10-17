using System;
using MySql.Data.MySqlClient;

namespace ConsoleDemo
{
    internal class Program
    {
        static string connect = "server=127.0.0.1;port=3306;database=webdemo;user=root;password=foto_cj1";
        static MySqlConnection conn = new MySqlConnection(connect);
    
        public static void Main(string[] args)
        {
            setSql();
        }

        public static void setSql()
        {
            try
            {
                conn.Open();
        
//        string sql = "select * from netweb";
//        MySqlCommand cmd = new MySqlCommand(sql, conn);
//                cmd.ExecuteReader(); //执行一些查询
//                cmd.ExecuteNonQuery(); //插入 删除
//                cmd.ExecuteScalar(); //查询返回单个值

//        MySqlDataReader reader = cmd.ExecuteReader();
//        while (reader.Read())
//        {
//          Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[1].ToString());
//        }

                string inser = "insert into netweb(name,age,title) values ('cjdww',34,'geee')";
                MySqlCommand cmd = new MySqlCommand(inser, conn);
                int result = cmd.ExecuteNonQuery(); //返回影响的行数
                Console.WriteLine(result);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}