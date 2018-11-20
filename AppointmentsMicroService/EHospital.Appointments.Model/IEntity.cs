namespace EHospital.Appointments.Model
{
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets entity ID.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets a soft delete property.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
