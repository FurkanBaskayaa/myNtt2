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
    public class CatagoryTranslationsController : Controller
    {
        private readonly IConfiguration _configuration;
        public CatagoryTranslationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{language}")]
        public JsonResult Get(string language)
        {
            string query = @"select *
                             from demo.catagory_translations
                             where language=@language";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@language", language);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(CatagoryTranslations catagoryTranslations)
        {
            string query = @"insert into demo.catagory_translations values(@catagory_id, @catagory_name, @language)";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@catagory_id", catagoryTranslations.catagory_id);
                    myCommand.Parameters.AddWithValue("@catagory_name", catagoryTranslations.catagory_name);
                    myCommand.Parameters.AddWithValue("@language", catagoryTranslations.language);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("New catagory translation Added");
        }

        [HttpPut]
        public JsonResult Put(CatagoryTranslations catagoryTranslations)
        {
            string query = @"update demo.catagory_translations set
                                catagory_id = @catagory_id,
                                catagory_name = @catagory_name,
                                language = @language
                             where catagory_id = @catagory_id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@catagory_id", catagoryTranslations.catagory_id);
                    myCommand.Parameters.AddWithValue("@catagory_name", catagoryTranslations.catagory_name);
                    myCommand.Parameters.AddWithValue("@language", catagoryTranslations.language);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("catagory translation Updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from demo.catagory_translations
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
            return new JsonResult("catagory translation Deleted");
        }
    }
}
