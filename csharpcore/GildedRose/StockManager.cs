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
                if (!IsSulfuras(item)) AgeItemByOneDay(item);
                if (IsOutOfDate(item)) HandleOutOfDateQuality(item);
            }
        }
        
        private void UpdateQuality(Item item)
        {
            if (IsBackstagePass(item)) IncreaseQualityBackstagePass(item);
            else if (IsSulfuras(item)) ;
            else if (IsAgedBrie(item)) IncreaseQualityBy1(item);
            else if (IsConjured(item)) DecreaseQualityBy(2, item);
            else DecreaseQualityBy(1, item);
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
        
        private void DecreaseQualityBy(int amount, Item item)
        {
            item.Quality = Math.Max(item.Quality - amount, 0);
        }
        

        private void AgeItemByOneDay(Item item)
        {
            item.SellIn -= 1;
        }

        private void HandleOutOfDateQuality(Item item)
        {
            if (IsAgedBrie(item)) IncreaseQualityBy1(item);
            else if (IsSulfuras(item)) ;
            else if (IsBackstagePass(item)) item.Quality = 0;
            else if (IsConjured(item)) DecreaseQualityBy(2, item);
            else DecreaseQualityBy(1, item);
        }

        private static bool IsOutOfDate(Item item)
        {
            return item.SellIn < 0;
        }
        
        public static bool IsConjured(Item item)
        {
            return item.Name.ToLower().Contains("conjured");
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
