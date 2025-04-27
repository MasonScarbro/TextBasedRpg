using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.Entities
{
    public class Player : Entity
    {
        public int Level { get; set; }
        public int Experience { get; set; }

        public List<string> Inventory { get; set; }

        public Player(string name = "Hero", int health = 100, int attackPower = 10, int defensePower = 5)
            : base(name, health, attackPower, defensePower)
        {
            Level = 1;
            Experience = 0;
            Inventory = new List<string>();
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            if (Experience >= Level * 100) // Example level-up condition
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            AttackPower += 2; // Increase attack power on level up
            DefensePower += 1; // Increase defense power on level up
            Health += 10; // Increase health on level up
            Console.WriteLine($"{Name} leveled up to level {Level}!");
        }

        public void UseItem(string item)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Console.WriteLine($"{Name} used {item}.");
                // Implement item effects here
            }
            else
            {
                Console.WriteLine($"{Name} does not have {item} in the inventory.");
            }
        }

        public void Attack(Entity target)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");
            target.TakeDamage(AttackPower);
            if (!target.IsAlive())
            {
                Console.WriteLine($"{target.Name} has been defeated!");
            }
            else
            {
                Console.WriteLine($"{target.Name} has {target.Health} health remaining.");
            }
        }
    }
}
