using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Century2.Api.Century
{
    public class RatesDeck : Deck<Rate>
    {

        public static Rate[] CreateDeck()
        {
            return RatesList.SetLibrary();
        }

        public static Rate[] Shuffle(Rate[] deck)
        {
            // if it's 1 or 0 items, just return
            if (deck.Length <= 1) return deck;

            Random rnd = new Random();

            // For each index in array
            for (int i = 0; i < deck.Length; i++)
            {

                // choose a random not-yet-placed item to place there
                // must be an item AFTER the current item, because the stuff
                // before has all already been placed
                int randomChoiceIndex = rnd.Next(i, deck.Length - 1);

                // place our random choice in the spot by swapping
                Rate temp = deck[randomChoiceIndex];

                deck[randomChoiceIndex] = deck[i];
                deck[i] = temp;
            }
    
            return deck;
        }
    }
}
