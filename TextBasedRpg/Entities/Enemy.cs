using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.StateManagment;
using TextBasedRpg.GameObjects;
using TextBasedRpg.Libraries;

namespace TextBasedRpg.Entities
{
    public class Enemy : Entity
    {
        public string Type;
        public int ExperienceReward { get; set; }

        public Enemy(string name, int health, int attackPower, int defensePower, string type, int experienceReward, List<Ability> abilities = null)
            : base(name, health, attackPower, defensePower, abilities)
        {
            Type = type;
            ExperienceReward = experienceReward;
        }

        public static Enemy GenerateRandom(int playerLevel)
        {
            Random rand = new Random();
            string adjective = EnemyLibrary.Adjectives[rand.Next(EnemyLibrary.Adjectives.Count)];
            string baseName = EnemyLibrary.BaseNames[rand.Next(EnemyLibrary.BaseNames.Count)];
            string type = EnemyLibrary.EnemyTypes[rand.Next(EnemyLibrary.EnemyTypes.Count)];
            string name = $"{adjective} {baseName}";

            
            int baseHealth = rand.Next(20, 40) + (playerLevel * 5);
            int baseAttack = rand.Next(5, 10) + (playerLevel * 2);
            int baseDefense = rand.Next(2, 6) + (playerLevel);

            int experienceReward = playerLevel * rand.Next(5, 15);

           
            List<Ability> abilities = new List<Ability>(); 

            return new Enemy(name, baseHealth, baseAttack, baseDefense, type, experienceReward, abilities);
        }

        public void AttackPlayer(Player player, Dice dice)
        {
            Console.WriteLine($"{Name} attacks {player.Name}!");
            int attackRoll = dice.Roll() + (AttackPower / 2);
            Console.WriteLine($"{Name} rolled {attackRoll} attack damage!");
            player.TakeDamage(attackRoll);
            if (!player.IsAlive())
            {
                Console.WriteLine($"{player.Name} has been defeated!");
            }
        }

        // I might not implement this method
        public void DropLoot(Player player)
        {
            
            Random rand = new Random();
            int lootChance = rand.Next(1, 101);
            if (lootChance <= 20)
            {
                
                Item item = Item.GenerateRandom(player);
                Console.WriteLine($"{Name} dropped {item.Name}");
                player.Inventory.AddItem(item);
            }
            else
            {
                Console.WriteLine($"{Name} did not drop any loot.");
            }
        }
        public void EndTurn()
        {
            Console.WriteLine($"{Name}'s turn has ended.");
            Abilities.ForEach(ability => ability.EndTurn());
        }
    }
}
