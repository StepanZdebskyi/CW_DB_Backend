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
    public class VacantionsSheduleController
    {
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;

        public VacantionsSheduleController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _ConnectionString = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build().GetConnectionString("MyMedicalDatabase");
        }

        [HttpGet]
        public JsonResult Get()
        {
            VacantionsSheduleDataConnection conn = new VacantionsSheduleDataConnection(_ConnectionString);
            List<VacantionSheduleModel> doctors = conn.SelectCommand();
            return new JsonResult(doctors);
        }

        [HttpPost]
        public JsonResult Post([FromBody] VacantionSheduleModel newDoc)
        {
            VacantionsSheduleDataConnection conn = new VacantionsSheduleDataConnection(_ConnectionString);
            string result = conn.InsertCommand(newDoc);
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete([FromBody] VacantionSheduleModel curDoc)
        {
            VacantionsSheduleDataConnection conn = new VacantionsSheduleDataConnection(_ConnectionString);
            string result = conn.DeleteCommand(curDoc);
            return new JsonResult(result);
        }


        [HttpPut]
        public JsonResult Put([FromBody] VacantionSheduleModel curDoc)
        {
            VacantionsSheduleDataConnection conn = new VacantionsSheduleDataConnection(_ConnectionString);
            string result = conn.UpdateCommand(curDoc);
            return new JsonResult(result);
        }
    }
}
