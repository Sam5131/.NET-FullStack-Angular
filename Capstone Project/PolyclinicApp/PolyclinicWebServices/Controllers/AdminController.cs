using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PolyclinicApp.DataAccessLayer;
using PolyclinicApp.DataAccessLayer.Models;

namespace PolyclinicWebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : Controller
    {
        //Create repository object

        public AdminController(PolyclinicRepository repos)
        {
            //Implement the logic here
            
            
        }
        public JsonResult GetAllPatientDetails()
        {
            //Implement the logic here 

           return Json(null);
        }

        public JsonResult GetPatientDetails(string patientId)
        {
            //Implement the logic here
            return null;
        }

        public JsonResult AddNewPatientDetails(Models.Patient patient)
        {
            //Implement the logic here
            bool status = false;
            return Json(status);
        }

        public JsonResult UpdatePatientAge(string patientId, byte age)
        {
            //Implement the logic here
            bool status = false;
            return Json(status);
        }

        public JsonResult CancelAppointment(int appointmentNo)
        {
            //Implement the logic here
            int status = 0;
            return Json(status);
        }

    }
}
