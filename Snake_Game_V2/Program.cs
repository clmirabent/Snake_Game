using System;

namespace Snake_Game_V2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("-----WELCOME TO SNAKE GAME V2-----");
            var game = new Game();
            game.Run();
        }
        
    }
}