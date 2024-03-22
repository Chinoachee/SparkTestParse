using HtmlAgilityPack;
using System.Text;

namespace SparkParserTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument document = new HtmlDocument();
            document.Load("C:\\Users\\anast\\Downloads\\Luk.html");
            HtmlNode titleNode = document.DocumentNode.SelectSingleNode("//h1[@id='pagetitle']");
            HtmlNode priceNode = document.DocumentNode.SelectSingleNode("//span[@class='price_value']");
            string nameItem = titleNode.InnerText != null ? titleNode.InnerText : "None";
            string priceItem = priceNode.InnerText;
            Console.WriteLine(nameItem);
            Console.WriteLine(priceItem);
            Item item = new Item(nameItem, Convert.ToDecimal(priceItem));
            using(StreamWriter sw = new StreamWriter("ЛукВкусный.csv", false, Encoding.UTF8))
            {
                sw.WriteLine(item   );
            }
        }
    }
}
