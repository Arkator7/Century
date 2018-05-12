using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Century2 {
    public class Program {
        public static void Main(string[] args) {
            // BuildWebHost(args).Run();

            // I/O
            UserInput ui = new UserInput();
            ProgramOutput po = new ProgramOutput();

            //List of players
            ProgramOutput.WelcomeMessage();

            // Init board
            ProgramOutput.StartMessage();
            GameSystem gs = new GameSystem();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
