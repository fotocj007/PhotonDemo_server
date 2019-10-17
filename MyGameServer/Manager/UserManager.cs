using System.Collections.Generic;
using MyGameServer.Model;
using NHibernate.Criterion;

namespace MyGameServer.Manager
{
    public class UserManager : IUserManager
    {
        public void Add(User msg)
        {
            ////session.Close();如果使用下面的using，就不用写session.Close()来释放session了，因为using会自动释放。下面的嵌套是先释放transacion,再释放session。
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(msg);
                    transaction.Commit();
                }
            }
        }

        public void Update(User msg)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(msg);
                    transaction.Commit();
                }
            }
        }

        public void Remove(User msg)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(msg);
                    transaction.Commit();
                }
            }
        }

        public User GetById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var msg = session.Get<User>(id);
                    transaction.Commit();
                    return msg;
                }
            }
        }

        public User GetByName(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var iCriteria = session.CreateCriteria(typeof(User));
                iCriteria.Add(Restrictions.Eq("UserName", name));

                //使用 UniqueResult 查询时,如果有多个满足的,这会报错
//                var msg = iCriteria.UniqueResult<User>();

                //使用List查询,这里取第一个返回
                var msgArr = iCriteria.List<User>();
                if (msgArr.Count > 0) return msgArr[0];

                return null;
            }
        }

        public ICollection<User> GetAllDb()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var msgArr = session.CreateCriteria(typeof(User)).List<User>();

                return msgArr;
            }
        }

        public bool VerifyModel(string name, string password)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var iCriteria = session.CreateCriteria(typeof(User));

                //多条件查询
                iCriteria.Add(Restrictions.Eq("UserName", name))
                    .Add(Restrictions.Eq("Password", password));

                var msgArr = iCriteria.List<User>();

                return msgArr.Count > 0;
            }
        }
    }
}