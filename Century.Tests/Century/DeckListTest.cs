using Century2.Api.Century;
using Xunit;

namespace Century2.Tests.Century
{
    public class DeckListTest
    {
        [Fact]
        public void SetupBoard_SetupSuccessfully()
        {
            Board board = new Board();
            board.SetupBoard();

            Assert.Equal(5, board.Orders.Length);
            Assert.Equal(6, board.Rates.Length);
            Assert.Equal(6, board.RateTribute.Length);
        }
    }
}
