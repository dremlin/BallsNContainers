using System;

namespace BallsNContainers
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new BallsNContainersGame();

            Console.Write($"The zero-based index of the container which does not receive a ball is {game.Play()}");
            Console.ReadKey();
        }
    }
}
