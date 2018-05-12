using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProgramOutput
{
    public static void CommandFeedback(string message)
    {
        Console.WriteLine(message);
    }

    public static int EndGameScoring(Player[] players)
    {
        Console.WriteLine("Game Ended");

        int highscore = 0;
        int index = 0;

        for (int i = 0; i < players.Length; i++)
        {
            int score = players[i].CalculateScore();

            if (score > highscore)
            {
                highscore = score;
                index = i;
            }
        }

        Console.WriteLine(players[index].Name + " wins with " + highscore +
            " points!");

        return highscore;
    }

    public static void PreExitPrompt()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static string GenerateDivider()
    {
        string returnString = "";

        for (int i = 0; i < 20; i++)
        {
            returnString += "-";
        }

        return returnString;
    }

    public static bool InvalidInputWarning(string message)
    {
        Console.Clear();
        Console.WriteLine(GenerateDivider());
        Console.WriteLine(message);
        Console.WriteLine("Invalid Input, try again");
        Console.WriteLine(GenerateDivider());

        return false;
    }

    public static string OrdersNotation(Order[] orders)
    {
        string returnString = "Board Orders: [";

        for (int i = 0; i < orders.Length; i++)
        {
            returnString += i + ": (";
            returnString += orders[i].OrderNotation() + "), ";
        }

        returnString += "]";

        return returnString;
    }

    public static void PrintBoardState(Board board)
    {
        Console.WriteLine(OrdersNotation(board.Orders));
        Console.WriteLine(RatesNotation(board.Rates));
        Console.WriteLine(RateTributeNotation(board.RateTribute));
        Console.WriteLine(GenerateDivider());
    }

    public static void PrintPlayerState(Player player)
    {
        Console.WriteLine(player.PlayerNotation());
        Console.WriteLine(GenerateDivider());
    }

    public static void PromptEnterName(int index)
    {
        if (index == 1)
        {
            Console.WriteLine("Player " + index + ", enter your name");
        } else {
            Console.WriteLine("Player " + index + ", enter your name (or press Enter to start game)");
        }
    }

    public static string RatesNotation(Rate[] rates)
    {
        string returnString = "Board Rates: [";

        for (int i = 0; i < rates.Length; i++)
        {
            returnString += i + ": (" + rates[i].RateNotation() + "), ";
        }

        returnString += "]";

        return returnString;
    }

    public static string RateTributeNotation(Caravan[] rateTribute)
    {
        string returnString = "Board RatesTribute: [";

        for (int i = 0; i < rateTribute.Length; i++)
        {
            returnString += i;
            returnString += ": (";
            returnString += rateTribute[i].CaravanNotation();
            returnString += "), ";
        }

        returnString += "]";

        return returnString;
    }

    public static void StartMessage()
    {
        Console.WriteLine("Starting Game");
    }

    public static void ValidInputMessage(string message)
    {
        Console.Clear();
        Console.WriteLine(GenerateDivider());
        Console.WriteLine(message);
        Console.WriteLine(GenerateDivider());
    }

    public static void WelcomeMessage()
    {
        Console.WriteLine("Hello, Welcome to Century: Golem Edition");
    }
}
