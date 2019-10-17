using System;
using System.Collections.Generic;
using Nbibernate.Manager;
using Nbibernate.Model;
using NHibernate;
using NHibernate.Cfg;

namespace Nbibernate
{
    internal class Program
    {
        public static void Main(string[] args)
        {
//            DbModel msg = new DbModel() { Name = "gggggKddd",Title = "jttttt", Age = 100};
//            IDbManager manager = new DbManager();
//            manager.Add(msg);

//            IDbManager manager = new DbManager();
//            DbModel msg = new DbModel() {Id = 8, Name = "kokoko", Age = 100, Title = "okoko"};
//            manager.Update(msg);
//
//            DbModel msg = new DbModel() { Id = 10};
//            manager.Remove(msg);


            IDbManager manager = new DbManager();
            
            //按照主键ID查询
            DbModel msg = manager.GetById(2);
            Console.WriteLine(msg.UserName);
            Console.WriteLine(msg.Password);
            
            

//            //按照name查询
//            DbModel msgv2 = manager.GetByName("cjcjcc");
//            Console.WriteLine(msgv2.Id);
//            Console.WriteLine(msgv2.Age);
//            
//            //获取所有数据
//            ICollection<DbModel> msgArr = manager.GetAllDb();
//            foreach (DbModel u in msgArr)
//            {
//                Console.WriteLine(u.Id + " " + u.Name + " " + u.Age);
//            }
//
//            //根据name和age查询,判断是否存在
            bool sit = manager.VerifyModel("foddd","12122");
            Console.WriteLine(sit);
//            
            
            Console.ReadKey();


//            var configuration = new Configuration();
//            configuration.Configure();               //解析hibernate.cfg配置文件
//            configuration.AddAssembly("Nbibernate"); //解析 映射文件 DbModel.hbm.xml (有的版本在映射文件添加了,该处可以省略)
//
//            ISessionFactory sessFa = null;
//            ISession session = null;
//            ITransaction transaction = null;
//            try
//            {
//                sessFa = configuration.BuildSessionFactory();
//                session = sessFa.OpenSession();    //打开一个跟数据库的回话
//                transaction = session.BeginTransaction();  //打开一个事务
//
//                var msg = new DbModel {Name = "Kiddd1111", Age = 13, Title = "Logeg"};
//                var msg2 = new DbModel {Name = "L2222", Age = 23, Title = "tiles"};
//
//                session.Save(msg);
//                session.Save(msg2);
//                transaction.Commit();
//
//                Console.WriteLine("ddddddd");
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }
//            finally
//            {
//                transaction?.Dispose();  //c#的新语法 ,如果不为null,则关闭
//                session?.Close();
//                sessFa?.Close();
//            }
//
//            Console.WriteLine("dddddddd");
//            Console.ReadKey();
        }
    }
}