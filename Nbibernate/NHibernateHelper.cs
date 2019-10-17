using NHibernate;
using NHibernate.Cfg;

namespace Nbibernate
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var config = new Configuration();
                    config.Configure(); //解析nhibernate.cfg配置文件
                    config.AddAssembly("Nbibernate"); //解析 映射文件

                    _sessionFactory = config.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            ////打开一个跟数据库的回话
            return SessionFactory.OpenSession();
        }
    }
}