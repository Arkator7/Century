using System;
using System.Linq;

public class GameSystem
{
    public Board board { get; private set; }
    public Player[] players { get; private set; }
    //IUserInput userInput;
    //IProgramOutput programOutput;

    public GameSystem()
    {
        this.InitiateGame();
        this.StartGame();
    }

    public void InitiateGame()
    {
        string[] players = new string[5];

        players[0] = "You";
        players[1] = "Miles";
        players[2] = "Giles";
        players[3] = "Niles";
        players[4] = "Biles";

        this.players = InitialisePlayers(players);
        //string[] players = UserInput.InitializePlayers();

        this.board = new Board();
        this.board.SetupBoard();
    }

    public void StartGame()
    {
        int turnIndex = 0;

        while (turnIndex % this.players.Length != 0 || !CheckEndGameState(this.players))
        {
            bool validPlay = false;
            string message = "";

            int playerIndex = turnIndex % this.players.Length;

            ProgramOutput.PrintBoardState(board);

            Player player = players[playerIndex];

            ProgramOutput.PrintPlayerState(player);

            (validPlay, message) = UserInput.GetCommands(board, player);

            ProgramOutput.CommandFeedback(message);

            if (validPlay == true)
            {
                ProgramOutput.ValidInputMessage(message);
                turnIndex++;
            }
            else
            {
                ProgramOutput.InvalidInputWarning(message);
            }
        }

        ProgramOutput.EndGameScoring(players);
        ProgramOutput.PreExitPrompt();
    }

    public static Player[] InitialisePlayers(string[] gamePlayers)
    {
        Player[] players = new Player[gamePlayers.Length];

        for (int i = 0; i < gamePlayers.Length; i++)
        {
            Caravan startGem;

            if (i == 0)
            {
                startGem = new Caravan( 2, 0, 0, 0 );
                players[i] = new Player(gamePlayers[i], true, startGem);
            }
            else if (i != gamePlayers.Length - 1)
            {
                startGem = new Caravan( 2, 0, 0, 0 );
                players[i] = new Player(gamePlayers[i], false, startGem);
            }
            else
            {
                startGem = new Caravan( 1, 1, 0, 0 );
                players[i] = new Player(gamePlayers[i], false, startGem);
            }
        }

        return players;
    }

    public static bool CheckEndGameState(Player[] players)
    {
        bool returnBool = false;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].AcquiredOrder.Count == 5)
            {
                returnBool = true;
            }
        }

        return returnBool;
    }
}
