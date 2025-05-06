using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.Entities;
using TextBasedRpg.GameObjects;

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
            currPlayer.Inventory.AddItem(new Item("Health Potion", ItemType.Consumable, 20, 0, 0));
            currPlayer.Inventory.AddItem(new Item("Iron Sword", ItemType.Weapon, 0, 10, 0));
            currPlayer.Inventory.AddItem(new Item("Leather Armor", ItemType.Armor, 0, 0, 5));
            currPlayer.Inventory.AddItem(new Item("Wooden Shield", ItemType.Armor, 0, 0, 3));
            currPlayer.Inventory.AddItem(new Item("Health Potion", ItemType.Consumable, 20, 0, 0));
            currPlayer.Inventory.AddItem(new Item("Steel Sword", ItemType.Weapon, 0, 10, 0));
            currPlayer.Abilities.Add(new Ability("Fireball", "A powerful fire attack", 20, 3));
            currPlayer.Abilities.Add(new Ability("Ice Spike", "A freezing attack", 15, 3));
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
            Enemy enemy = Enemy.GenerateRandom(currPlayer.Level);
            entities.Add(enemy);

            Console.WriteLine($"A wild {enemy.Name} appears!");

            while (enemy.IsAlive() && currPlayer.IsAlive())
            {
                Console.WriteLine("\n1. Attack");
                Console.WriteLine("\n2. Abilities");
                Console.WriteLine("3. Run");
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
                    
                    for (int i = 0; i < currPlayer.Abilities.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {currPlayer.Abilities[i].ToString()})");
                    }
                    Console.WriteLine("\nEnter the number of the item you want to use/equip, or press Enter to return...");
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out int selectedIndex) &&
                        selectedIndex > 0 && selectedIndex <= currPlayer.Abilities.Count)
                    {
                        currPlayer.Attack(enemy, dice, ability: currPlayer.Abilities[selectedIndex - 1]);
                        currPlayer.EndTurn();
                        if (enemy.IsAlive())
                        {
                            enemy.AttackPlayer(currPlayer, dice);
                            enemy.EndTurn();
                        }
                        
                    }
                }
                else if (action == "3")
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
            else if (!enemy.IsAlive())
            {
                Console.WriteLine("\nThe battle has ended.");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

        private void Inventory()
        {
            Console.Clear();
            Console.WriteLine("Inventory Menu");
            var items = currPlayer.Inventory.AllItems;
            if (currPlayer.Inventory.AllItems.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {items[i].Name} ({items[i].Type})");
                }
                Console.WriteLine($"\n_________________ Equipped _________________");
                foreach (Item equipped in currPlayer.Inventory.EquippedItems)
                {
                    Console.WriteLine($"{equipped.ToString()}");
                }

                Console.WriteLine("\nEnter the number of the item you want to use/equip, or press Enter to return...");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int selectedIndex) &&
                selectedIndex > 0 && selectedIndex <= items.Count)
                {
                    Item item = items[selectedIndex - 1];
                    EquipResult result = currPlayer.UseItem(item);
                    switch (result)
                    {
                        case EquipResult.Success:
                            Console.WriteLine($"You equipped {item.Name}.");
                            break;
                        case EquipResult.WeaponSlotFull:
                            Console.WriteLine("You already have a weapon equipped. Unequip it first? (yes/no)");
                            if (Console.ReadLine()?.ToLower() == "yes")
                            {
                                currPlayer.UnequipItem(ItemType.Weapon);
                                currPlayer.UseItem(item); 
                            }
                            break;
                        case EquipResult.ArmorSlotFull:
                            Console.WriteLine("You're wearing too much armor. Unequip one? (yes/no)");
                            if (Console.ReadLine()?.ToLower() == "yes")
                            {
                                currPlayer.UnequipItem(ItemType.Armor);
                                currPlayer.UseItem(item); 
                            }
                            break;
                        case EquipResult.Consumable:
                            Console.WriteLine($"{item.Name} consumed. Health is now {currPlayer.Health}.");
                            break;
                        case EquipResult.NotInInventory:
                            Console.WriteLine($"{item.Name} is not in your inventory.");
                            break;
                    }
                    
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
