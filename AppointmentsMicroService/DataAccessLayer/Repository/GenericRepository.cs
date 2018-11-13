using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.AppointmentContext;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Realisation of abstract IGenericRepository
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// DataBase Context.
        /// </summary>
        private readonly EHospitalContext context;

        /// <summary>
        /// Set of Entities.
        /// </summary>
        private DbSet<T> entities;

        /// <summary>
        /// Erorr Message.
        /// </summary>
        readonly string errorMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository(DbContext)"/> class.
        /// </summary>
        /// <param name="context">DB context.</param>
        public GenericRepository(EHospitalContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        /// <summary>
        /// GetAll Entities.
        /// </summary>
        /// <returns>Set of Entities.</returns>
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="id">Id of Entity's</param>
        /// <returns>Entity.</returns>
        public T GetById(int id)
        {
            return entities.Find(id);
        }

        /// <summary>
        /// Create new Entity.
        /// </summary>
        /// <param name="entity">Entity to Create.</param>
        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Update Entity.
        /// </summary>
        /// <param name="entity">Entity with Update</param>
        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Delete Entity.
        /// </summary>
        /// <param name="entity">Entity to Delete.</param>
        public T Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// Save Changes in DataBase.
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}