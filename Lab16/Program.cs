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
            string[,] products = new string[5, 3];
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 0)
                        {
                            Console.WriteLine("Введите код товара");
                        }
                        if (j == 1)
                        {
                            Console.WriteLine("Введите наименование товара");
                        }
                        if (j == 2)
                        {
                            Console.WriteLine("Введите цену товара");
                        }
                        products[i, j] = Console.ReadLine();
                    }
                }

            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0,10} ", products[i, j]);
                }
                Console.WriteLine();
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            Products product1 = new Products { CodProduct = Convert.ToInt32(products[0, 0]), NameProduct = products[0, 1], PriceProduct = Convert.ToDouble(products[0, 2]) };
            Products product2 = new Products { CodProduct = Convert.ToInt32(products[1, 0]), NameProduct = products[1, 1], PriceProduct = Convert.ToDouble(products[1, 2]) };
            Products product3 = new Products { CodProduct = Convert.ToInt32(products[2, 0]), NameProduct = products[2, 1], PriceProduct = Convert.ToDouble(products[2, 2]) };
            Products product4 = new Products { CodProduct = Convert.ToInt32(products[3, 0]), NameProduct = products[3, 1], PriceProduct = Convert.ToDouble(products[3, 2]) };
            Products product5 = new Products { CodProduct = Convert.ToInt32(products[4, 0]), NameProduct = products[4, 1], PriceProduct = Convert.ToDouble(products[4, 2]) };
            string json1 = JsonSerializer.Serialize<Products>(product1) + JsonSerializer.Serialize<Products>(product2) + JsonSerializer.Serialize<Products>(product3) + JsonSerializer.Serialize<Products>(product4) + JsonSerializer.Serialize<Products>(product5);
            Console.WriteLine(json1);

            string path = "Products.json";
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine(json1);
            }

            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    {
                        string json2 = sr.ReadToEnd();
                        Console.WriteLine(json2);
                        Products pr2 = JsonSerializer.Deserialize<Products>(json2);
                    }
                }
                catch (Exception)
                {

                }
            }


            Console.ReadKey();
        }
    }
    class Products
    {
        public int CodProduct { get; set; }
        public string NameProduct { get; set; }
        public double PriceProduct { get; set; }

    }
}


