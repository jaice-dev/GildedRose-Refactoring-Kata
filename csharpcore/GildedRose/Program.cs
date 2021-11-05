using System;
using System.Collections.Generic;
using GildedRose;

namespace GildedRoseKata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            var items = GenerateItems();

            var stockManager = new StockManager(items);

            for (var day = 0; day < 31; day++)
            {
                HandleOutput(day, items);
                stockManager.UpdateQuality();
            }
        }

        private static void HandleOutput(int day, IList<Item> items)
        {
            Console.WriteLine("-------- day " + day + " --------");
            Console.WriteLine("name, sellIn, quality");
            for (var item = 0; item < items.Count; item++)
            {
                Console.WriteLine(items[item].Name + ", " + items[item].SellIn + ", " + items[item].Quality);
            }

            Console.WriteLine("");
        }

        private static IList<Item> GenerateItems()
        {
            return new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
                // this conjured item does not work properly yet
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };
        }
    }
}
