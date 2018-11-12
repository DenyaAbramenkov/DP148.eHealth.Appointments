using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class AppointmentBill
    {
        int AppointmentId { get; set; }

        Guid InvoiceNumber { get; set; }

        int Amount { get; set; }

        bool IsCompleted { get; set; }

        bool IsDeleted { get; set; }
    }
}
