using Century2.Api.Century;
using System;
using System.ComponentModel;
using Xunit;

namespace Century2.Tests.Century
{
    public class CaravanTest
    {
        [Fact]
        public void CaravanNotation_YYGBRR_DisplayCaravan()
        {
            Caravan caravan = new Caravan(2, 1, 1, 2);

            Assert.Equal("YYGBRR", caravan.CaravanNotation());
        }

        [Fact]
        public void CaravanNotation_GRR_DisplayCaravan()
        {
            Caravan caravan = new Caravan(0, 1, 0, 2);

            Assert.Equal("GRR", caravan.CaravanNotation());
        }

        [Fact]
        public void CaravanNotation_YGBBR_AssemblesCorrectString()
        {
            Caravan parseCaravan = new Caravan( 1, 1, 2, 1 );
            
            string parseString = parseCaravan.CaravanNotation();

            Assert.Equal("YGBBR", parseString);
        }

        [Fact]
        public void CaravanNotation_YYBRRR_AssemblesCorrectString()
        {
            Caravan parseCaravan = new Caravan( 2, 0, 1, 3 );
            
            string parseString = parseCaravan.CaravanNotation();

            Assert.Equal("YYBRRR", parseString);
        }

        [Fact]
        public void TransmuteGem_Y2G_TransmuteCorrectly()
        {
            Caravan beginCaravan = new Caravan( 1, 0, 0, 0 );
            Caravan endCaravan = new Caravan( 0, 1, 0, 0 );

            beginCaravan.TransmuteGem(Gem.Yellow);
            Assert.Equal(beginCaravan, endCaravan);
        }

        [Fact]
        public void TransmuteGem_G2B2R_TransmuteCorrectly()
        {
            Caravan beginCaravan = new Caravan( 0, 1, 0, 0 );
            Caravan endCaravan = new Caravan( 0, 0, 0, 1 );

            beginCaravan.TransmuteGem(Gem.Green);
            beginCaravan.TransmuteGem(Gem.Blue);

            Assert.Equal(beginCaravan, endCaravan);
        }

        [Fact]
        public void TransmuteGem_YY2GG_TransmuteCorrectly()
        {
            Caravan beginCaravan = new Caravan( 2, 0, 0, 0 );
            Caravan endCaravan = new Caravan( 0, 2, 0, 0 );

            beginCaravan.TransmuteGem(Gem.Yellow);
            beginCaravan.TransmuteGem(Gem.Yellow);

            Assert.Equal(beginCaravan, endCaravan);
        }

        [Fact]
        public void TransmuteGem_NoB_CantTransmuteNothing()
        {
            Caravan beginCaravan = new Caravan( 1, 0, 0, 0 );
            Caravan endCaravan = new Caravan( 1, 0, 0, 0 );

            beginCaravan.TransmuteGem(Gem.Blue);

            Assert.Equal(beginCaravan, endCaravan);
        }

        [Fact]
        public void DiscardGem_Y_DiscardCorrectly()
        {
            Caravan beginCaravan = new Caravan( 3, 0, 0, 8 );
            Caravan endCaravan = new Caravan( 2, 0, 0, 8 );

            beginCaravan.DiscardGem(Gem.Yellow);

            Assert.Equal(beginCaravan, endCaravan);
        }

        [Fact]
        public void DiscardGem_BBBBBBBG_DiscardCorrectly()
        {
            Caravan beginCaravan = new Caravan( 2, 1, 9, 6 );
            Caravan endCaravan = new Caravan( 2, 0, 2, 6 );

            beginCaravan.DiscardGem(Gem.Blue);
            beginCaravan.DiscardGem(Gem.Blue);
            beginCaravan.DiscardGem(Gem.Blue);
            beginCaravan.DiscardGem(Gem.Blue);
            beginCaravan.DiscardGem(Gem.Blue);
            beginCaravan.DiscardGem(Gem.Blue);
            beginCaravan.DiscardGem(Gem.Blue);
            beginCaravan.DiscardGem(Gem.Green);

            Assert.Equal(beginCaravan, endCaravan);
        }

        [Fact]
        public void DiscardGem_NoY_CantDiscardEmpty()
        {
            Caravan beginCaravan = new Caravan(0, 1, 0, 0);
            Caravan endCaravan = new Caravan(0, 1, 0, 0);

            beginCaravan.DiscardGem(Gem.Blue);

            Assert.Equal(beginCaravan, endCaravan);
        }

