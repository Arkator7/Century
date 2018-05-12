using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Century.Api.Century
{
    public class OrdersList : DeckList<Order>
    {
        static int[,] list = {
            {7, 3, 2, 0, 0},
            {8, 2, 0, 2, 0},
            {8, 2, 3, 0, 0},
            {9, 2, 1, 0, 1}, //+1
            {10, 0, 2, 2, 0},
            {10, 0, 5, 0, 0},
            {10, 2, 0, 0, 2},
            {11, 2, 0, 3, 0},
            {11, 3, 0, 0, 2},
            {12, 0, 0, 4, 0},
            {12, 0, 2, 0, 2},
            {12, 0, 2, 1, 1}, //+1
            {12, 0, 3, 2, 0},
            {12, 1, 1, 1, 1}, //+2
            {13, 0, 1, 2, 1}, //+1
            {13, 1, 0, 4, 0},
            {13, 2, 2, 2, 0}, //+1
            {14, 0, 1, 1, 2}, //+1
            {14, 0, 3, 0, 2},
            {14, 2, 0, 0, 3},
            {14, 3, 1, 1, 1}, //+2
            {15, 0, 0, 5, 0},
            {15, 2, 2, 0, 2}, //+1
            {16, 0, 0, 0, 4},
            {16, 0, 0, 4, 1},
            {16, 0, 2, 0, 3},
            {16, 1, 3, 1, 1}, //+2
            {17, 1, 0, 0, 4},
            {17, 1, 2, 2, 1}, //+2
            {17, 2, 0, 2, 2}, //+1
            {18, 0, 0, 2, 3},
            {18, 0, 1, 0, 4},
            {18, 1, 1, 3, 1}, //+2
            {19, 0, 0, 1, 4},
            {19, 0, 2, 2, 2}, //+1
            {19, 1, 0, 3, 2}, //+1
            {20, 0, 0, 0, 5},
            {20, 1, 0, 2, 3}, //+1
            {20, 1, 1, 1, 3}, //+2
        };

        static public Order[] SetLibrary()
        {
            Order[] orderDeck = new Order[list.GetLength(0)];

            for (int i = 0; i < list.GetLength(0); i++)
            {
                int[] row = new int[list.GetLength(1)];

                for (int j = 0; j < list.GetLength(1); j++)
                {
                    row[j] = list[i, j];
                }

                orderDeck[i] = NewElement(row);
            }

            return orderDeck;
        }

        private static Order NewElement(int[] info) {
            return new Order(info[0],
                    new Caravan(info[1], info[2], info[3], info[4]));
        }
    }
}
