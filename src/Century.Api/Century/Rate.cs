using Century.Api.Century;
using System;
using System.Collections.Generic;

public class Rate
{
    public Caravan RateOut { get; private set; }
    public Caravan RateIn { get; private set; }
    public int Transmute { get; private set; }
    public bool Played { get; private set; }

    public Rate(Caravan rOut, Caravan rIn, int tMute)
    {
        RateOut = rOut;
        RateIn = rIn;
        Transmute = tMute;
        Played = false;
    }

    public string RateNotation()
    {
        string returnString = "";

        if (this.RateOut.CaravanNotation() == "" && 
            this.RateIn.CaravanNotation() == "")
        {
            returnString += "{" + this.Transmute + "}";
        } else {
            if (this.RateOut.CaravanNotation() != "")
            {
                returnString += this.RateOut.CaravanNotation();
            } else {
                returnString += "()";
            }

            returnString += " -> " + this.RateIn.CaravanNotation();
        }

        return returnString;
    }

    public void SetPlayed(bool played)
    {
        this.Played = played;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj == this) return true;
        var rate = obj as Rate;
        return rate != null && Equals(rate);
    }

    protected bool Equals(Rate rate)
    {
        return RateOut.Equals(rate.RateOut)
               && RateIn.Equals(rate.RateIn)
               && Transmute.Equals(rate.Transmute)
               && Played.Equals(rate.Played);
    }
}