        [Fact]
        public void AddCaravan_YYBBplusGGRR_AddCorrectly()
        {
            Caravan firstCaravan = new Caravan(2, 0, 2, 0);
            Caravan addCaravan = new Caravan(0, 2, 0, 2);

            firstCaravan.AddCaravan(addCaravan);

            Assert.Equal(new Caravan(2, 2, 2, 2), firstCaravan);
        }

        [Fact]
        public void AddCaravan_YYBBRRRplusYYYY_AddCorrectly()
        {
            Caravan firstCaravan = new Caravan(2, 0, 2, 3);
            Caravan addCaravan = new Caravan(4, 0, 0, 0);

            firstCaravan.AddCaravan(addCaravan);

            Assert.Equal(new Caravan(6, 0, 2, 3), firstCaravan);
        }

        [Fact]
        public void HasGem_YYYRR_HasY()
        {
            Caravan caravan = new Caravan(3, 0, 0, 2);

            Assert.True(caravan.HasGem(Gem.Yellow));
        }

        [Fact]
        public void HasGem_YYYRR_HasNotG()
        {
            Caravan caravan = new Caravan(3, 0, 0, 2);

            Assert.False(caravan.HasGem(Gem.Green));
        }

        [Fact]
        public void HasGem_0_HasNotB()
        {
            Caravan caravan = new Caravan(0, 0, 0, 0);

            Assert.False(caravan.HasGem(Gem.Blue));
        }

        [Fact]
        public void HasGem_YYYGGGBBB_HasNotR()
        {
            Caravan caravan = new Caravan(3, 3, 3, 0);

            Assert.False(caravan.HasGem(Gem.Red));
        }

        [Theory]
        [InlineData("y")]
        [InlineData("Y")]
        public void GemInput_Y_ReturnYellow(string input)
        {
            Assert.Equal(Gem.Yellow, Caravan.GemInput(input));
        }

        [Theory]
        [InlineData("g")]
        [InlineData("G")]
        public void GemInput_G_ReturnGreen(string input)
        {
            Assert.Equal(Gem.Green, Caravan.GemInput(input));
        }

        [Theory]
        [InlineData("b")]
        [InlineData("B")]
        public void GemInput_B_ReturnBlue(string input)
        {
            Assert.Equal(Gem.Blue, Caravan.GemInput(input));
        }

        [Theory]
        [InlineData("r")]
        [InlineData("R")]
        public void GemInput_r_ReturnRed(string input)
        {
            Assert.Equal(Gem.Red, Caravan.GemInput(input));
        }

        [Fact]
        public void GemInput_q_ThrowsError()
        {
            Exception ex = Assert.Throws<InvalidEnumArgumentException>(() =>
                Caravan.GemInput("q"));

            Assert.Equal("Invalid input: 'q' for GemInput()", ex.Message);
        }

        [Fact]
        public void HasInventory_YY_HasYY()
        {
            Caravan caravan = new Caravan(2, 0, 0, 0);

            Assert.True(caravan.HasInventory(new Caravan(2, 0, 0, 0)));
        }

        [Fact]
        public void HasInventory_YYGGR_HasGR()
        {
            Caravan caravan = new Caravan(2, 2, 0, 1);

            Assert.True(caravan.HasInventory(new Caravan(0, 1, 0, 1)));
        }

        [Fact]
        public void HasInventory_YYGGBBRR_HasYY()
        {
            Caravan caravan = new Caravan(2, 2, 2, 2);

            Assert.True(caravan.HasInventory(new Caravan(2, 0, 0, 0)));
        }

        [Fact]
        public void HasInventory_GGBBRR_HasNoY()
        {
            Caravan caravan = new Caravan(0, 2, 2, 2);

            Assert.False(caravan.HasInventory(new Caravan(1, 0, 0, 0)));
        }

        
        [Fact]
        public void HasInventory_YBR_HasNoGR()
        {
            Caravan caravan = new Caravan(1, 0, 1, 1);

            Assert.False(caravan.HasInventory(new Caravan(0, 1, 0, 1)));
        }

        [Fact]
        public void TotalGems_YYGBRR_Return6()
        {
            Assert.Equal(6, new Caravan(2, 1, 1, 2).TotalGems());
        }

        [Fact]
        public void TotalGems_BBBBBRR_Return7()
        {
            Assert.Equal(7, new Caravan(0, 0, 5, 2).TotalGems());
        }

        [Fact]
        public void TotalGems_Return0()
        {
            Assert.Equal(0, new Caravan(0, 0, 0, 0).TotalGems());
        }
    }
}
