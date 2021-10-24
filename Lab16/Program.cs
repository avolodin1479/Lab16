using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Lab16
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = new Product[5];
            {
                for (int i = 0; i < 5; i++)
                {
                    products[i] = new Product(0, null, 0);
                    Console.WriteLine("Введите код продукта");
                    products[i].CodProduct = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите наименование продукта");
                    products[i].NameProduct = Console.ReadLine();
                    Console.WriteLine("Введите цену продукта");
                    products[i].PriceProduct = Convert.ToDouble(Console.ReadLine());
                }
            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };


            string json1 = JsonSerializer.Serialize(products, options);
            Console.WriteLine(json1);

            string path = "Products.json";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(json1);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    string json2 = sr.ReadToEnd();
                    Product products2 = JsonSerializer.Deserialize<Product>(json2);

                }
                catch { }
            }
            
            double max = 0;
            string nameMax= null;
            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (products[i].PriceProduct > products[j].PriceProduct)
                    {
                        double temp = products[i].PriceProduct; 
                        string temp2 = products[i].NameProduct;

                        products[i].PriceProduct = products[j].PriceProduct;
                        products[i].NameProduct = products[j].NameProduct;

                        products[j].PriceProduct = temp;
                        products[j].NameProduct = temp2;

                        max = products[j].PriceProduct;
                        nameMax= products[j].NameProduct;
                    }
                }
            }
            Console.WriteLine("Самый дорогой товар {0} по цене {1}", nameMax, max);
            

            Console.ReadKey();
        }
    }
    class Product
    {
        public int CodProduct { get; set; }
        public string NameProduct { get; set; }
        public double PriceProduct { get; set; }
        public Product(int codProduct, string nameProduct, double priceProduct)
        { CodProduct = codProduct; NameProduct = nameProduct; PriceProduct = priceProduct; }
    }

    
}



