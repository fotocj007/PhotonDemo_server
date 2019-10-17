using System.Collections.Generic;
using Nbibernate.Model;

namespace Nbibernate.Manager
{
    internal interface IDbManager
    {
        void Add(DbModel msg);
        void Update(DbModel msg);
        void Remove(DbModel msg);

        DbModel GetById(int id);
        DbModel GetByName(string name);
        ICollection<DbModel> GetAllDb();

        bool VerifyModel(string name, string registerdate);
    }
}