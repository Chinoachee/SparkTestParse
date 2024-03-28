using HtmlAgilityPack;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace SparkPars {
    internal class Program {
        public static List<Page> GetCategoryInfo(string catalogUrl = "https://spark63.ru/catalog/") {
            List<Page> pages = new List<Page>();
            WebClient client = new WebClient();
            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(client.DownloadString(catalogUrl));

            HtmlNodeCollection listItems = document.DocumentNode.SelectNodes("//li[contains(@class, 'name')]");

            foreach(HtmlNode node in listItems) {
                string hrefValue = node.SelectSingleNode("./a").Attributes["href"].Value.Substring("/category".Length);
                string spanValue = node.SelectSingleNode("./a/span[@class='font_md']").InnerText;
                pages.Add(new Page(spanValue,catalogUrl + hrefValue));
            }
            return pages;
        }
        public static List<Page> GetCountPageInCategory() {
            List<Page> notFullPages = GetCategoryInfo();
            int countPage = 0;
            foreach(Page page in notFullPages) {
                WebClient client = new WebClient();
                HtmlDocument document = new HtmlDocument();

                document.LoadHtml(client.DownloadString(page.UrlCategory));

                HtmlNodeCollection listCountPages = document.DocumentNode.SelectNodes("//a[@class='dark_link']");

                foreach(HtmlNode node in listCountPages) {
                    string hrefValue = node.InnerText;
                    countPage = Convert.ToInt32(hrefValue);
                }
                page.CountPage = countPage;
            }
            return notFullPages;
        }
        static void Main(string[] args) {
            List<Page> list = GetCountPageInCategory();
            List<Item> items = new List<Item>();
            foreach(Page page in list) {
                for(int i = 1; i <= page.CountPage; i++) {
                    WebClient client = new WebClient();
                    HtmlDocument document = new HtmlDocument();
                    if(i == 1) document.LoadHtml(client.DownloadString(page.UrlCategory));
                    else document.LoadHtml(client.DownloadString(page.UrlCategory + $"?PAGEN_1={i}"));
                    Console.WriteLine(page.UrlCategory);
                    Console.WriteLine(i + "page");
                    HtmlNodeCollection listItems = document.DocumentNode.SelectNodes("//div[contains(@class, 'inner_wrap TYPE_1')]");
                    foreach(HtmlNode node in listItems) {
                        string itemName = node.SelectSingleNode(".//div[@class='item-title']/a/span").InnerText.Replace("&quot;","").Replace(";","/"); //Название

                        HtmlNode articleBlock = node.SelectSingleNode(".//div[@class='article_block']");
                        string dataValue = articleBlock.Attributes["data-value"].Value; //Код товара

                        string stockValue = node.SelectSingleNode(".//span[@class='value font_sxs']").InnerText; //Не нужно наверное

                        HtmlNode priceNode = node.SelectSingleNode(".//span[@class='price_value']"); //Цена
                        string price;
                        if(priceNode != null) {
                            price = node.SelectSingleNode(".//span[@class='price_value']").InnerText;
                            if(price.Contains(".")) price = price.Replace(".",",");
                            if(price.Contains("&nbsp")) price = price.Replace("&nbsp;","");
                        } 
                        else price = "Неизвестно";
                        

                        string category = page.NameCategory; //Категория

                        string hrefItem = "https://spark63.ru" + node.SelectSingleNode(".//a[@class='dark_link js-notice-block__title option-font-bold font_sm']").Attributes["href"].Value; //Ссылка костыльная

                        HtmlNode itemsCountNode = node.SelectSingleNode(".//span[@class='plus dark-color']");
                        string countItems; //количество
                        if(itemsCountNode != null) {
                            countItems = itemsCountNode.Attributes["data-max"].Value;
                        } else {
                            countItems = "0";
                        }
                        string filePath = "Товары.csv";
                        if(!File.Exists(filePath)) {
                            File.WriteAllText(filePath,"Код товара;Категория товара;Название;Цена(₽);Количество;Cсылка на товар\n");
                        }
                        using(StreamWriter writer = new StreamWriter(filePath,append:true)) {
                            writer.WriteLine($"{dataValue};{category};{itemName};{price};{countItems};{hrefItem}");
                        }
                        Console.WriteLine($"Товар: {itemName}, Арт.: {dataValue}, Кол-во:{stockValue}, Price:{price}\nCategory:{category}, Href: {hrefItem}\nCount:{countItems}");
                    }
                }
            }
        }
    }
}

