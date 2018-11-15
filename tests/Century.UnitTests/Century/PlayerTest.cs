using Xunit;

namespace Century.UnitTests.Century
{
    public class PlayerTest
    {
        [Fact]
        public void ActionFeedback_ValidT_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("t",
                null, new Rate(new Caravan(3, 0, 0, 0),
                new Caravan(0, 3, 0, 0), 0), 0, true);

            Assert.Equal("James takes the rate: (YYY -> GGG)",
                displayString);
        }

        [Fact]
        public void PlayerNotation_StartingPlayer_DisplayPlayer()
        {
            Player player = new Player("Jack", false, new Caravan(2, 0, 0, 0));

            string displayString = player.PlayerNotation();

            Assert.Equal("Name: Jack\n" + "Gem: YY\n" + "Player Orders: []\n" +
                         "Player Extra Points: []\n" +
                         "Player Rates (total): [0: (() -> YY), 1: ({2}), ]\n" +
                         "Player Rates (can play): [0: (() -> YY), 1: ({2}), ]", displayString);
        }

        [Fact]
        public void PlayerNotation_MidgamePlayer_DisplayPlayer()
        {
            Player player = new Player("Ben", false, new Caravan(3, 2, 3, 1));

            player.AcquiredOrder.Add(new Order(16, new Caravan(0, 0, 0, 4)));
            player.ExtraPoints.Add(1);
            player.AcquiredOrder.Add(new Order(18, new Caravan(0, 1, 0, 4)));
            player.ExtraPoints.Add(0);

            Rate rate = new Rate(new Caravan(0, 0, 0, 0), new Caravan(0, 0, 0, 0), 4);
            rate.SetPlayed(true);
            player.RateHand.Add(rate);

            player.RateHand.Add(new Rate(new Caravan(0, 0, 0, 0), new Caravan(0, 0, 0, 1), 0));
            player.RateHand.Add(new Rate(new Caravan(0, 2, 0, 0), new Caravan(2, 0, 0, 1), 0));

            Rate rate2 = new Rate(new Caravan(0, 0, 0, 2), new Caravan(0, 2, 3, 0), 0);
            rate2.SetPlayed(true);
            player.RateHand.Add(rate2);

            // player.rateHand.Find;

            string displayString = player.PlayerNotation();

            Assert.Equal("Name: Ben\n" + "Gem: YYYGGBBBR\n" +
                         "Player Orders: [0: (16 - {RRRR}), 1: (18 - {GRRRR}), ]\n" +
                         "Player Extra Points: [0: (1), 1: (0), ]\n" +
                         "Player Rates (total): [0: (() -> YY), 1: ({2}), 2: ({4}), 3: (() -> R), 4: (GG -> YYR), 5: (RR -> GGBBB), ]\n" +
                         "Player Rates (can play): [0: (() -> YY), 1: ({2}), 3: (() -> R), 4: (GG -> YYR), ]", displayString);
        }

        [Fact]
        public void ActionFeedback_ValidB_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("b",
                new Order(20, new Caravan(0, 0, 0, 5)), null, 0, true);

