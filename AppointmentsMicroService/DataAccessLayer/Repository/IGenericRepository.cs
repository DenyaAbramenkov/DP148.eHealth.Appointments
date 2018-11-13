using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Abstract generic repository with CRUD operations.
    /// </summary>
    /// <typeparam name="T">Entity.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all Entities 
        /// </summary>
        /// <returns>Set of Entities.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get Entity by Id.
        /// </summary>
        /// <param name="id">Entity's Id.</param>
        /// <returns>Entity.</returns>
        T GetById(int id);

        /// <summary>
        /// Create new Entity.
        /// </summary>
        /// <param name="entity">Entity to create.</param>
        T Create(T entity);

        /// <summary>
        /// Update Entity.
        /// </summary>
        /// <param name="entity">Entity with Update</param>
        T Update(T entity);

        /// <summary>
        /// Entity Delete.
        /// </summary>
        /// <param name="entity">Entity to Delete</param>
        T Delete(T entity);

        /// <summary>
        /// Save changes in DataBase.
        /// </summary>
        void SaveChanges();
    }
}