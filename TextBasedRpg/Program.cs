using TextBasedRpg.Entities;
using TextBasedRpg.StateManagment;

namespace TextBasedRpg
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }

}
