using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkParserTest
{
    internal class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Article { get; set; }
        public string Count { get; set; }
        public Item(string name, decimal price, string article,string count)
        {
            Name = name;
            Price = price;
            Article = article;
            Count = count;
        }
        public override string ToString()
        {
            return Name + ";" + Price + ";" + Article + ";" + Count;
        }
    }
}
