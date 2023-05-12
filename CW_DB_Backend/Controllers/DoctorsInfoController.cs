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
    public class DoctorsInfoController
    {
        private readonly IConfiguration _Configuration;
        private readonly string _ConnectionString;

        public DoctorsInfoController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _ConnectionString = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build().GetConnectionString("MyMedicalDatabase");
        }

        [HttpGet]
        public JsonResult Get()
        {
            DoctorsInfoDataConnection conn = new DoctorsInfoDataConnection(_ConnectionString);
            List<DoctorsInfoModel> doctors = conn.SelectCommand();
            return new JsonResult(doctors);
        }

        [HttpPost]
        public JsonResult Post([FromBody] DoctorsInfoModel newInfo)
        {
            DoctorsInfoDataConnection conn = new DoctorsInfoDataConnection(_ConnectionString);
            string result = conn.InsertCommand(newInfo);
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete([FromBody] DoctorsInfoModel curInfo)
        {
            DoctorsInfoDataConnection conn = new DoctorsInfoDataConnection(_ConnectionString);
            string result = conn.DeleteCommand(curInfo);
            return new JsonResult(result);
        }


        [HttpPut]
        public JsonResult Put([FromBody] DoctorsInfoModel curInfo)
        {
            DoctorsInfoDataConnection conn = new DoctorsInfoDataConnection(_ConnectionString);
            string result = conn.UpdateCommand(curInfo);
            return new JsonResult(result);
        }
    }
}
