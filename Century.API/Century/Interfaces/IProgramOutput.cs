using System;
using System.Collections.Generic;
using System.Text;

public interface IProgramOutput
{
    void CommandFeedback(string message);
    int EndGameScoring(Player[] players);
    bool InvalidInputWarning(string message);
    void PreExitPrompt();
    void PrintBoardState(Board board);
    void PrintPlayerState(Player player);
    void PromptEnterName(int displayIndex);
    void ValidInputMessage(string message);
}
