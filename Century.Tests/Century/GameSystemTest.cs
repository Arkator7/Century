using Century2.Api.Century;
using Xunit;

namespace Century2.Tests.Century
{
    public class GameSystemTest
    {
        UserInput ui = new UserInput();

        [Fact]
        public void InitialisePlayers_TwoPlayers_InitialiseSuccessfully() {
            string[] playerNames = {"Bob", "Jane", "Tmart"};

            Player[] players = GameSystem.InitialisePlayers(playerNames);

            Assert.False(GameSystem.CheckEndGameState(players));
            Assert.Equal("Bob", players[0].Name);
            Assert.Equal(players[0].GemField, new Caravan(2, 0, 0, 0));
            Assert.True(players[0].FirstPlayer);
            Assert.Equal("Jane", players[1].Name);
            Assert.Equal(players[1].GemField, new Caravan(2, 0, 0, 0));
            Assert.False(players[1].FirstPlayer);
            Assert.Equal("Tmart", players[2].Name);
            Assert.Equal(players[2].GemField, new Caravan(1, 1, 0, 0));
            Assert.False(players[2].FirstPlayer);
        }

        [Fact]
        public void checkEndGameStateTest_NewGame_ReturnsFalse() {
            Player[] players = new Player[1];
            players[0] = new Player("John", false, new Caravan(2, 0, 0, 0));

            Assert.False(GameSystem.CheckEndGameState(players));
        }

        [Fact]
        public void checkEndGameStateTest_EndGame_ReturnsTrue() {
            Player[] players = new Player[1];
            players[0] = new Player("John", false, new Caravan(2, 0, 0, 0));

            players[0].AcquiredOrder.Add(new Order(20, new Caravan(0, 0, 0, 5)));
            players[0].AcquiredOrder.Add(new Order(19, new Caravan(0, 2, 2, 2)));
            players[0].AcquiredOrder.Add(new Order(19, new Caravan(1, 0, 3, 2)));
            players[0].AcquiredOrder.Add(new Order(17, new Caravan(1, 0, 0, 4)));
            players[0].AcquiredOrder.Add(new Order(16, new Caravan(0, 0, 0, 4)));

            Assert.True(GameSystem.CheckEndGameState(players));
        }
    }
}