            Assert.Equal("James filfills the order: (20 - {RRRRR}) !!!",
                displayString);
        }

        [Fact]
        public void FulfilOrder_180023Order_FulfilCorrectly()
        {
            Caravan playerCaravan = new Caravan(0, 1, 3, 3);
            Player player1 = new Player("Tem", false, playerCaravan);

            Caravan orderCaravan = new Caravan(0, 0, 2, 3);
            Order order = new Order(18, orderCaravan);

            Board board = new Board();
            board.LoadOrdersLibrary();

            player1.FulfilOrder(board, order);

            Caravan endCaravan = new Caravan(0, 1, 1, 0);

            Assert.Equal(playerCaravan, endCaravan);
        }

        [Fact]
        public void FlipNextOrder_180023Order_FlipCorrectly()
        {
            Caravan playerCaravan = new Caravan(3, 3, 0, 0);
            Player player1 = new Player("Tem", false, playerCaravan);

            Board board = new Board();
            board.LoadOrdersLibrary();

            for (int i = 0; i < board.Orders.Length; i++) {
                board.Orders[i] = board.FlipTopOrder();
            }

            Order secondOrder = board.Orders[1];

            player1.FulfilOrder(board, board.Orders[0]);

            Assert.Equal(board.Orders[0], secondOrder);
            Assert.NotEqual(board.Orders[1], secondOrder);
        }

        [Fact]
        public void AcquiredOrder_180023Order_PlayerHasOrder()
        {
            Caravan playerCaravan = new Caravan(0, 1, 3, 3);
            Player player1 = new Player("Tem", false, playerCaravan);

            Caravan orderCaravan = new Caravan(0, 0, 2, 3);
            Order order = new Order(18, orderCaravan);

            Board board = new Board();
            board.LoadOrdersLibrary();

            board.Orders[0] = order;

            player1.FulfilOrder(board, order);

            Assert.Equal(player1.AcquiredOrder[0], order);
        }

        [Fact]
        public void PlayRate_PlayTwoY_GainTwoYellowGems()
        {
            Caravan caravan = new Caravan(0, 1, 3, 3);
            Player player1 = new Player("Tem", false, caravan);

            Caravan zero = new Caravan(0, 0, 0, 0);
            Caravan TwoY = new Caravan(2, 0, 0, 0);

            Rate TwoYellow = new Rate(zero, TwoY, 0);

            player1.PlayRate(TwoYellow, 1);

            Assert.Equal(player1.GemField, new Caravan(2, 1, 3, 3));
        }

        [Fact]
        public void PlayRate_Play3G3B_Exchange9GTo9B()
        {
            Caravan caravan = new Caravan(0, 9, 0, 0);
            Player player1 = new Player("Tem", false, caravan);

            Caravan threeG = new Caravan(0, 3, 0, 0);
            Caravan threeB = new Caravan(0, 0, 3, 0);

            Rate gToB = new Rate(threeG, threeB, 0);

            player1.PlayRate(gToB, 3);

            Assert.Equal(player1.GemField, new Caravan(0, 0, 9, 0));
        }

        [Fact]
        public void PlayRate_PlayTransmute_TransmuteThreeTimes()
        {
            Caravan caravan = new Caravan(2, 1, 1, 0);
            Player player1 = new Player("Tem", false, caravan);

            Caravan zero = new Caravan(0, 0, 0, 0);

            Rate transmute = new Rate(zero, zero, 3);

            //player1.PlayRate(transmute, 3);

            player1.TransmuteGem(Gem.Green);
            Assert.Equal(player1.GemField, new Caravan(2, 0, 2, 0));
            player1.TransmuteGem(Gem.Blue);
            Assert.Equal(player1.GemField, new Caravan(2, 0, 1, 1));
            player1.TransmuteGem(Gem.Yellow);
            Assert.Equal(player1.GemField, new Caravan(1, 1, 1, 1));
        }

        [Fact]
        public void PlayRest_PlayerWellRested()
        {
            Caravan caravan = new Caravan(0, 9, 0, 0);
            Player player = new Player("Tem", false, caravan);

            Caravan threeG = new Caravan(0, 3, 0, 0);
            Caravan threeB = new Caravan(0, 0, 3, 0);

            Rate gToB = new Rate(threeG, threeB, 0);

            player.RateHand.Add(gToB);

            player.PlayRate(gToB, 3);

            Assert.True(gToB.Played);

            player.PlayRest();

            Assert.False(gToB.Played);
        }

        [Fact]
        public void TakeRate_TakesFirstRate_FirstRateTaken()
        {
            Player player = new Player("Tem", false, new Caravan(2, 0, 0, 0));

            Board board = new Board();
            board.LoadRatesLibrary();

            for (int i = 0; i < board.Rates.Length; i++)
            {
                board.Rates[i] = board.FlipTopRate();
                board.RateTribute[i] = new Caravan(0, 0, 0, 0);
            }

            player.TakeRate(board, 0);

            Assert.Equal(3, player.RateHand.Count);
        }
    }
}
