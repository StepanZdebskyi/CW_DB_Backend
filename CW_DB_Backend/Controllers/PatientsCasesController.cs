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
    public class PatientsCasesController
    {
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;

        public PatientsCasesController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _ConnectionString = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build().GetConnectionString("MyMedicalDatabase");
        }

        [HttpGet]
        public JsonResult Get()
        {
            PatientCasesDataConnection conn = new PatientCasesDataConnection(_ConnectionString);
            List<PatientCaseModel> spec = conn.SelectCommand();
            return new JsonResult(spec);
        }

        [HttpPost]
        public JsonResult Post([FromBody] PatientCaseModel newPatCase)
        {
            PatientCasesDataConnection conn = new PatientCasesDataConnection(_ConnectionString);
            string result = conn.InsertCommand(newPatCase);
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete([FromBody] PatientCaseModel curPatCase)
        {
            PatientCasesDataConnection conn = new PatientCasesDataConnection(_ConnectionString);
            string result = conn.DeleteCommand(curPatCase);
            return new JsonResult(result);
        }


        [HttpPut]
        public JsonResult Put([FromBody] PatientCaseModel curPatCase)
        {
            PatientCasesDataConnection conn = new PatientCasesDataConnection(_ConnectionString);
            string result = conn.UpdateCommand(curPatCase);
            return new JsonResult(result);
        }
    }
}
