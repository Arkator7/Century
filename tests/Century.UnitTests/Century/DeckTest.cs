using Xunit;

namespace Century.UnitTests.Century
{
    public class DeckTest
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

        [Fact]
        public void SetLibrary_SetupSuccessfully()
        {
            Normal[] normalDeck = NormalDeck.SetLibrary();

            Assert.Equal(new Normal('A', NormalDeck.Suit.Spade), normalDeck[0]);
            Assert.Equal(new Normal('3', NormalDeck.Suit.Spade), normalDeck[2]);
            Assert.Equal(new Normal('A', NormalDeck.Suit.Heart), normalDeck[13]);
            Assert.Equal(new Normal('8', NormalDeck.Suit.Club ), normalDeck[33]);
        }
        
        public static class NormalDeck 
        {
            private readonly static int[,] list = { };

            static public Normal[] SetLibrary()
            {
                Normal[] deck = new Normal[52];
                char[] rank = {'A','2','3','4','5','6','7','8','9','T',
                                 'J','Q','K'};
                Suit[] suits = {Suit.Spade, Suit.Heart, Suit.Club,
                                Suit.Diamond};

                for (int i = 0; i < 13; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        deck[i+j*13] = new Normal(rank[i], suits[j]);
                    }
                }

                return deck;
            }

            public enum Suit {
                Spade = 1,
                Club = 2,
                Diamond = 3,
                Heart = 4
            }
        }

        public class Normal
        {
            public char rank;
            public NormalDeck.Suit suit;

            public Normal(char rank, NormalDeck.Suit suit)
            {
                this.rank = rank;
                this.suit = suit;
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (obj == this) return true;
                return obj is Normal card && Equals(card);
            }

            protected bool Equals(Normal card)
            {
                return this.rank.Equals(card.rank)
                    && this.suit.Equals(card.suit);
            }
        }

    }
}
