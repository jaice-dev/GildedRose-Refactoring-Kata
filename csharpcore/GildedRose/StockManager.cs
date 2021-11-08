using System;
using System.Collections.Generic;
using System.Linq;
using GildedRoseKata;

namespace GildedRose
{
    public class StockManager
    {
        public readonly IList<ItemWrapper> Items;
        public StockManager(IEnumerable<Item> items)
        {
            Items = items.Select<Item, ItemWrapper>(item =>
            {
                if (IsSulfuras(item)) return new Sulfuras(item);
                if (IsAgedBrie(item)) return new AgedBrie(item);
                if (IsBackstagePass(item)) return new Backstage(item);
                return new NormalItem(item);
            }).ToList();
        }

        public void UpdateStockForNextDay()
        {
            foreach (var item in Items)
            {
                item.Update();
            }
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
