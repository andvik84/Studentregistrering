using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentregistrering.DataAccess.Data
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        internal StudentDbContext _ctx;
        public GenericRepository(StudentDbContext ctx)
        {
            _ctx = ctx;
        }
        public T Add(T entity)
        {
            var added = _ctx.Add(entity).Entity;
            _ctx.SaveChanges();
            return added;
        }

        public void Delete(T entity)
        {
            _ctx.Remove(entity);
            _ctx.SaveChanges();
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            var all = _ctx.Set<T>().ToList();
            return all;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            var updatedEntity = _ctx.Update(entity).Entity;
            _ctx.SaveChanges();
            return updatedEntity;
        }
    }
}
