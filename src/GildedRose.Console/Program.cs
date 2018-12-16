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
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
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
