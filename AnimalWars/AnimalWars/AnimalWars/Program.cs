using System;

namespace AnimalWars
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (AdventureAndRescueGame game = new AdventureAndRescueGame())
            {
                game.Run();
            }
        }
    }
#endif
}

