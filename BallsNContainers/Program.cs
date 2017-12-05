using System;

namespace BallsNContainers
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new BallsNContainersGame(8);

            Console.Write(string.Format("The zero-based index of the container which does not receive a ball is {0}", game.Play()));
            Console.ReadKey();
        }
    }
}
