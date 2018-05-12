using System;
using System.Collections.Generic;
using System.Text;

public interface IUserInput
{
    string GetInput();
    void AnyKey();
    (bool, string) GetCommands(Board board, Player player);
    void DiscardGems(Caravan gemField);
    string[] InitializePlayers(IProgramOutput po);
    void TransmuteGems(Caravan gemField, Rate rate);
    void TributeGems(Caravan gemField, Board board, int index);
}
