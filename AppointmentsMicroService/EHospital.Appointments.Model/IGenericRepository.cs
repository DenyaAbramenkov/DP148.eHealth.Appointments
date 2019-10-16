using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHospital.Appointments.Model
{
    /// <summary>
    /// Abstract generic repository with CRUD operations.
    /// </summary>
    /// <typeparam name="T">Entity.</typeparam>
    public interface IGenericRepository<T> where T : IEntity
    {
        /// <summary>
        /// Get all Entities 
        /// </summary>
        /// <returns>Set of Entities.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Get Entity by Id.
        /// </summary>
        /// <param name="id">Entity's Id.</param>
        /// <returns>Entity.</returns>
        Task<T> GetById(int id);

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
        Task SaveChanges();
    }
}