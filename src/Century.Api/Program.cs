namespace Century
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // I/O
            UserInput ui = new UserInput();
            ProgramOutput po = new ProgramOutput();

            //List of players
            ProgramOutput.WelcomeMessage();

            // Init board
            ProgramOutput.StartMessage();
            GameSystem gs = new GameSystem();
        }
    }
}