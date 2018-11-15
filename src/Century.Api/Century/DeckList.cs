namespace Century.Api.Century
{
    public class DeckList<T>
    {
        static int[,] list = { };

        static public T[] SetLibrary()
        {
            T[] deck = new T[list.GetLength(0)];

            for (int i = 0; i < list.GetLength(0); i++)
            {
                int[] row = new int[list.GetLength(1)];

                for (int j = 0; j < list.GetLength(1); j++)
                {
                    row[j] = list[i, j];
                }

                deck[i] = NewElement(row);
            }

            return deck;
        }

        private static T NewElement(int[] info)
        {
            if (typeof(T) == typeof(int))
                return (T)(object) Sum(info);
            else if (typeof(T) == typeof(Order))
                return (T)(object)new Order(info[0],
                        new Caravan(info[1], info[2], info[3], info[4]));
            else if (typeof(T) == typeof(Rate))
                return (T)(object)new Rate(
                    new Caravan(info[0], info[1], info[2], info[3]),
                    new Caravan(info[4], info[5], info[6], info[7]),
                    info[8]
                );
            else
                return default(T);
        }

        private static int Sum(int[] info)
        {
            int returnInt = 0;

            for (int i = 0; i < info.Length; i++)
            {
                returnInt += info[i];
            }

            return returnInt;
        }
    }
}