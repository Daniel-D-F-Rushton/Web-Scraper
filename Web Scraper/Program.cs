using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Web_Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
           GetPage();
            string s = Console.ReadLine();
        }

        private static async void GetPage()
        {
            var url = "https://www.ebay.co.uk/sch/i.html?_from=R40&_trksid=p2499334.m570.l1313&_nkw=test&_sacat=177132";

            // download the page
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

           

            // make ready for parsing
            var Shop = new HtmlDocument();
            Shop.LoadHtml(html);

            // Get the place where the products are viewed
            var ShopShelf = Shop.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("srp-results srp-list clearfix")).ToList();

            // get a list of the products
            var ListOfProductsOnShelf = ShopShelf[0].Descendants("li")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("s-item s-item__pl-on-bottom s-item--watch-at-corner")).ToList();

            // Look at each individual product
            List<string> Titles = new();
            foreach (var SingleProduct in ListOfProductsOnShelf)
            {

                Titles.Add(SingleProduct.Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("s-item__price")).FirstOrDefault().InnerText);
                    
            }

            foreach (var item in Titles)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }

    }
}
