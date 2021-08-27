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
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select user_id, username, password, mail_address
                             from demo.Users";

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
        public JsonResult Post(Users user)
        {
            string query = @"insert into Users(username, password, mail_address)
                             values(@user_name, @password, @mail_address)";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@user_name", user.username);
                    myCommand.Parameters.AddWithValue("@password", user.password);
                    myCommand.Parameters.AddWithValue("@mail_address", user.mail_address);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("New User Added");
        }

        [HttpPut]
        public JsonResult Put(Users user)
        {
            string query = @"update Users set
                                username = @username,
                                password = @password,
                                mail_address = @mail_address
                             where user_id = @user_id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@username", user.username);
                    myCommand.Parameters.AddWithValue("@password", user.password);
                    myCommand.Parameters.AddWithValue("@mail_address", user.mail_address);
                    myCommand.Parameters.AddWithValue("@user_id", user.user_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("User Updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from Users
                             where user_id = @user_id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@user_id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("User Deleted");
        }
    }
}
