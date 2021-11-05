using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRose
{
    public class StockManager
    {
        IList<Item> Items;
        public StockManager(IList<Item> items)
        {
            Items = items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                switch (Items[i].Name)
                {
                    case "Backstage passes to a TAFKAL80ETC concert":
                        UpdateBackstagePass(i);
                        break;
                    case "Aged Brie":
                        IncreaseQualityBy1(i);
                        break;
                    default:
                        DecreaseQualityBy1(i);
                        break;
                }
                
                UpdateSellIn(i);

                if (Items[i].SellIn < 0)
                {
                    OutOfDate(i);
                }
            }
        }

        private void UpdateSellIn(int i)
        {
            if (Items[i].Name != "Sulfuras, Hand of Ragnaros") Items[i].SellIn -= 1;
        }

        private void UpdateBackstagePass(int i)
        {
            IncreaseQualityBy1(i);

            if (Items[i].SellIn < 11) IncreaseQualityBy1(i);

            if (Items[i].SellIn < 6) IncreaseQualityBy1(i);
        }

        private void OutOfDate(int i)
        {
            switch (Items[i].Name)
            {
                case "Aged Brie":
                    IncreaseQualityBy1(i);
                    break;
                case "Backstage passes to a TAFKAL80ETC concert":
                    Items[i].Quality = 0;
                    break;
                default:
                    DecreaseQualityBy1(i);
                    break;
            }
        }

        private void IncreaseQualityBy1(int i)
        {
            if (Items[i].Quality < 50) Items[i].Quality += 1;
        }

        private void DecreaseQualityBy1(int i)
        {
            if (Items[i].Quality > 0 && Items[i].Name != "Sulfuras, Hand of Ragnaros") Items[i].Quality -= 1;
        }
    }
}
