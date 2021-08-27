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
    public class ProductTranslationsController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductTranslationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{language}")]
        public JsonResult Get([FromQuery] int[] idArr, string language)
        {
            string query = @"select *
                             from demo.product_translations
                             where language=@language and product_id = @product_id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@language", language);
                    if(idArr.Length>0)
                    {
                        myCommand.Parameters.AddWithValue("@product_id", idArr[0]);
                        foreach (int id in idArr)
                        {
                            myCommand.Parameters["@product_id"].Value = id;
                            myReader = myCommand.ExecuteReader();
                            table.Load(myReader);
                        }
                    }
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(ProductTranslations productTranslations)
        {
            string query = @"insert into demo.product_translations values(@product_id, @language, @product_name, @price, @currency, @summary)";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@product_id", productTranslations.product_id);
                    myCommand.Parameters.AddWithValue("@product_name", productTranslations.product_name);
                    myCommand.Parameters.AddWithValue("@language", productTranslations.language);
                    myCommand.Parameters.AddWithValue("@price", productTranslations.price);
                    myCommand.Parameters.AddWithValue("@currency", productTranslations.currency);
                    myCommand.Parameters.AddWithValue("@summary", productTranslations.summary);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("New product translation Added");
        }

        [HttpPut]
        public JsonResult Put(ProductTranslations productTranslations)
        {
            string query = @"update demo.product_translations set
                                product_id = @product_id,
                                product_name = @product_name,
                                language = @language,
                                price = @price,
                                currency = @currency,
                                summary = @summary
                             where product_id = @product_id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@product_id", productTranslations.product_id);
                    myCommand.Parameters.AddWithValue("@product_name", productTranslations.product_name);
                    myCommand.Parameters.AddWithValue("@language", productTranslations.language);
                    myCommand.Parameters.AddWithValue("@price", productTranslations.price);
                    myCommand.Parameters.AddWithValue("@currency", productTranslations.currency);
                    myCommand.Parameters.AddWithValue("@summary", productTranslations.summary);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("product translation Updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from demo.product_translations
                             where product_id = @product_id";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@product_id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("product translation Deleted");
        }
    }
}
