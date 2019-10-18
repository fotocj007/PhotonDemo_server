using System.Collections.Generic;
using Nbibernate.Model;
using NHibernate.Criterion;
using NHibernate.Util;
using System.Dynamic;

namespace Nbibernate.Manager
{
    public class DbManager : IDbManager
    {
        public void Add(DbModel msg)
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

        public void Update(DbModel msg)
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

        public void Remove(DbModel msg)
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

        public DbModel GetById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var msg = session.Get<DbModel>(id);
                    transaction.Commit();
                    return msg;
                }
            }
        }

        public DbModel GetByName(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var iCriteria = session.CreateCriteria(typeof(DbModel));
                iCriteria.Add(Restrictions.Eq("Name", name));
                
                //使用 UniqueResult 查询时,如果有多个满足的,这会报错
                //DbModel msg = iCriteria.UniqueResult<DbModel>();

                //使用List查询,这里取第一个返回
                DbModel msg = null;
                var msgArr = iCriteria.List<DbModel>();
    
                if ((bool) msgArr.FirstOrNull()) msg = msgArr[0];

                return msg;
            }
        }

        public ICollection<DbModel> GetAllDb()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var msgArr = session.CreateCriteria(typeof(DbModel)).List<DbModel>();

                return msgArr;
            }
        }

        public bool VerifyModel(string name, string age)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var iCriteria = session.CreateCriteria(typeof(DbModel));
                
                //多条件查询
                iCriteria.Add(Restrictions.Eq("UserName", name))
                    .Add(Restrictions.Eq("RegisterDate", age));

                DbModel msg = null;
                var msgArr = iCriteria.List<DbModel>();
                if (msgArr.Any()) return true;

                return false;
            }
        }
    }
}