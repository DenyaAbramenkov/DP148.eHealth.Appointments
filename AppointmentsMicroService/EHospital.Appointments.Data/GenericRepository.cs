using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHospital.Appointments.Model;
using Microsoft.EntityFrameworkCore;

namespace EHospital.Appointments.Data
{
    /// <summary>
    /// Realisation of abstract IGenericRepository
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
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
        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.Where(e => e.IsDeleted != true).ToListAsync();
        }

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="id">Id of Entity's</param>
        /// <returns>Entity.</returns>
        public async Task<T> GetById(int id)
        {
            return await entities.FirstOrDefaultAsync(e => e.IsDeleted != true && e.Id == id);
        }

        /// <summary>
        /// Create new Entity.
        /// </summary>
        /// <param name="entity">Entity to Create.</param>
        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Can't create entity");
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
                throw new ArgumentNullException("Can't update entity");
            }
            entities.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
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
                throw new ArgumentNullException("Can't delete entity");
            }
            entities.Attach(entity);
            var entry = context.Entry(entity);
            entry.Property("IsDeleted").IsModified = true;
            context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Save Changes in DataBase.
        /// </summary>
        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}