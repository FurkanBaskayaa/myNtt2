using System;
namespace project1demo.Models
{
    public class ProductTranslations
    {
        public int product_id { get; set; }
        public string language { get; set; }
        public string product_name { get; set; }
        public int price { get; set; }
        public string currency { get; set; }
        public string summary { get; set; }
    }
}
