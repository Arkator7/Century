using System;

public class Board : IBoard
{
    public Order[] OrderDeck { get; private set; }
    public Rate[] RatesDeck { get; private set; }

    public Order[] Orders { get; private set; } = new Order[5];
    public Rate[] Rates { get; private set; } = new Rate[6];
    public Caravan[] RateTribute { get; private set; } = new Caravan[6];

    public Board()
    {

    }

    public void SetupBoard()
    {
        LoadRatesLibrary();
        this.RatesDeck = Century.Api.Century.RatesDeck.Shuffle(this.RatesDeck);

        for (int i = 0; i < this.Rates.Length; i++)
        {
            this.Rates[i] = FlipTopRate();
        }

        LoadOrdersLibrary();
        this.OrderDeck = Century.Api.Century.OrderDeck.Shuffle(this.OrderDeck);

        for (int i = 0; i < this.Orders.Length; i++)
        {
            this.Orders[i] = FlipTopOrder();
        }

        for (int i = 0; i < this.RateTribute.Length; i++)
        {
            this.RateTribute[i] = new Caravan(0, 0, 0, 0);
        }
    }

    public void LoadOrdersLibrary()
    {
        this.OrderDeck = Century.Api.Century.OrderDeck.CreateDeck();
    }

    public void LoadRatesLibrary()
    {
        this.RatesDeck = Century.Api.Century.RatesDeck.CreateDeck();
    }

    public Order FlipTopOrder()
    {
        Order topElement = this.OrderDeck[0];

        RemoveOrder("deck", 0);

        return topElement;
    }

    public Rate FlipTopRate()
    {
        Rate topElement = this.RatesDeck[0];

        RemoveRate("deck", 0);

        return topElement;
    }

    private void RemoveOrder(string source, int idx = 0)
    {
        Order[] destination;

        switch(source) {
            case "deck":
                // Note: The nature of the game only involves the top card of
                // the library. Can hard-code this condition.

                destination = new Order[this.OrderDeck.Length - 1];
                Array.Copy(this.OrderDeck, 1, destination, 0,
                    this.OrderDeck.Length - 1);

                this.OrderDeck = destination;

                break;
            case "field":
                destination = new Order[this.Orders.Length];
                if (idx > 0)
                    Array.Copy(this.Orders, 0, destination, 0, idx);

                if (idx < this.Orders.Length - 1)
                    Array.Copy(this.Orders, idx + 1, destination, idx,
                        this.Orders.Length - idx - 1);

                destination[this.Orders.Length - 1] = FlipTopOrder();

                this.Orders = destination;
                break;
            // default:
            //     throw new InvalidOperationException("Invalid source: \'" + 
            //         source + "\' for RemoveOrder()");
        }
    }

    private void RemoveRate(string source, int idx = 0)
    {
        Rate[] destination;

        switch(source) {
            case "deck":
                // Note: The nature of the game only involves the top card of
                // the library. Can hard-code this condition.

                destination = new Rate[this.RatesDeck.Length - 1];
                Array.Copy(this.RatesDeck, 1, destination, 0,
                    this.RatesDeck.Length - 1);

                this.RatesDeck = destination;

                break;
            case "field":
                destination = new Rate[this.Rates.Length];
                Caravan[] caravanDestination = 
                    new Caravan[this.RateTribute.Length];

                if (idx > 0)
                    Array.Copy(this.Rates, 0, destination, 0, idx);
                if (idx > 0)
                    Array.Copy(this.RateTribute, 0, caravanDestination, 0, idx);

                if (idx < this.Rates.Length - 1)
                    Array.Copy(this.Rates, idx + 1, destination, idx,
                        this.Rates.Length - idx - 1);
                if (idx < this.Orders.Length - 1)
                    Array.Copy(this.RateTribute, idx + 1, caravanDestination, idx,
                        this.RateTribute.Length - idx - 1);

                destination[this.Rates.Length - 1] = FlipTopRate();
                caravanDestination[this.RateTribute.Length - 1] = 
                    new Caravan(0, 0, 0, 0);

                this.Rates = destination;
                this.RateTribute = caravanDestination;

                break;
            // default:
            //     throw new InvalidOperationException("Invalid source: \'" +
            //         source + "\' for RemoveRate()");
        }

    }

    public int TakeOrder(Order order)
    {
        int[] returnValue = { 3, 1, 0, 0, 0 };

        for(int i = 0; i < this.Orders.Length; i++)
        {
            if (this.Orders[i] == order)
            {
                RemoveOrder("field", i);

                return returnValue[i];
            }
        }

        return -1;
    }

    public Caravan TakeRate(Rate rate)
    {
        Caravan returnCaravan = new Caravan(0, 0, 0, 0);

        for (int i = 0; i < this.Rates.Length; i++)
        {
            if (this.Rates[i] == rate)
            {
                returnCaravan = this.RateTribute[i];

                RemoveRate("field", i);
            }
        }

        return returnCaravan;
    }

    public void TributeRate(Gem gem, int index)
    {
        this.RateTribute[index].TributeRate(gem);
    }
}
