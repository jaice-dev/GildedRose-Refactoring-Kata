using GildedRoseKata;

namespace GildedRose
{
    public class ItemWrapper
    {
        public Item Item;
        public ItemWrapper(Item item)
        {
            Item = item;
        }
        
        public virtual void Update()
        {
            Item.SellIn -= 1;
            Item.Quality -= 1;
            if (Item.SellIn < 0) Item.Quality -= 1;
            LimitMinimumQuality();
        }

        public void LimitMinimumQuality()
        {
            if (Item.Quality < 0) Item.Quality = 0;
        }

        public override string ToString()
        {
            return Item.Name + ", " + Item.SellIn + ", " + Item.Quality;
        }
    }

    public class NormalItem : ItemWrapper
    {
        public NormalItem(Item item) : base(item)
        {
        }
    }

    public class AgedBrie : ItemWrapper
    {       
        public AgedBrie(Item item) : base(item)
        {
        }

        public override void Update()
        {
            Item.SellIn -= 1;
            Item.Quality += 1;
            if (Item.SellIn < 0) Item.Quality += 1;
            LimitMinimumQuality();
        }
        
    }

    public class Backstage : ItemWrapper
    {
        public Backstage(Item item) : base(item)
        {
        }
        
        public override void Update()
        {
            Item.SellIn -= 1;
            if (Item.SellIn < 0)
            {
                Item.Quality = 0;
                return;
            }

            Item.Quality += 1;
            if (Item.SellIn < 10) Item.Quality += 1;
            if (Item.SellIn < 5) Item.Quality += 1;
            LimitMinimumQuality();
        }
        
    }

    public class Sulfuras : ItemWrapper
    {
        public Sulfuras(Item item) : base(item)
        {
        }
        
        public override void Update()
        {
        }
    }
}