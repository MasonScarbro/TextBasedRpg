using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.Entities;

namespace TextBasedRpg.StateManagment
{
    public class Game
    {
        private Player currPlayer;
        private List<Entity> entities;
        private State currState;
        private Dice dice = new Dice();

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
                switch (currState)
                {
                    case State.MainMenu:
                        MainMenu();
                        break;
                    case State.Gameplay:
                        Gameplay();
                        break;
                    case State.Inventory:
                        Inventory();
                        break;
                    case State.Stats:
                        Stats();
                        break;
                    case State.Exit:
                        return;
                }
            }
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Text-Based RPG!");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                currState = State.Gameplay;
            }
            else if (choice == "2")
            {
                currState = State.Exit;
            }
        }

        private void Gameplay()
        {
            Console.Clear();
            Console.WriteLine("Gameplay Menu");
            Console.WriteLine("1. Explore");
            Console.WriteLine("2. View Inventory");
            Console.WriteLine("3. View Stats");
            Console.WriteLine("4. Return to Main Menu");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Explore();
                    break;
                case "2":
                    currState = State.Inventory;
                    break;
                case "3":
                    currState = State.Stats;
                    break;
                case "4":
                    currState = State.MainMenu;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private void Explore()
        {
            Console.Clear();
            Console.WriteLine("You venture into the unknown...");

            // For now, we'll just randomly spawn an enemy
            Enemy enemy = new Enemy("Goblin", 30, 8, 3, "Beast", 50);
            entities.Add(enemy);

            Console.WriteLine($"A wild {enemy.Name} appears!");

            while (enemy.IsAlive() && currPlayer.IsAlive())
            {
                Console.WriteLine("\n1. Attack");
                Console.WriteLine("2. Run");
                Console.Write("Choose an action: ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    currPlayer.Attack(enemy, dice);
                    currPlayer.EndTurn();
                    if (enemy.IsAlive())
                    {
                        enemy.AttackPlayer(currPlayer, dice);
                        enemy.EndTurn();
                    }

                }
                else if (action == "2")
                {
                    Console.WriteLine("You fled the battle!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid action. Try again.");
                }
            }

            if (!currPlayer.IsAlive())
            {
                Console.WriteLine("Game Over. Press any key to exit...");
                Console.ReadKey();
                currState = State.Exit;
            }
        }

        private void Inventory()
        {
            Console.Clear();
            Console.WriteLine("Inventory Menu");
            if (currPlayer.Inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                for (int i = 0; i < currPlayer.Inventory.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currPlayer.Inventory[i]}");
                }

                Console.WriteLine("Select an item number to use it or press Enter to go back.");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int index) && index > 0 && index <= currPlayer.Inventory.Count)
                {
                    string selectedItem = currPlayer.Inventory[index - 1];
                    currPlayer.UseItem(selectedItem);
                }
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            currState = State.Gameplay;
        }
        private void Stats()
        {
            Console.Clear();
            Console.WriteLine("Player Stats:");
            Console.WriteLine($"Name: {currPlayer.Name}");
            Console.WriteLine($"Level: {currPlayer.Level}");
            Console.WriteLine($"Health: {currPlayer.Health}");
            Console.WriteLine($"Attack Power: {currPlayer.AttackPower}");
            Console.WriteLine($"Defense Power: {currPlayer.DefensePower}");
            Console.WriteLine($"Experience: {currPlayer.Experience}");
            Console.WriteLine();

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            currState = State.Gameplay;
        }

    }
}
