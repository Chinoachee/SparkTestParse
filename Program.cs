using HtmlAgilityPack;
using System.Text;

namespace SparkParserTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument document = new HtmlDocument();
            document.Load("C:\\Users\\anast\\Downloads\\Torf.html");

            HtmlNode titleNode = document.DocumentNode.SelectSingleNode("//h1[@id='pagetitle']");
            string nameItem = titleNode.InnerText != null ? titleNode.InnerText : "None";

            HtmlNode priceNode = document.DocumentNode.SelectSingleNode("//span[@class='price_value']");
            string priceItem = priceNode.InnerText.Replace(".",",");

            HtmlNode articleNode = document.DocumentNode.SelectSingleNode("//span[@class='article__value']");
            string articleItem = articleNode.InnerText;

            HtmlNode idNode = document.DocumentNode.SelectSingleNode("//div[@class='counter_block md']");
            string idItem = idNode.GetAttributeValue("data-item", "");

            HtmlNode countNode = document.DocumentNode.SelectSingleNode("//span[@id='bx_117848907_" + idItem + "_quant_up']");
            string countItem = countNode.GetAttributeValue("data-max", "");


            Item item = new Item(nameItem, Convert.ToDecimal(priceItem),articleItem,countItem);

            if (!File.Exists("Товары.csv")) File.WriteAllText("Товары.csv","Название товара;Цена(рубли);Артикль;Количество\n");
            using(StreamWriter sw = new StreamWriter("Товары.csv", append: true, Encoding.UTF8))
            {
                sw.WriteLine(item);
            }
        }
    }
}
