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
    public class PatientsController
    {
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;

        public PatientsController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _ConnectionString = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build().GetConnectionString("MyMedicalDatabase");
        }

        [HttpGet]
        public JsonResult Get()
        {
            PatientsDataConnetion conn = new PatientsDataConnetion(_ConnectionString);
            List<PatientModel> patients = conn.SelectCommand();
            return new JsonResult(patients);
        }

        [HttpPost]
        public JsonResult Post([FromBody] PatientModel newPat)
        {
            PatientsDataConnetion conn = new PatientsDataConnetion(_ConnectionString);
            string result = conn.InsertCommand(newPat);
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete([FromBody] PatientModel curPat)
        {
            PatientsDataConnetion conn = new PatientsDataConnetion(_ConnectionString);
            string result = conn.DeleteCommand(curPat);
            return new JsonResult(result);
        }


        [HttpPut]
        public JsonResult Put([FromBody] PatientModel curPat)
        {
            PatientsDataConnetion conn = new PatientsDataConnetion(_ConnectionString);
            string result = conn.UpdateCommand(curPat);
            return new JsonResult(result);
        }
    }
}
