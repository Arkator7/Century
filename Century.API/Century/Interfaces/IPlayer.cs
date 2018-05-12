using System;
using System.Collections.Generic;

interface IPlayer {
    string ActionFeedback(string action, Order order, Rate rate, int usage, bool valid);
    int CalculateScore();
    bool FulfilOrder(Board board, Order order);
    bool TakeRate(Board board, int index);
    bool PlayRate(Rate rate, int use);
    void PlayRest();
}