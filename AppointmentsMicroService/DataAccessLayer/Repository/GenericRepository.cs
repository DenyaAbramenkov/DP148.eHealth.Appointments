using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        AppointmentContext _context;
        DbSet<T> _dbSet;

        public GenericRepository(AppointmentContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _dbSet = _context.Set<T>();
            _context.SaveChanges();
        }

        public T Get<TKey>(TKey id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public void Update(T entity)
        {
            _context.SaveChanges();
        }

    }
}

