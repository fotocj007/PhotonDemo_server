using System.Collections.Generic;
using MyGameServer.Model;

namespace MyGameServer.Manager
{
    internal interface IUserManager
    {
        void Add(User msg);
        void Update(User msg);
        void Remove(User msg);

        User GetById(int id);
        User GetByName(string name);
        ICollection<User> GetAllDb();

        bool VerifyModel(string name, string password);
    }
}