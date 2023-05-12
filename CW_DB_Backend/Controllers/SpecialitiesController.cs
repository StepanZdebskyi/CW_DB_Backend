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
    public class SpecialitiesController
    {
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;

        public SpecialitiesController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _ConnectionString = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build().GetConnectionString("MyMedicalDatabase");
        }

        [HttpGet]
        public JsonResult Get()
        {
            SpecialitiesDataConnection conn = new SpecialitiesDataConnection(_ConnectionString);
            List<SpecialityModel> spec = conn.SelectCommand();
            return new JsonResult(spec);
        }

        [HttpPost]
        public JsonResult Post([FromBody] SpecialityModel newSpec)
        {
            SpecialitiesDataConnection conn = new SpecialitiesDataConnection(_ConnectionString);
            string result = conn.InsertCommand(newSpec);
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete([FromBody] SpecialityModel curSpec)
        {
            SpecialitiesDataConnection conn = new SpecialitiesDataConnection(_ConnectionString);
            string result = conn.DeleteCommand(curSpec);
            return new JsonResult(result);
        }


        [HttpPut]
        public JsonResult Put([FromBody] SpecialityModel curSpec)
        {
            SpecialitiesDataConnection conn = new SpecialitiesDataConnection(_ConnectionString);
            string result = conn.UpdateCommand(curSpec);
            return new JsonResult(result);
        }
    }
}
