using System;
using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public const string DexterityVest = "+5 Dexterity Vest";
        public const string AgedBrie = "Ages Brie";
        public const string ElixirOfTheMongoose = "Elixir of the Mongoose";
        public const string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
        public const string BackstageConcertPass = "Backstage passes to a TAFKAL80ETC concert";
        public const string ConjouredPrefix = "Conjured";
        public const string ConjouredManaCake = ConjouredPrefix + " Mana Cake";

        public IList<Item> Items { get; set; }

        private Program() { }

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = CreateProgram();

            System.Console.ReadKey();
        }

        public static Program CreateProgram()
        {
            return new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = DexterityVest, SellIn = 10, Quality = 20},
                    new Item {Name = AgedBrie, SellIn = 2, Quality = 0},
                    new Item {Name = ElixirOfTheMongoose, SellIn = 5, Quality = 7},
                    new Item {Name = SulfurasHandOfRagnaros, SellIn = 0, Quality = 80},
                    new Item {Name = BackstageConcertPass, SellIn = 15, Quality = 20 },
                    new Item {Name = ConjouredManaCake, SellIn = 3, Quality = 6}
                }
            };
        }

        public void UpdateQuality()
        {
            foreach(var item in Items)
            {
                if (item.Name == SulfurasHandOfRagnaros)
                {
                    // Sulfuras is immutable
                    continue;
                }

                // Decrease Sell In for all other items
                item.SellIn -= 1;

                AdjustItemQuality(item);
            }
        }

        private void AdjustItemQuality(Item item)
        {
            switch (item.Name)
            {
                case AgedBrie:
                    {
                        AdjustAgedBrieQuality(item);
                        break;
                    }
                case BackstageConcertPass:
                    {
                        AdjustBackstagePassQuality(item);
                        break;
                    }
                default:
                    {
                        AdjustNormalItemQuality(item);
                        break;
                    }
            }
        }

        private void AdjustAgedBrieQuality(Item item)
        {
            // Quality can not be above 50
            if (item.Quality < 50)
            {
                // Aged Brie Quality increases the older it gets
                item.Quality++;
            }
        }

        private void AdjustBackstagePassQuality(Item item)
        {
            if (item.SellIn < 0 &&
                item.Quality != 0)
            {
                // Backstage pass is worthless after the concert
                item.Quality = 0;
                return;
            }

            // Quality can not be above 50
            if (item.Quality < 50)
            {
                var newQuality = item.Quality;

                // Backstage pass gets more valuable 
                // as the conert draws near
                if(item.SellIn <= 5)
                {
                    newQuality += 2;
                }
                else if (item.SellIn <= 10)
                {
                    newQuality++;
                }

                newQuality++;

                // Quality can not go above 50
                item.Quality = Math.Min(newQuality, 50);
            }
        }

        private void AdjustNormalItemQuality(Item item)
        {
            // Quality can not be below 0
            if (item.Quality != 0)
            {
                // Conjoured items degrade twice as fast
                var degredationMultiplier = item.Name.StartsWith(ConjouredPrefix) ? 2 : 1;

                // Items degrade by 1, but degrade twice
                // as fast when past Sell In is less than 0
                var degredationValue = (item.SellIn >= 0 ? 1 : 2) * degredationMultiplier;

                // Quality can not go below 0
                item.Quality = Math.Max(item.Quality - degredationValue, 0);
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
