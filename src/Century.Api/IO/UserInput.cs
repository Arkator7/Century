using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserInput
{
    public static string GetInput() {
        return Console.ReadLine().Trim();
    }

    public static void AnyKey() {
        Console.ReadKey();
    }

    public static (bool, string) GetCommands(Board board, Player player)
    {
        bool validPlay = false;
        string message = "";

        string[] playerMove = GetInput().Split(" ",
            StringSplitOptions.RemoveEmptyEntries);

        if (playerMove.Length > 0)
        {
            switch (playerMove[0])
            {
                case "b":
                    (validPlay, message) = player.FulfilOrder(playerMove.Skip(1).ToArray(), board);

                    break;
                case "t":
                    (validPlay, message) = player.TakeRate(playerMove.Skip(1).ToArray(), board);

                    break;
                case "r":
                    (validPlay, message) = player.PlayRate(playerMove.Skip(1).ToArray());

                    break;
                case "e":
                    (validPlay, message) = player.PlayRest(playerMove.Skip(1).ToArray());

                    break;
                default:
                    validPlay = false;
                    break;
            }
        }
        else
        {
            validPlay = false;
        }

        return (validPlay, message);
    }

    public static void DiscardGems(Caravan gemField)
    {
        while (gemField.TotalGems() > 10)
        {
            Console.WriteLine("Discard down to 10, choose Gem to discard");

            string input = GetInput();

            gemField.DiscardGem(Caravan.GemInput(input));
        }
    }

    public static string[] InitializePlayers()
    {
        List<string> names = new List<string>();

        int displayIndex = 1;
        string input;

        ProgramOutput.PromptEnterName(displayIndex);

        input = GetInput();

        if (input != "")
        {
            names.Add(input);
            displayIndex++;
        }

        while ((input != "" || names.Count() < 1) && names.Count() < 5)
        {
            ProgramOutput.PromptEnterName(displayIndex);
            input = GetInput();

            if (input != "")
            {
                names.Add(input);
                displayIndex++;
            }
        }

        string[] returnStringArray = names.Select(i => i.ToString()).ToArray();

        return returnStringArray;
    }

    public static void TransmuteGems(Caravan gemField, Rate rate)
    {
        for (int i = 0; i < rate.Transmute; i++)
        {
            Console.WriteLine("Choose Gem " + (i+1) + " to transmute");

            string input = GetInput();

            bool check = gemField.TransmuteGem(Caravan.GemInput(input));

            if (check == false)
            {
                Console.WriteLine("Invalid Input, try again");
                i--;
            }
        }
    }

    public static void TributeGems(Caravan gemField, Board board, int index)
    {
        for (int i = 0; i < index; i++)
        {
            Console.WriteLine("Taking a rate that isn't the first, " +
                "choose a gem to tribute");
            string input = GetInput();

            if (input.Length != 0)
            {
                Gem gemInput = Caravan.GemInput(input);

                if (gemField.HasGem(gemInput))
                {
                    gemField.DiscardGem(gemInput);
                    board.TributeRate(gemInput, i);
                }
                else
                {
                    Console.WriteLine("Invalid Input, try again");
                    i--;
                }
            }
            else
            {
                Console.WriteLine("Invalid Input, try again");
                i--;
            }
        }
    }
}

