using System;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRose
{
    public class StockManager
    {
        public readonly IList<Item> Items;
        public StockManager(IList<Item> items)
        {
            Items = items;
        }

        public void UpdateStockForNextDay()
        {
            foreach (var item in Items)
            {
                UpdateQuality(item);
                UpdateSellBy(item);
                if (IsOutOfDate(item)) UpdateQualityFurther(item);
            }
        }

        //Order of methods?

        private void UpdateQuality(Item item)
        {
            if (IsBackstagePass(item)) IncreaseQualityBackstagePass(item);
            else if (IsAgedBrie(item)) IncreaseQualityBy1(item);
            else if (IsConjured(item)) DecreaseQualityBy2();
            else DecreaseQualityBy1(item);
        }

        private void IncreaseQualityBackstagePass(Item item)
        {
            IncreaseQualityBy1(item);
            if (item.SellIn < 11) IncreaseQualityBy1(item);
            if (item.SellIn < 6) IncreaseQualityBy1(item);
        }

        private void IncreaseQualityBy1(Item item)
        {
            if (item.Quality < 50) item.Quality += 1;
        }
        
        private void DecreaseQualityBy1(Item item)
        {
            if (item.Quality > 0 && !IsSulfuras(item)) 
                item.Quality -= 1;
        }
        
        private void DecreaseQualityBy2(Item item)
        {
            if (item.Quality > 2 && !IsSulfuras(item)) 
                item.Quality -= 2;
        }

        private void UpdateSellBy(Item item)
        {
            if (!IsSulfuras(item)) 
                item.SellIn -= 1;
        }

        private void UpdateQualityFurther(Item item)
        {
            if (IsAgedBrie(item)) IncreaseQualityBy1(item);
            else if (IsBackstagePass(item)) item.Quality = 0;
            else DecreaseQualityBy1(item);
        }

        public static bool IsConjured(Item item)
        {
            return item.Name.ToLower().Contains("conjured");
        }
        
        private static bool IsOutOfDate(Item item)
        {
            return item.SellIn < 0;
        }

        private static bool IsBackstagePass(Item item)
        {
            return item.Name.ToLower().Contains("backstage pass");
        }
        
        private static bool IsSulfuras(Item item)
        {
            return item.Name.ToLower().Contains("sulfuras, hand of ragnaros");
        }
        
        private static bool IsAgedBrie(Item item)
        {
            return item.Name.ToLower().Contains("aged brie");
        }
    }
}
