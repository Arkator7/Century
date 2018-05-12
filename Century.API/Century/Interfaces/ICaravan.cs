using System;
using System.Collections.Generic;

public interface ICaravan
{
    bool TransmuteGem(Gem selection);
    bool DiscardGem(Gem selection);
    void TributeRate(Gem gem);
    void AddCaravan(Caravan caravan);
    bool HasGem(Gem gem);
    bool HasInventory(Caravan requestedInventory);
    int TotalGems();
}

public enum Gem {
    Yellow = 1,
    Green = 2,
    Blue = 3,
    Red = 4
}
