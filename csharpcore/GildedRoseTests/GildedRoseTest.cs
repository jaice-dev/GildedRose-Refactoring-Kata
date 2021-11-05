using Xunit;
using System.Collections.Generic;
using ApprovalUtilities.Utilities;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
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

        [Fact]
        public void Foo()
        {
            IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            app.UpdateQuality();
            Assert.Equal("foo", items[0].Name);
        }
        [Fact]
        public void CheckQualityDegradesBy1InIfInDate()
        {
            IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 10, Quality = 10 } };
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            app.UpdateQuality();
            Assert.Equal(9, items[0].Quality);
        }
        [Fact]
        public void CheckQualityDegradesTwiceAsFastAfterSellDate()
        {
            IList<Item> items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 10 } };
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            app.UpdateQuality();
            Assert.Equal(8, items[0].Quality);
        }
        
        [Fact]
        public void QualityCannotBeNegative()
        {
            IList<Item> items = GenerateItems();
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            for (var i = 0; i < 100; i++)
            {
                app.UpdateQuality();
            }
            Assert.All(items, item => Assert.True(!(item.Quality < 0)));
        }
        
        [Fact]
        public void AgedBrieIncreasesInQualityWithTime()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            for (var i = 0; i < 100; i++)
            {
                app.UpdateQuality();
            }
            Assert.True(items[0].Quality > 0);
        }
        
        [Fact]
        public void QualityIsNeverMoreThan50UnlessRagnaros()
        {
            IList<Item> items = GenerateItems();
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            for (var i = 0; i < 100; i++)
            {
                app.UpdateQuality();
            }
            Assert.All(items, item => Assert.True(
                item.Quality <= 50 | (item.Name == "Sulfuras, Hand of Ragnaros" && item.Quality == 80)
                ));
        }
        
        [Fact]
        public void SulfurasAlwaysHasQualityOf80()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 } };
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            for (var i = 0; i < 100; i++)
            {
                app.UpdateQuality();
            }
            Assert.All(items, item => Assert.True(item.Quality == 80));
        }
        
        [Fact]
        public void ConjuredItemsDegradeInQualityTwiceAsFast()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            
            app.UpdateQuality();

            Assert.Equal(4 , items[0].Quality);
        }
        
        [Fact]
        public void OutOfDateConjuredItemsDegradeInQuality4TimesAsFast()
        {
            IList<Item> items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 10 } };
            GildedRose.StockManager app = new GildedRose.StockManager(items);
            
            app.UpdateQuality();

            Assert.Equal(6 , items[0].Quality);
        }
    }
}
