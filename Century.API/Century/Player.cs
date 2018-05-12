using System;
using System.Collections.Generic;

public class Player : IPlayer
{
    public bool FirstPlayer { get; private set; }
    public string Name { get; private set; }
    public Caravan GemField { get; private set; }
    public List<Order> AcquiredOrder { get; private set; } = new List<Order>();
    public List<Rate> RateHand { get; private set; } = new List<Rate>();
    public List<int> ExtraPoints { get; private set; } = new List<int>();

    public Player(string playerName, bool firstPlay, Caravan startGem)
    {
        Name = playerName;
        FirstPlayer = firstPlay;
        GemField = startGem;

        Caravan zero = new Caravan( 0, 0, 0, 0 );
        Caravan twoYellow = new Caravan( 2, 0, 0, 0 );

        this.RateHand.Add(new Rate(zero, twoYellow, 0));
        this.RateHand.Add(new Rate(zero, zero, 2));
    }

    public string PlayerNotation()
    {
        string returnString = "Name: " + this.Name + "\nGem: " +
                              this.GemField.CaravanNotation() + 
                              "\nPlayer Orders: [";

        for (int i = 0; i < this.AcquiredOrder.Count; i++)
        {
            returnString += i + ": (" + this.AcquiredOrder[i].OrderNotation() +
                            "), ";
        }

        returnString += "]\nPlayer Extra Points: [";

        for (int i = 0; i < this.AcquiredOrder.Count; i++)
        {
            returnString += i + ": (" + this.ExtraPoints[i] + "), ";
        }

        returnString += "]\nPlayer Rates (total): [";

        for (int i = 0; i < this.RateHand.Count; i++)
        {
            returnString += i + ": (" + this.RateHand[i].RateNotation() + "), ";
        }

        returnString += "]\nPlayer Rates (can play): [";

        for (int i = 0; i < this.RateHand.Count; i++)
        {
            if (this.RateHand[i].Played == false)
            {
                returnString += i + ": (";
                returnString += this.RateHand[i].RateNotation() + "), ";
            }
        }

        returnString += "]";

        return returnString;
    }

