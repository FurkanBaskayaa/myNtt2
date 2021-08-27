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
    public class BelongsToController : Controller
    {
        private readonly IConfiguration _configuration;
        public BelongsToController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"select product_id
                             from demo.belongs_to
                             where catagory_id=@catagory_id";

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
            return new JsonResult(table);
        }
    }
}
