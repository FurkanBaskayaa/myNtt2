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
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{language}")]
        public JsonResult Get([FromQuery] int[] idArr, string language)
        {
            string query = @"select *
                             from demo.product_translations NATURAL JOIN demo.products
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
                    if (idArr.Length > 0)
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
        public JsonResult Post(Products product, string language, string product_name, int price, string currency, string summary, int catagory_id)
        {
            int addedProductId;
            bool isProductExist;
            bool isProductTranslationExist;

            string query = @"insert into Products(rating, photo_url)
                             values(@rating, @photo_url);";

            string query2 = @"SELECT product_id FROM demo.Products ORDER BY product_id DESC LIMIT 1";

            string query3 = @"insert into demo.product_translations values(@product_id, @language, @product_name, @price, @currency, @summary)";

            string query4 = @"select product_id from demo.products where product_id = @product_id";

            string query5 = @"select product_id language from demo.product_translations where product_id = @product_id and language=@language";

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

            //Check if this product exists
            using (MySqlConnection mycon4 = new MySqlConnection(sqlDataSource))
            {
                mycon4.Open();
                using (MySqlCommand myCommand4 = new MySqlCommand(query4, mycon4))
                {
                    myCommand4.Parameters.AddWithValue("@product_id", product.product_id);
                    myReader4 = myCommand4.ExecuteReader();
                    table4.Load(myReader4);
                    if (table4.Rows.Count == 1)
                    {
                        isProductExist = true;
                    }
                    else
                    {
                        isProductExist = false;
                    }
                    myReader4.Close();
                    mycon4.Close();
                }
            }
            if (isProductExist)
            {
                //Check if product translation exists
                using (MySqlConnection mycon6 = new MySqlConnection(sqlDataSource))
                {
                    mycon6.Open();
                    using (MySqlCommand myCommand6 = new MySqlCommand(query5, mycon6))
                    {
                        myCommand6.Parameters.AddWithValue("@product_id", product.product_id);
                        myCommand6.Parameters.AddWithValue("@language", language);
                        myReader6 = myCommand6.ExecuteReader();
                        table6.Load(myReader6);
                        if (table6.Rows.Count == 1)
                        {
                            isProductTranslationExist = true;
                        }
                        else
                        {
                            isProductTranslationExist = false;
                        }
                        myReader6.Close();
                        mycon6.Close();
                    }
                    if (isProductTranslationExist)
                    {
                        return new JsonResult("This product translation already exists");
                    }
                    else
                    {
                        //Add new translation to existing product
                        using (MySqlConnection mycon3 = new MySqlConnection(sqlDataSource))
                        {
                            mycon3.Open();
                            using (MySqlCommand myCommand3 = new MySqlCommand(query3, mycon3))
                            {
                                myCommand3.Parameters.AddWithValue("@product_id", product.product_id);
                                myCommand3.Parameters.AddWithValue("@product_name", product_name);
                                myCommand3.Parameters.AddWithValue("@language", language);
                                myCommand3.Parameters.AddWithValue("@price", price);
                                myCommand3.Parameters.AddWithValue("@currency", currency);
                                myCommand3.Parameters.AddWithValue("@summary", summary);
                                myReader3 = myCommand3.ExecuteReader();
                                table3.Load(myReader3);

                                myReader3.Close();
                                mycon3.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                //Add new product to products table
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@rating", product.rating);
                        myCommand.Parameters.AddWithValue("@photo_url", product.photo_url);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }
                //Get product_id of last added product
                using (MySqlConnection mycon2 = new MySqlConnection(sqlDataSource))
                {
                    mycon2.Open();
                    using (MySqlCommand myCommand2 = new MySqlCommand(query2, mycon2))
                    {
                        myReader2 = myCommand2.ExecuteReader();
                        table2.Load(myReader2);

                        DataRow row = table2.Rows[0];
                        addedProductId = row.Field<int>("product_id");

                        myReader2.Close();
                        mycon2.Close();
                    }
                }
                //Add new translation to newly created product
                using (MySqlConnection mycon5 = new MySqlConnection(sqlDataSource))
                {
                    mycon5.Open();
                    using (MySqlCommand myCommand5 = new MySqlCommand(query3, mycon5))
                    {
                        myCommand5.Parameters.AddWithValue("@product_id", addedProductId);
                        myCommand5.Parameters.AddWithValue("@product_name", product_name);
                        myCommand5.Parameters.AddWithValue("@language", language);
                        myCommand5.Parameters.AddWithValue("@price", price);
                        myCommand5.Parameters.AddWithValue("@currency", currency);
                        myCommand5.Parameters.AddWithValue("@summary", summary);
                        myReader5 = myCommand5.ExecuteReader();
                        table5.Load(myReader5);

                        myReader5.Close();
                        mycon5.Close();
                    }
                }
                //Add new product to belongsto relation
                var ctrl = new BelongsToController(_configuration);
                ctrl.ControllerContext = ControllerContext;
                BelongsTo bt = new BelongsTo();
                bt.product_id = addedProductId;
                bt.catagory_id = catagory_id;

                ctrl.Post(bt);
            }
            return new JsonResult("New product Added");
        }

        [HttpPut]
        public JsonResult Put(int product_id, int rating, string photo_url, ProductTranslations productTranslations)
        {
            string query = @"update Products set
                                rating = @rating,
                                photo_url = @photo_url
                             where product_id = @product_id";
            string query2 = @"update demo.product_translations set
                                product_name = @product_name,
                                price = @price,
                                currency = @currency,
                                summary = @summary
                             where product_id = @product_id and language = @language";

            DataTable table = new DataTable();
            DataTable table2 = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("DemoAppCon");

            MySqlDataReader myReader;
            MySqlDataReader myReader2;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@rating", rating);
                    myCommand.Parameters.AddWithValue("@photo_url", photo_url);
                    myCommand.Parameters.AddWithValue("@product_id", product_id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }
            using (MySqlConnection mycon2 = new MySqlConnection(sqlDataSource))
            {
                mycon2.Open();
                using (MySqlCommand myCommand2 = new MySqlCommand(query2, mycon2))
                {
                    myCommand2.Parameters.AddWithValue("@product_id", productTranslations.product_id);
                    myCommand2.Parameters.AddWithValue("@product_name", productTranslations.product_name);
                    myCommand2.Parameters.AddWithValue("@language", productTranslations.language);
                    myCommand2.Parameters.AddWithValue("@price", productTranslations.price);
                    myCommand2.Parameters.AddWithValue("@currency", productTranslations.currency);
                    myCommand2.Parameters.AddWithValue("@summary", productTranslations.summary);
                    myReader2 = myCommand2.ExecuteReader();
                    table2.Load(myReader2);

                    myReader2.Close();
                    mycon2.Close();
                }
            }
            return new JsonResult("product Updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from Products
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
            return new JsonResult("product Deleted");
        }
    }
}
