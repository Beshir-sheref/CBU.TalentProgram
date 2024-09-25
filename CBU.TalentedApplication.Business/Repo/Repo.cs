using CBU.TalentedApplication.Business.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CBU.TalentedApplication.Business.Repo
{
    public class Repo<T> where T : class
    {
        protected TalenedSystemContext db = new TalenedSystemContext();

        protected DbSet<T> DbSet => db.Set<T>();

        public List<T> Search(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Where(predicate).ToList();
        }

        public T Find(int id)
        {
            return db.Set<T>().Find(id);
        }
        

        public T Add(T item)
        {
            db.Set<T>().Add(item);

            SaveChanges();

            return item;
        }
        public void Update(T item)
        {
            db.Set<T>().Attach(item);
            db.Entry(item).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(int id)
        {
            db.Set<T>().Remove(Find(id));

            SaveChanges();
        }
        protected void SaveChanges()
        {
            db.SaveChanges();
        }
        public string ConvertToMD5(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            byte[] hashData = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(text));

            return Convert.ToBase64String(hashData);
        }
    }
}