    public string ActionFeedback(string action, Order order,
        Rate rate, int usage, bool valid)
    {
        if (valid)
        {
            switch (action)
            {
                case "b":
                    return this.Name + " filfills the order: (" +
                        order.OrderNotation() + ") !!!";
                case "t":
                    return this.Name + " takes the rate: (" + rate.RateNotation() +
                        ")";
                case "r":
                    if (rate.Transmute != 0)
                    {
                        return this.Name + " does a gem transmutation up to " +
                            rate.Transmute + " gems!";
                    }
                    else if (rate.RateOut.Equals(new Caravan(0,0,0,0)))
                    {
                        return this.Name + " mines the gems: (" +
                            rate.RateIn.CaravanNotation() + ") !";
                    }
                    else
                    {
                        return this.Name + " does the conversion: (" +
                            rate.RateNotation() + ") " + usage + " times";
                    }
                case "e":
                    return this.Name + " has taken a break.";
                default:
                    throw new InvalidOperationException();
            }
        }
        else
        {
            switch (action)
            {
                case "b":
                    return this.Name + " attempts to filfill the order: (" +
                        order.OrderNotation() + "), but fails.";
                case "t":
                    return this.Name + " tries to takes the rate: (" +
                        rate.RateNotation() + "), but can't";
                case "r":
                    if (rate.Transmute != 0)
                    {
                        return this.Name + " can't do gem transmutations of " +
                            rate.Transmute + "!";
                    }
                    else if (rate.RateOut.Equals(new Caravan(0,0,0,0)))
                    {
                        return this.Name + " can't mines the gems: (" +
                            rate.RateIn.CaravanNotation() + ") " + usage +
                            " times!";
                    }
                    else
                    {
                        return this.Name + " is unable to use the conversion: (" +
                            rate.RateNotation() + ") " + usage + " times!";
                    }
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public int CalculateScore()
    {
        int victoryPoint = 0;
        int extraPoint = 0;

        for (int i = 0; i < this.AcquiredOrder.Count; i++)
        {
            victoryPoint += this.AcquiredOrder[i].victoryPoint;
            extraPoint += this.ExtraPoints[i];
        }

        // Non-Yellow Gems get a point
        int gemPoints = this.GemField.greenGems + this.GemField.blueGems +
            this.GemField.redGems;

        int scoreTotal = victoryPoint + extraPoint + gemPoints;

        Console.WriteLine("Player " + this.Name + " scored " + victoryPoint +
            " in orders,");
        Console.WriteLine(extraPoint + " in bonus points, and " + gemPoints +
            " for non-yellow gems.");
        Console.WriteLine("totalling " + scoreTotal + " points!");

        return scoreTotal;
    }

    public (bool, string) FulfilOrder(string[] playerMove, Board board)
    {
        bool validPlay = false;
        string message = "";

        int orderIndex = -1;

        if (playerMove.Length > 0)
        {
            if (Int32.TryParse(playerMove[0], out orderIndex) &&
                orderIndex < 5)
            {
                Order orderToTake = board.Orders[orderIndex];
                validPlay = this.FulfilOrder(board, orderToTake);
                message = this.ActionFeedback("b", 
                    orderToTake, null, 0, validPlay);
            }
            else
            {
                validPlay = false;
            }
        }
        else
        {
            validPlay = false;
        }

        return (validPlay, message);
    }

    public bool FulfilOrder(Board board, Order order)
    {
        bool validPlay = ValidateOrder(order.requiredGems, this.GemField);

        if (validPlay)
        {
            this.GemField.yellowGems = this.GemField.yellowGems - order.requiredGems.yellowGems;
            this.GemField.greenGems = this.GemField.greenGems - order.requiredGems.greenGems;
            this.GemField.blueGems = this.GemField.blueGems - order.requiredGems.blueGems;
            this.GemField.redGems = this.GemField.redGems - order.requiredGems.redGems;

            AcquiredOrder.Add(order);
            ExtraPoints.Add(board.TakeOrder(order));
        }

        return validPlay;
    }

    private bool ValidateOrder(Caravan requirements, Caravan inventory)
    {
        return inventory.HasInventory(requirements);
    }

    public (bool, string) PlayRate(string[] playerMove)
    {
        bool validPlay = false;
        string message = "";

        if (playerMove.Length > 1)
        {
            //1. xth rate
            int xth = -1;
            int usage = -1;

            if (int.TryParse(playerMove[0], out xth) &&
                xth < this.RateHand.Count &&
                int.TryParse(playerMove[1], out usage) &&
                usage > 0)
            {
                Rate rateToPlay = this.RateHand[xth];

                validPlay = this.PlayRate(rateToPlay, usage);

                message = this.ActionFeedback("r",
                    null, rateToPlay, usage, validPlay);
            }
            else
            {
                validPlay = false;
            }
        }
        else
        {
            validPlay = false;
        }

        return (validPlay, message);
    }

    public bool PlayRate(Rate rate, int use)
    {
        bool validPlay = !rate.Played && ValidRatePlay(rate, use);

        if (validPlay)
        {
            if (rate.Transmute != 0)
            {
                UserInput.TransmuteGems(this.GemField, rate);

                rate.SetPlayed(true);
            }
            else
            {
                if (ValidRatePlay(rate, use))
                {
                    ExecuteRate(rate, use);
                }

                UserInput.DiscardGems(this.GemField);

                rate.SetPlayed(true);
            }
        }
        else
        {
            Console.WriteLine("Invalid Input, try again");
        }

        return validPlay;
    }

    public void TransmuteGem(Gem selection)
    {
        this.GemField.TransmuteGem(selection);
    }

    private bool ValidRatePlay(Rate rate, int use)
    {
        if (rate.RateOut.Equals(new Caravan(0,0,0,0)))
        {
            return use == 1;
        }
        else
        {
            return (
              (GemField.yellowGems +
                use * (rate.RateIn.yellowGems - rate.RateOut.yellowGems) >= 0) &&
              (GemField.greenGems +
                use * (rate.RateIn.greenGems - rate.RateOut.greenGems) >= 0) &&
              (GemField.blueGems +
                use * (rate.RateIn.blueGems - rate.RateOut.blueGems) >= 0) &&
              (GemField.redGems +
                use * (rate.RateIn.redGems - rate.RateOut.redGems) >= 0)
            );
        }
    }

    private void ExecuteRate(Rate rate, int use)
    {
        this.GemField.yellowGems = this.GemField.yellowGems +
          use * (rate.RateIn.yellowGems - rate.RateOut.yellowGems);
        this.GemField.greenGems = this.GemField.greenGems +
          use * (rate.RateIn.greenGems - rate.RateOut.greenGems);
        this.GemField.blueGems = this.GemField.blueGems +
          use * (rate.RateIn.blueGems - rate.RateOut.blueGems);
        this.GemField.redGems = this.GemField.redGems +
          use * (rate.RateIn.redGems - rate.RateOut.redGems);
    }

    public (bool, string) PlayRest(string[] playerMove)
    {
        bool validPlay = true;
        string message = "";

        this.PlayRest();

        message = this.ActionFeedback("e",
            null, null, 0, validPlay);

        return (validPlay, message);
    }

    public void PlayRest()
    {
        for (int i = 0; i < this.RateHand.Count; i++)
        {
            this.RateHand[i].SetPlayed(false);
        }
    }

    public (bool, string) TakeRate(string[] playerMove, Board board)
    {
        bool validPlay = false;
        string message = "";

        int rateIndex = -1;

        if (playerMove.Length > 0)
        {
            if (Int32.TryParse(playerMove[0], out rateIndex) &&
                rateIndex < 6)
            {
                Rate rateToTake = board.Rates[rateIndex];
                validPlay = this.TakeRate(board, rateIndex);
                message = this.ActionFeedback("t",
                    null, rateToTake, 0, validPlay);
            }
            else
            {
                validPlay = false;
            }
        }
        else
        {
            validPlay = false;
        }

        return (validPlay, message);
    }

    public bool TakeRate(Board board, int index)
    {
        bool validPlay = RateExist(board, board.Rates[index]) &&
            index <= GemField.TotalGems();

        //Check board for rate
        if (validPlay)
        {
            UserInput.TributeGems(this.GemField, board, index);

            //Add Rate to player rateHand
            this.RateHand.Add(board.Rates[index]);

            //Add tributed gems to player caravan
            this.GemField.AddCaravan(board.RateTribute[index]);

            while (this.GemField.TotalGems() > 10)
            {
                UserInput.DiscardGems(this.GemField);
            }

            //Notify Board to remove rate from field and display next rate
            board.TakeRate(board.Rates[index]);
        }

        return validPlay;
    }

    private bool RateExist(Board board, Rate rate)
    {
        bool returnBool = false;

        for (int i = 0; i < board.Rates.Length; i++)
        {
            if (rate == board.Rates[i])
            {
                returnBool = true;
            }
        }

        return returnBool;
    }
}
