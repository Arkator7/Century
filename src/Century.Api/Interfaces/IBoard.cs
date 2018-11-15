using System;
using System.Collections.Generic;

public interface IBoard {
    void SetupBoard();
    void LoadRatesLibrary();
    void LoadOrdersLibrary();
    Order FlipTopOrder();
    Rate FlipTopRate();
    int TakeOrder(Order o);
    Caravan TakeRate(Rate r);
    void TributeRate(Gem gem, int index);
}
