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

        [HttpGet("{language}")]
        public JsonResult Get(string language)
        {
            string query = @"select *
                             from demo.catagory_translations NATURAL JOIN demo.Catagory
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
        public JsonResult Post(int catagory_id, CatagoryTranslations ct)
        {
            bool isCatagoryExist;
            bool isCatagoryTranslationExist;
            int addedCatagoryId;

            string query = @"insert into Catagory values()";

            string query2 = @"select catagory_id from demo.catagory where catagory_id = @catagory_id";

            string query3 = @"select catagory_id language from demo.catagory_translations where catagory_id = @catagory_id and language=@language";

            string query4 = @"insert into demo.catagory_translations values(@catagory_id, @catagory_name,@language)";

            string query5 = @"SELECT catagory_id FROM demo.Catagory ORDER BY catagory_id DESC LIMIT 1";

            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            DataTable table3 = new DataTable();
            DataTable table4 = new DataTable();
            DataTable table5 = new DataTable();
            DataTable table6 = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;
            MySqlDataReader myReader2;
            MySqlDataReader myReader3;
            MySqlDataReader myReader4;
            MySqlDataReader myReader5;
            MySqlDataReader myReader6;

            //Check if this catagory exists
            using (MySqlConnection mycon2 = new MySqlConnection(sqlDataSource))
            {
                mycon2.Open();
                using (MySqlCommand myCommand2 = new MySqlCommand(query2, mycon2))
                {
                    myCommand2.Parameters.AddWithValue("@catagory_id", catagory_id);
                    myReader2 = myCommand2.ExecuteReader();
                    table2.Load(myReader2);
                    if (table2.Rows.Count == 1)
                    {
                        isCatagoryExist = true;
                    }
                    else
                    {
                        isCatagoryExist = false;
                    }
                    myReader2.Close();
                    mycon2.Close();
                }
            }
            if (isCatagoryExist)
            {
                //Check if catagory translation exists
                using (MySqlConnection mycon3 = new MySqlConnection(sqlDataSource))
                {
                    mycon3.Open();
                    using (MySqlCommand myCommand3 = new MySqlCommand(query3, mycon3))
                    {
                        myCommand3.Parameters.AddWithValue("@catagory_id", ct.catagory_id);
                        myCommand3.Parameters.AddWithValue("@language", ct.language);
                        myReader3 = myCommand3.ExecuteReader();
                        table3.Load(myReader3);
                        if (table3.Rows.Count == 1)
                        {
                            isCatagoryTranslationExist = true;
                        }
                        else
                        {
                            isCatagoryTranslationExist = false;
                        }
                        myReader3.Close();
                        mycon3.Close();
                    }
                    if (isCatagoryTranslationExist)
                    {
                        return new JsonResult("This Catagory translation already exists");
                    }
                    else
                    {
                        //Add new translation to existing product
                        using (MySqlConnection mycon4 = new MySqlConnection(sqlDataSource))
                        {
                            mycon4.Open();
                            using (MySqlCommand myCommand4 = new MySqlCommand(query4, mycon4))
                            {
                                myCommand4.Parameters.AddWithValue("@catagory_id", ct.catagory_id);
                                myCommand4.Parameters.AddWithValue("@catagory_name", ct.catagory_name);
                                myCommand4.Parameters.AddWithValue("@language", ct.language);

                                myReader4 = myCommand4.ExecuteReader();
                                table4.Load(myReader4);

                                myReader4.Close();
                                mycon4.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                //Add new catagory to catagory table
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
                //Get catagory_id of last added catagory
                using (MySqlConnection mycon5 = new MySqlConnection(sqlDataSource))
                {
                    mycon5.Open();
                    using (MySqlCommand myCommand5 = new MySqlCommand(query5, mycon5))
                    {
                        myReader5 = myCommand5.ExecuteReader();
                        table5.Load(myReader5);

                        DataRow row = table5.Rows[0];
                        addedCatagoryId = row.Field<int>("catagory_id");

                        myReader5.Close();
                        mycon5.Close();
                    }
                }
                //Add new translation to newly created product
                using (MySqlConnection mycon6 = new MySqlConnection(sqlDataSource))
                {
                    mycon6.Open();
                    using (MySqlCommand myCommand6 = new MySqlCommand(query4, mycon6))
                    {
                        myCommand6.Parameters.AddWithValue("@catagory_id", addedCatagoryId);
                        myCommand6.Parameters.AddWithValue("@catagory_name", ct.catagory_name);
                        myCommand6.Parameters.AddWithValue("@language", ct.language);
                        myReader6 = myCommand6.ExecuteReader();
                        table6.Load(myReader6);

                        myReader6.Close();
                        mycon6.Close();
                    }
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
