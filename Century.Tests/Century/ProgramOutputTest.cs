using Century.Api.Century;
using Xunit;

namespace Century.Tests.Century
{
    public class ProgramOutputTest
    {
        [Fact]
        public void ActionFeedback_ValidRConvert_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("r",
                null, new Rate(new Caravan(0, 3, 0, 0),
                new Caravan(0, 0, 3, 0), 0), 2, true);

            Assert.Equal("James does the conversion: (GGG -> BBB) 2 times",
                displayString);
        }

        [Fact]
        public void ActionFeedback_ValidRMine_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("r",
                null, new Rate(new Caravan(0, 0, 0, 0),
                new Caravan(2, 1, 0, 0), 0), 1, true);

            Assert.Equal("James mines the gems: (YYG) !",
                displayString);
        }

        [Fact]
        public void ActionFeedback_ValidRTransmute_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("r",
                null, new Rate(new Caravan(0, 0, 0, 0),
                new Caravan(0, 0, 0, 0), 3), 1, true);

            Assert.Equal("James does a gem transmutation up to 3 gems!",
                displayString);
        }

        [Fact]
        public void ActionFeedback_ValidE_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("e",
                null, null, 0, true);

            Assert.Equal("James has taken a break.",
                displayString);
        }

        [Fact]
        public void ActionFeedback_InvalidB_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("b",
                new Order(20, new Caravan(0, 0, 0, 5)), null, 0, false);

            Assert.Equal("James attempts to filfill the order:" +
                " (20 - {RRRRR}), but fails.", displayString);
        }

        [Fact]
        public void ActionFeedback_InvalidT_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("t",
                null, new Rate(new Caravan(3, 0, 0, 0),
                new Caravan(0, 3, 0, 0), 0), 0, false);

            Assert.Equal("James tries to takes the rate: (YYY -> GGG)," +
                " but can't", displayString);
        }

        [Fact]
        public void ActionFeedback_InvalidRConvert_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("r",
                null, new Rate(new Caravan(0, 3, 0, 0),
                new Caravan(0, 0, 3, 0), 0), 2, false);

            Assert.Equal("James is unable to use the conversion:" +
                " (GGG -> BBB) 2 times!", displayString);
        }

        [Fact]
        public void ActionFeedback_InvalidRMine_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("r",
                null, new Rate(new Caravan(0, 0, 0, 0),
                new Caravan(2, 1, 0, 0), 0), 1, false);

            Assert.Equal("James can't mines the gems: (YYG) 1 times!",
                displayString);
        }

        [Fact]
        public void ActionFeedback_InvalidRTransmute_DisplayMessage()
        {
            Player player = new Player("James", false, new Caravan(0, 0, 0, 0));

            string displayString = player.ActionFeedback("r",
                null, new Rate(new Caravan(0, 0, 0, 0),
                new Caravan(0, 0, 0, 0), 3), 1, false);

            Assert.Equal("James can't do gem transmutations of 3!",
                displayString);
        }

        [Fact]
        public void CalculateScore_Player1_DisplayScore()
        {
            Player player = new Player("Will", false, new Caravan(1, 3, 0, 1));

            player.AcquiredOrder.Add(new Order(20, new Caravan(0, 0, 0, 5)));
            player.AcquiredOrder.Add(new Order(19, new Caravan(0, 0, 1, 4)));
            player.AcquiredOrder.Add(new Order(16, new Caravan(0, 0, 4, 1)));
            player.AcquiredOrder.Add(new Order(15, new Caravan(2, 2, 0, 2)));
            player.AcquiredOrder.Add(new Order(14, new Caravan(0, 1, 1, 2)));

            player.ExtraPoints.Add(0);
            player.ExtraPoints.Add(1);
            player.ExtraPoints.Add(3);
            player.ExtraPoints.Add(0);
            player.ExtraPoints.Add(1);

            Assert.Equal(93, player.CalculateScore());
        }

        [Fact]
        public void CalculateScore_Player2_DisplayScore()
        {
            Player player = new Player("Ben", false, new Caravan(0, 2, 2, 2));

            player.AcquiredOrder.Add(new Order(20, new Caravan(1, 0, 2, 3)));
            player.AcquiredOrder.Add(new Order(18, new Caravan(1, 1, 3, 1)));
            player.AcquiredOrder.Add(new Order(17, new Caravan(2, 0, 2, 2)));
            player.AcquiredOrder.Add(new Order(16, new Caravan(1, 3, 1, 1)));

            player.ExtraPoints.Add(1);
            player.ExtraPoints.Add(1);
            player.ExtraPoints.Add(1);
            player.ExtraPoints.Add(0);

            Assert.Equal(80, player.CalculateScore());
        }

        [Fact]
        public void CalculateScore_Player3_DisplayScore()
        {
            Player player = new Player("Ben", false, new Caravan(5, 0, 0, 0));

            player.AcquiredOrder.Add(new Order(15, new Caravan(2, 2, 0, 2)));
            player.AcquiredOrder.Add(new Order(12, new Caravan(1, 1, 1, 1)));
            player.AcquiredOrder.Add(new Order(13, new Caravan(2, 2, 2, 0)));

            player.ExtraPoints.Add(3);
            player.ExtraPoints.Add(1);
            player.ExtraPoints.Add(0);

            Assert.Equal(44, player.CalculateScore());
        }

        [Fact]
        public void EndGameScoring_ThreePlayerScore_DisplayWinner()
        {
            Player[] players = new Player[3];

            players[0] = new Player("Will", false, new Caravan(1, 3, 0, 1));

            players[0].AcquiredOrder.Add(new Order(20, new Caravan(0, 0, 0, 5)));
            players[0].AcquiredOrder.Add(new Order(19, new Caravan(0, 0, 1, 4)));
            players[0].AcquiredOrder.Add(new Order(16, new Caravan(0, 0, 4, 1)));
            players[0].AcquiredOrder.Add(new Order(15, new Caravan(2, 2, 0, 2)));
            players[0].AcquiredOrder.Add(new Order(14, new Caravan(0, 1, 1, 2)));

            players[0].ExtraPoints.Add(0);
            players[0].ExtraPoints.Add(1);
            players[0].ExtraPoints.Add(3);
            players[0].ExtraPoints.Add(0);
            players[0].ExtraPoints.Add(1);

            players[1] = new Player("Ben", false, new Caravan(0, 2, 2, 2));

            players[1].AcquiredOrder.Add(new Order(20, new Caravan(1, 0, 2, 3)));
            players[1].AcquiredOrder.Add(new Order(18, new Caravan(1, 1, 3, 1)));
            players[1].AcquiredOrder.Add(new Order(17, new Caravan(2, 0, 2, 2)));
            players[1].AcquiredOrder.Add(new Order(16, new Caravan(1, 3, 1, 1)));

            players[1].ExtraPoints.Add(1);
            players[1].ExtraPoints.Add(1);
            players[1].ExtraPoints.Add(1);
            players[1].ExtraPoints.Add(0);

            players[2] = new Player("Ben", false, new Caravan(5, 0, 0, 0));

            players[2].AcquiredOrder.Add(new Order(15, new Caravan(2, 2, 0, 2)));
            players[2].AcquiredOrder.Add(new Order(12, new Caravan(1, 1, 1, 1)));
            players[2].AcquiredOrder.Add(new Order(13, new Caravan(2, 2, 2, 0)));

            players[2].ExtraPoints.Add(3);
            players[2].ExtraPoints.Add(1);
            players[2].ExtraPoints.Add(0);

            Assert.Equal(93, ProgramOutput.EndGameScoring(players));
        }

        [Fact]
        public void InvalidInputWarning_ReturnsFalse()
        {
            Assert.False(ProgramOutput.InvalidInputWarning(""));
        }

        [Fact]
        public void OrderNotation_20_YGBRRR_DisplayOrder() {
            Order order = new Order(20, new Caravan(1, 1, 1, 3));

            string displayString = order.OrderNotation();

            Assert.Equal("20 - {YGBRRR}", displayString);
        }

        [Fact]
        public void OrdersNotation_DisplayOrders() {
            Order[] orders = new Order[5];

            orders[0] = new Order(20, new Caravan(1, 1, 1, 3));
            orders[1] = new Order(17, new Caravan(1, 0, 0, 4));
            orders[2] = new Order( 9, new Caravan(2, 1, 0, 1));
            orders[3] = new Order(12, new Caravan(0, 0, 4, 0));
            orders[4] = new Order(16, new Caravan(0, 2, 0, 3));

            string displayString = ProgramOutput.OrdersNotation(orders);

            Assert.Equal("Board Orders: [0: (20 - {YGBRRR}), 1: (17 - {YRRRR}), 2: (9 - {YYGR}), 3: (12 - {BBBB}), 4: (16 - {GGRRR}), ]", displayString);
        }

        [Fact]
        public void PrintBoardState_Passing() {
            Board board = new Board();

            board.SetupBoard();

            ProgramOutput.PrintBoardState(board);
        }

        [Fact]
        public void PrintPlayerState_Passing() {
            Player player = new Player("Jack", false, new Caravan(2, 0, 0, 0));

            ProgramOutput.PrintPlayerState(player);
        }

        [Fact]
        public void PromptEnterName_Index1_Passing() {
            ProgramOutput.PromptEnterName(1);
        }

        [Fact]
        public void PromptEnterName_Index2_Passing() {
            ProgramOutput.PromptEnterName(2);
        }

        [Fact]
        public void RateNotation_RRtoGGBBB_DisplayRate() {
            Rate rate = new Rate(new Caravan(0, 0, 0, 2), new Caravan(0, 2, 3, 0), 0);

            string displayString = rate.RateNotation();

            Assert.Equal("RR -> GGBBB", displayString);
        }

        [Fact]
        public void RatesNotation_DisplayRates() {
            Rate[] rates = new Rate[6];

            rates[0] = new Rate(new Caravan(0, 0, 0, 2), new Caravan(0, 2, 3, 0), 0);
            rates[1] = new Rate(new Caravan(2, 0, 1, 0), new Caravan(0, 0, 0, 2), 0);
            rates[2] = new Rate(new Caravan(4, 0, 0, 0), new Caravan(0, 0, 2, 0), 0);
            rates[3] = new Rate(new Caravan(0, 0, 0, 0), new Caravan(0, 0, 0, 0), 3);
            rates[4] = new Rate(new Caravan(3, 0, 0, 0), new Caravan(0, 3, 0, 0), 0);
            rates[5] = new Rate(new Caravan(0, 0, 0, 1), new Caravan(0, 0, 2, 0), 0);

            string displayString = ProgramOutput.RatesNotation(rates);

            Assert.Equal("Board Rates: [0: (RR -> GGBBB), 1: (YYB -> RR), 2: (YYYY -> BB), 3: ({3}), 4: (YYY -> GGG), 5: (R -> BB), ]", displayString);
        }

        [Fact]
        public void RateTributeNotation_DisplayRateTribute() {
            Caravan[] rateTribute = new Caravan[6];

            rateTribute[0] = new Caravan(2, 0, 0, 0);
            rateTribute[1] = new Caravan(0, 2, 0, 0);
            rateTribute[2] = new Caravan(1, 0, 0, 0);
            rateTribute[3] = new Caravan(0, 0, 0, 0);
            rateTribute[4] = new Caravan(0, 0, 0, 1);
            rateTribute[5] = new Caravan(1, 0, 1, 0);

            string displayString = ProgramOutput.RateTributeNotation(rateTribute);

            Assert.Equal("Board RatesTribute: [0: (YY), 1: (GG), 2: (Y), 3: (), 4: (R), 5: (YB), ]", displayString);
        }

        [Fact]
        public void StartMessage_Passing() {
            ProgramOutput.StartMessage();
        }

        [Fact]
        public void ValidInputMessage_Passing() {
            ProgramOutput.ValidInputMessage("");
        }

        [Fact]
        public void WelcomeMessage_Passing() {
            ProgramOutput.WelcomeMessage();
        }
    }
}
