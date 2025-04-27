using TextBasedRpg.Entities;

namespace TextBasedRpg
{
    public class Game
    {
        private Player currPlayer;
        private List<Entity> entities;
        private State currState;

        public Game()
        {
            currPlayer = new Player();
            entities = new List<Entity>();
            currState = State.MainMenu;
        }

        public void Start()
        {
            while (true)
            {
                //switch (currState)
                //{
                //    case State.MainMenu:
                //        MainMenu();
                //        break;
                //    case State.Gameplay:
                //        Gameplay();
                //        break;
                //    case State.Inventory:
                //        Inventory();
                //        break;
                //    case State.Stats:
                //        Stats();
                //        break;
                //    case State.Exit:
                //        return;
                //}
            }
        }
    }

}
