using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW_DB_Backend.DBConnectionModules;
using CW_DB_Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CW_DB_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class PatientAttendingDoctorsController
    {
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;

        public PatientAttendingDoctorsController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _ConnectionString = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build().GetConnectionString("MyMedicalDatabase");
        }

        [HttpGet]
        public JsonResult Get()
        {
            PatientAttendingDoctorsDataConnection conn = new PatientAttendingDoctorsDataConnection(_ConnectionString);
            List<PatientAttendingDoctorsModel> doctors = conn.SelectCommand();
            return new JsonResult(doctors);
        }

        [HttpPost]
        public JsonResult Post([FromBody] PatientAttendingDoctorsModel newPatDoct)
        {
            PatientAttendingDoctorsDataConnection conn = new PatientAttendingDoctorsDataConnection(_ConnectionString);
            string result = conn.InsertCommand(newPatDoct);
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete([FromBody] PatientAttendingDoctorsModel curPatDoct)
        {
            PatientAttendingDoctorsDataConnection conn = new PatientAttendingDoctorsDataConnection(_ConnectionString);
            string result = conn.DeleteCommand(curPatDoct);
            return new JsonResult(result);
        }


        [HttpPut]
        public JsonResult Put([FromBody] PatientAttendingDoctorsModel curPatDoct)
        {
            PatientAttendingDoctorsDataConnection conn = new PatientAttendingDoctorsDataConnection(_ConnectionString);
            string result = conn.UpdateCommand(curPatDoct);
            return new JsonResult(result);
        }
    }
}
