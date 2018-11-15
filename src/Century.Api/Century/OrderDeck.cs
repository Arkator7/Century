using System;

namespace Century.Api.Century
{
    public class OrderDeck : Deck<Order>
    {

        public static Order[] CreateDeck()
        {
            return OrdersList.SetLibrary();
        }

        public static Order[] Shuffle(Order[] deck)
        {
            if (deck.Length <= 1) return deck;

            Random rnd = new Random();

            for (int i = 0; i < deck.Length; i++)
            {
                int randomChoiceIndex = rnd.Next(i, deck.Length - 1);

                Order temp = deck[randomChoiceIndex];

                deck[randomChoiceIndex] = deck[i];
                deck[i] = temp;
            }

            return deck;
        }
    }
}
