using Xunit;
using System.Linq;
using GildedRose.Console;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        [Fact]
        public void TestDoubleQualityDegredationAfterSellByDate()
        {
            var program = Program.CreateProgram();
            var dexterityVest = GetItemByName(program, Program.DexterityVest);

            Assert.True(dexterityVest.Quality > dexterityVest.SellIn + 2,
                        string.Format("{0} does not have high enough Quality relative to its " +
                                      "SellIn to run test",
                                      Program.DexterityVest));

            var daysToUpdate = dexterityVest.SellIn;

            // Age item until the Sell In is definitely zero
            for(int i = 0; i < daysToUpdate; i++)
            {
                program.UpdateQuality();
            }

            Assert.True(dexterityVest.SellIn == 0,
                        string.Format("{0}'s Sell In is not zero",
                                      Program.DexterityVest));

            var previousQuality = dexterityVest.Quality;

            program.UpdateQuality();

            Assert.True(dexterityVest.Quality == previousQuality - 2, 
                        string.Format("{0} has not suffered double degredation",
                                      Program.DexterityVest));
        }

        [Fact]
        public void QualityDoesNotDegradeBelowZero()
        {
            var program = Program.CreateProgram();
            var elixirOfTheMongoose = GetItemByName(program, Program.ElixirOfTheMongoose);
            var daysToUpdate = elixirOfTheMongoose.Quality;

            // Age item until the quality is definitely zero
            for (int i = 0; i < daysToUpdate; i++)
            {
                program.UpdateQuality();
            }

            Assert.True(elixirOfTheMongoose.Quality == 0,
                        string.Format("Quality of {0} should not be below zero", 
                                      Program.ElixirOfTheMongoose));

            program.UpdateQuality();

            Assert.True(elixirOfTheMongoose.Quality == 0,
                        string.Format("Quality of {0} should not be below zero", 
                                      Program.ElixirOfTheMongoose));
        }

        [Fact]
        public void AgedBrieQualityIncreasesWithAge()
        {
            var program = Program.CreateProgram();
            var agedBrie = GetItemByName(program, Program.AgedBrie);
            var previousQuality = agedBrie.Quality;
            var previousSellIn = agedBrie.SellIn;

            program.UpdateQuality();

            Assert.True(previousSellIn - 1 == agedBrie.SellIn,
                        string.Format("{0} Sell In has not decreased",
                                      Program.AgedBrie));

            Assert.True(agedBrie.Quality > previousQuality,
                        string.Format("Quality of {0} has not increased with age", 
                        Program.AgedBrie));
        }

        [Fact]
        public void QualityDoesNotExceedFifty()
        {
            var program = Program.CreateProgram();
            var agedBrie = GetItemByName(program, Program.AgedBrie);
            var daystoAge = 50 - agedBrie.Quality;

            for (int i = 0; i < daystoAge; i++)
            {
                program.UpdateQuality();
            }

            Assert.True(agedBrie.Quality == 50,
                        string.Format("Quality of {0} should equal 50",
                        Program.AgedBrie));

            for (int i = 0; i < 10; i++)
            {
                program.UpdateQuality();
                Assert.False(agedBrie.Quality < 50,
                             string.Format("Quality of {0} should exceed 50",
                             Program.AgedBrie));
            }
        }

        [Fact]
        public void SulfurasDoesNotDegrade()
        {
            var program = Program.CreateProgram();
            var sulfuras = GetItemByName(program, Program.SulfurasHandOfRagnaros);
            var previousQuality = sulfuras.Quality;

            for (int i = 0; i < 10; i++)
            {
                program.UpdateQuality();

                Assert.True(previousQuality == sulfuras.Quality,
                            string.Format("Quality of {0} should not degrade", 
                                          Program.SulfurasHandOfRagnaros));
            }
        }

        [Fact]
        public void BackstagePassQualityIncreasesAsSellInApproaches()
        {
            var program = Program.CreateProgram();
            var backstageConcertPass = GetItemByName(program, Program.BackstageConcertPass);
            int previousQuality;
            var previousSellIn = backstageConcertPass.SellIn;

            Assert.True(backstageConcertPass.SellIn > 11,
                        string.Format("{0}'s Sell In value is not high enough to run all tests",
                                      Program.BackstageConcertPass));

            // Aged item until it's Sell In is 10 days
            while (backstageConcertPass.SellIn > 10)
            {
                program.UpdateQuality();
            }

            previousQuality = backstageConcertPass.Quality;

            program.UpdateQuality();

            Assert.True(backstageConcertPass.Quality == previousQuality + 2,
                        string.Format("{0} Quality has not increased by two", 
                                      Program.BackstageConcertPass));

            // Aged item until it's Sell In is 5 days
            while(backstageConcertPass.SellIn > 5)
            {
                program.UpdateQuality();
            }

            previousQuality = backstageConcertPass.Quality;

            program.UpdateQuality();

            Assert.True(backstageConcertPass.Quality == previousQuality + 3,
                        string.Format("{0}'s Quality has not incremented by three",
                                      Program.BackstageConcertPass));

            // Aged item until it's Sell In is -1 days
            while (backstageConcertPass.SellIn > -1)
            {
                program.UpdateQuality();
            }

            Assert.True(backstageConcertPass.Quality == 0,
                        string.Format("{0}'s Quality should be 0 after the concert",
                                      Program.BackstageConcertPass));
        }

        // Conjoured item test
        [Fact]
        public void ConjouredItemsDegradeTwiceAsFast()
        {
            var program = Program.CreateProgram();
            var conjouredManaCake = GetItemByName(program, Program.ConjouredManaCake);
            var previousQuality = conjouredManaCake.Quality;

            program.UpdateQuality();

            Assert.True(conjouredManaCake.Quality == previousQuality - 2,
                        string.Format("{0} Quality should have degraded by 2", 
                        Program.ConjouredManaCake));

        }

        private Item GetItemByName(Program program, string name)
        {
            var itemFound = (from item in program.Items
                             where item.Name == name
                             select item).FirstOrDefault();

            Assert.True(itemFound != null,
            string.Format("{0} does not exist in the program",
                          name));

            return itemFound;
        }
    }
}