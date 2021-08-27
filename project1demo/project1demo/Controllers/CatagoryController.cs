using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using project1demo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project1demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatagoryController : Controller
    {
        private readonly IConfiguration _configuration;
        public CatagoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select catagory_id
                             from demo.Catagory";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post()
        {
            string query = @"insert into Catagory values()";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("New catagory Added");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from Catagory
                             where catagory_id = @catagory_id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@catagory_id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("catagory Deleted");
        }
    }
}
