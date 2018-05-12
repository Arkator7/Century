using Century.Api.Century;
using Xunit;

namespace Century.Tests.Century
{
    public class BoardTests
    {

        string[] names = { "A", "B", "C" };

        [Fact]
        public void SetupBoard_SetupSuccessfully()
        {
            Board board = new Board();
            board.SetupBoard();

            Assert.Equal(5, board.Orders.Length);
            Assert.Equal(6, board.Rates.Length);
            Assert.Equal(6, board.RateTribute.Length);
        }

        [Fact]
        public void LoadRatesLibrary_LoadCorrectly()
        {
            Board board = new Board();
            board.LoadRatesLibrary();

            Rate testRate = board.RatesDeck[8];

            Caravan rout = new Caravan( 0, 1, 0, 0 );
            Assert.Equal(testRate.RateOut, rout);

            Caravan rin = new Caravan( 3, 0, 0, 0 );
            Assert.Equal(testRate.RateIn, rin);

            Assert.Equal(0, testRate.Transmute);
            Assert.False(testRate.Played);
        }

        [Fact]
        public void LoadOrderLibrary_LoadCorrectly()
        {
            Board board = new Board();
            board.LoadOrdersLibrary();

            Caravan fiveGreens = new Caravan( 3, 2, 0, 0 );
            Order testOrder = new Order(7, fiveGreens);

            Assert.Equal(board.OrderDeck[0], testOrder);
        }

        [Fact]
        public void FlipTopOrder_TakesNewOrder()
        {
            Board board = new Board();
            board.LoadOrdersLibrary();

            for (int i = 0; i < board.Orders.Length; i++)
            {
                board.Orders[i] = board.FlipTopOrder();
            }

            Board board2 = new Board();
            board2.LoadOrdersLibrary();

            for (int i = 0; i < board2.Orders.Length; i++)
            {
                board2.Orders[i] = board2.FlipTopOrder();
            }

            board.TakeOrder(board.Orders[1]);

            Assert.Equal(board.Orders[0], board2.Orders[0]);
            Assert.Equal(board.Orders[1], board2.Orders[2]);
            Assert.Equal(board.Orders[2], board2.Orders[3]);
            Assert.Equal(board.Orders[3], board2.Orders[4]);
        }

        [Fact]
        public void FlipTopRate_TakesNewRate()
        {
            Board board = new Board();
            board.LoadRatesLibrary();

            for (int i = 0; i < board.Rates.Length; i++)
            {
                board.Rates[i] = board.FlipTopRate();
            }

            Board board2 = new Board();
            board2.LoadRatesLibrary();

            for (int i = 0; i < board2.Rates.Length; i++)
            {
                board2.Rates[i] = board2.FlipTopRate();
            }

            board.TakeRate(board.Rates[2]);

            Assert.Equal(board.Rates[0], board2.Rates[0]);
            Assert.Equal(board.Rates[1], board2.Rates[1]);
            Assert.Equal(board.Rates[2], board2.Rates[3]);
            Assert.Equal(board.Rates[3], board2.Rates[4]);
            Assert.Equal(board.Rates[4], board2.Rates[5]);
        }

        [Fact]
        public void TributeRate_TakesGemsToRate_TributeSuccessfully()
        {
            Board board = new Board();

            for (int i = 0; i < board.RateTribute.Length; i++)
            {
                board.RateTribute[i] = new Caravan(0, 0, 0, 0);
            }

            board.TributeRate(Gem.Yellow, 0);
            board.TributeRate(Gem.Green, 1);
            board.TributeRate(Gem.Blue, 2);
            board.TributeRate(Gem.Red, 3);

            Assert.Equal(new Caravan(1,0,0,0), board.RateTribute[0]);
            Assert.Equal(new Caravan(0,1,0,0), board.RateTribute[1]);
            Assert.Equal(new Caravan(0,0,1,0), board.RateTribute[2]);
            Assert.Equal(new Caravan(0,0,0,1), board.RateTribute[3]);

            board.TributeRate(Gem.Yellow, 3);
            board.TributeRate(Gem.Green, 3);

            Assert.Equal(new Caravan(1,1,0,1), board.RateTribute[3]);

            board.TributeRate(Gem.Yellow, 3);

            Assert.Equal(new Caravan(2,1,0,1), board.RateTribute[3]);
        }
    }
}
