namespace Nbibernate.Model
{
    public class DbModel
    {
        //字段跟数据库表列一样
        public virtual int Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }
        public virtual string RegisterDate { get; set; }
    }
}