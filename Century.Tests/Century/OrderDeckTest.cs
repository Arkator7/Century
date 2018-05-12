using Century.Api.Century;
using Xunit;

namespace Century.Tests.Century
{
    public class OrderDeckTest
    {
        [Fact]
        public void FlipTopCard_Flipped_OneLessCard()
        {
            Board board = new Board();
            board.LoadOrdersLibrary();

            int originalSize = board.OrderDeck.Length;

            board.FlipTopOrder();

            int newSize = board.OrderDeck.Length;

            Assert.Equal(originalSize, newSize + 1);
        }

    }
}
