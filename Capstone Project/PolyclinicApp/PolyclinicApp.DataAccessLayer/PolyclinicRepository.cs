using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PolyclinicApp.DataAccessLayer.Models;

namespace PolyclinicApp.DataAccessLayer
{
    public class PolyclinicRepository(PolyclinicDbContext context)
    {
        // Returns all patients or null on error
        public List<Patient> GetAllPatients()
        {
            try
            {
                return context.Patients.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Patient GetPatientDetails(string PatientId)
        {
            Patient patient;
            try
            {
                patient = (from p in context.Patients
                           where p.PatientId == PatientId
                           select p).FirstOrDefault();
            }
            catch (Exception)
            {
                patient = null;
            }
            return patient;
        }
        public bool AddNewPatientDetails(Patient patientObj)
        {
            bool status;
            try
            {
                if (patientObj != null)
                {
                    context.Patients.Add(patientObj);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public bool UpdatePatientAge(string patientId, byte newAge)
        {
            bool status;
            try
            {
                if (patientId != null)
                {
                    Patient patient = (from p in context.Patients
                                       where p.PatientId == patientId
                                       select p).FirstOrDefault();
                    patient.Age = newAge;
                    using var newContext = new PolyclinicDbContext();
                    newContext.Patients.Update(patient);
                    newContext.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public int CancelAppointment(int appointmentNo)
        {
            int status;
            try
            {
                Appointment appointment = (from a in context.Appointments
                                           where a.AppointmentNo == appointmentNo
                                           select a).FirstOrDefault();
                if(appointment != null)
                {
                    using var newContext = new PolyclinicDbContext();
                    newContext.Appointments.Remove(appointment);
                    newContext.SaveChanges();
                    status = 1;
                }
                else
                {
                    status = -1;
                }
            }
            catch { status = -99; }
            return status;
        }
        public List<DoctorAppointment> FetchAllAppointments(string doctorId, DateTime date)
        {
            List<DoctorAppointment> lstAppointment = new();

            SqlParameter prmDoctorId = new("@doctorId", doctorId);
            SqlParameter prmDate = new("@date", date)
            {
                SqlDbType = System.Data.SqlDbType.DateTime
            };

            try
            {
                lstAppointment = context.DoctorAppointments.FromSqlRaw("SELECT * FROM ufn_FetchAllAppointments(@doctorid, @date)", prmDoctorId, prmDate).ToList();
            }
            catch (Exception)
            {
                lstAppointment = null;
            }
            return lstAppointment;
        }
        public decimal CalculateDoctorFees(string doctorId, DateTime date)
        {
            decimal result = 0;
            try
            {
                if(doctorId != null)
                {
                    result = (from d in context.Doctors
                              select PolyclinicDbContext.ufn_CalcualteDoctorFees(doctorId, date)).FirstOrDefault();
                }
            }
            catch { result = -99; }
            return result;
        }
        public int GetDoctorAppointment(string patientId, string doctorId, DateTime dateOfAppointment, out int appointmentNo)
        {
            appointmentNo = 0;
            int noOfRowsAffected = 0;
            int result = 0;

            SqlParameter prmPatientId = new("@PatientId", patientId);

            SqlParameter prmDateOfAppointment = new("@date", dateOfAppointment)
            {
                DbType = System.Data.DbType.DateTime
            };

            SqlParameter prmDoctorId = new("@DoctorId", doctorId);

            SqlParameter prmAppointmentNo = new("@appointmentNo", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            SqlParameter prmResult = new SqlParameter("@result", System.Data.SqlDbType.Int);
            prmResult.Direction = System.Data.ParameterDirection.Output;

            try
            {
                noOfRowsAffected = context.Database.ExecuteSqlRaw("EXEC @result = usp_GetDoctorAppointment @patientId, @DoctorId, @date, @appointmentNo OUT", prmResult, prmPatientId, prmDoctorId, prmDateOfAppointment, prmAppointmentNo);
                result = Convert.ToInt32(prmResult.Value);
                appointmentNo = Convert.ToInt32(prmAppointmentNo.Value);
            }
            catch
            {
                appointmentNo = 0;
                result = -99;
            }
            return result;
        }
    }
}
