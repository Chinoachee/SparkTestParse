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
            HtmlNode articleNode = document.DocumentNode.SelectSingleNode("//span[@class='article__value']");
            HtmlNode countNode = document.DocumentNode.SelectSingleNode("//span[@id='bx_117848907_131465_quant_up']");
            string nameItem = titleNode.InnerText != null ? titleNode.InnerText : "None";
            string priceItem = priceNode.InnerText;
            string articleItem = articleNode.InnerText;
            string countItem = countNode.GetAttributeValue("data-max", ""); 
            Item item = new Item(nameItem, Convert.ToDecimal(priceItem),articleItem,countItem);
            using(StreamWriter sw = new StreamWriter("ЛукВкусный.csv", false, Encoding.UTF8))
            {
                sw.WriteLine("Название товара;Цена(рубли);Артикль;Количество");
                sw.WriteLine(item);
            }
        }
    }
}
