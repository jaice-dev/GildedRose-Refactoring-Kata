using GildedRoseKata;

namespace GildedRose
{
    public class Normal : Item
    {
        public void Update()
        {
            SellIn -= 1;
            Quality -= 1;
            if (SellIn < 0) Quality -= 1;
            LimitMinimumQuality();
        }

        public void LimitMinimumQuality()
        {
            if (Quality < 0) Quality = 0;
        }

        public string Log()
        {
            return Name + ", " + SellIn + ", " + Quality;
        }
    }

    public class AgedBrie : Normal
    {
        public new void Update()
        {
            SellIn -= 1;
            Quality += 1;
            if (SellIn < 0) Quality += 1;
            LimitMinimumQuality();
        }
    }

    public class Backstage : Normal
    {
        public new void Update()
        {
            SellIn -= 1;
            if (SellIn < 0)
            {
                Quality = 0;
                return;
            }

            Quality += 1;
            if (SellIn < 10) Quality += 1;
            if (SellIn < 5) Quality += 1;
            LimitMinimumQuality();
        }
    }

    public class Sulfuras : Normal
    {
        public new void Update()
        {
        }
    }
}