using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.StateManagment;
using TextBasedRpg.GameObjects;

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
        public void EndTurn()
        {
            Console.WriteLine($"{Name}'s turn has ended.");
            Abilities.ForEach(ability => ability.EndTurn());
        }
    }
}
