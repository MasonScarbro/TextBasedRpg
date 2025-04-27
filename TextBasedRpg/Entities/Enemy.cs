using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.Entities
{
    public class Enemy : Entity
    {
        public string Type;
        public int ExperienceReward { get; set; }

        public Enemy(string name, int health, int attackPower, int defensePower, string type, int experienceReward)
            : base(name, health, attackPower, defensePower)
        {
            Type = type;
            ExperienceReward = experienceReward;
        }

        public void AttackPlayer(Player player)
        {
            Console.WriteLine($"{Name} attacks {player.Name}!");
            player.TakeDamage(AttackPower);
            if (!player.IsAlive())
            {
                Console.WriteLine($"{player.Name} has been defeated!");
            }
        }

        public void DropLoot()
        {
            
            Random rand = new Random();
            int lootChance = rand.Next(1, 101);
            if (lootChance <= 20)
            {
                Console.WriteLine($"{Name} dropped loot!");
                // TODO: Implement loot drop logic here
            }
            else
            {
                Console.WriteLine($"{Name} did not drop any loot.");
            }
        }
    }
}
