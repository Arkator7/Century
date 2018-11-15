using System;
using System.ComponentModel;

public class Caravan : ICaravan
{
    public int yellowGems; // { get; private set; }
    public int greenGems; // { get; private set; }
    public int blueGems; // { get; private set; }
    public int redGems; // { get; private set; }

    public Caravan(int yellowGems, int greenGems, int blueGems, int redGems)
    {
        this.yellowGems = yellowGems;
        this.greenGems = greenGems;
        this.blueGems = blueGems;
        this.redGems = redGems;
    }

    public string CaravanNotation()
    {
        string returnString = "";

        for (int i = 0; i < this.yellowGems; i++)
        {
            returnString += "Y";
        }
        for (int i = 0; i < this.greenGems; i++)
        {
            returnString += "G";
        }
        for (int i = 0; i < this.blueGems; i++)
        {
            returnString += "B";
        }
        for (int i = 0; i < this.redGems; i++)
        {
            returnString += "R";
        }

        return returnString;
    }

    public bool TransmuteGem(Gem selection)
    {
        ref int op = ref GemOperation(selection);

        if (selection != Gem.Red && op > 0)
        {
            ref int opUp = ref GemOperation(selection + 1);

            op -= 1;
            opUp += 1;

            return true;
        } else {
            return false;
        }
    }

    public bool DiscardGem(Gem selection)
    {
        ref int op = ref GemOperation(selection);

        if (op > 0) {
            op -= 1;
            return true;
        } else {
            return false;
        }
    }

    public void TributeRate(Gem gem)
    {
        switch (gem)
        {
            case Gem.Yellow:
                this.AddCaravan(new Caravan(1, 0, 0, 0));
                break;
            case Gem.Green:
                this.AddCaravan(new Caravan(0, 1, 0, 0));
                break;
            case Gem.Blue:
                this.AddCaravan(new Caravan(0, 0, 1, 0));
                break;
            case Gem.Red:
                this.AddCaravan(new Caravan(0, 0, 0, 1));
                break;
            default:
                throw new InvalidEnumArgumentException("Invalid gem: \'" +
                    gem + "\' for TributeRate()");
        }
    }


    public void AddCaravan(Caravan caravan)
    {
        this.yellowGems += caravan.yellowGems;
        this.greenGems += caravan.greenGems;
        this.blueGems += caravan.blueGems;
        this.redGems += caravan.redGems;
    }

    public bool HasGem(Gem gem)
    {
        switch (gem) {
            case Gem.Yellow:
                return this.yellowGems > 0;
            case Gem.Green:
                return this.greenGems > 0;
            case Gem.Blue:
                return this.blueGems > 0;
            case Gem.Red:
                return this.redGems > 0;
            default:
                throw new InvalidEnumArgumentException("Invalid gem: \'" +
                    gem + "\' for HasGem()");
        }
    }

    public static Gem GemInput(string input)
    {
        switch (input) {
            case "Y":
            case "y":
                return Gem.Yellow;
            case "G":
            case "g":
                return Gem.Green;
            case "B":
            case "b":
                return Gem.Blue;
            case "R":
            case "r":
                return Gem.Red;
            default:
                throw new InvalidEnumArgumentException("Invalid input: \'" +
                    input + "\' for GemInput()");
        }
    }

    private ref int GemOperation(Gem selection)
    {
        switch (selection) {
            case Gem.Yellow:
                return ref this.yellowGems;
            case Gem.Green:
                return ref this.greenGems;
            case Gem.Blue:
                return ref this.blueGems;
            case Gem.Red:
                return ref this.redGems;
            default:
                throw new InvalidOperationException();
        }
    }

    public bool HasInventory(Caravan requestedInventory)
    {
        return (this.yellowGems >= requestedInventory.yellowGems &&
                  this.greenGems >= requestedInventory.greenGems &&
                  this.blueGems >= requestedInventory.blueGems &&
                  this.redGems >= requestedInventory.redGems);
    }

    public int TotalGems()
    {
        return this.yellowGems + this.greenGems + this.blueGems + this.redGems;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj == this) return true;
        var van = obj as Caravan;
        return van != null && Equals(van);
    }

    protected bool Equals(Caravan van)
    {
        return this.yellowGems.Equals(van.yellowGems)
               && this.greenGems.Equals(van.greenGems)
               && this.blueGems.Equals(van.blueGems)
               && this.redGems.Equals(van.redGems);
    }
}
