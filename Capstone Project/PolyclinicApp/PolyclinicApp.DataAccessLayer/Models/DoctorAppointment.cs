using System;
using System.Collections.Generic;
using System.Text;

namespace PolyclinicApp.DataAccessLayer.Models
{
    public class DoctorAppointment
    {
        public string DoctorName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string PatientId { get; set; } = null!;
        public string PatientName { get; set; } = null!;
        public int AppointmentNo { get; set; }

    }
}
