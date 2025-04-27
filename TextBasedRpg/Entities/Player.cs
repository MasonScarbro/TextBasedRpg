using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.GameObjects;
using TextBasedRpg.StateManagment;

namespace TextBasedRpg.Entities
{
    public class Player : Entity
    {
        public int Level { get; set; }
        public int Experience { get; set; }

        public List<string> Inventory { get; set; }


        public Player(string name = "Hero", int health = 100, int attackPower = 10, int defensePower = 5, List<Ability> abilities = null)
            : base(name, health, attackPower, defensePower, abilities)
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

        public void Attack(Entity target, Dice dice)
        {
            Console.WriteLine($"{Name} attacks {target.Name}!");
            int attackRoll = dice.Roll() + (AttackPower / 2);
            Console.WriteLine($"{Name} rolled {attackRoll} attack damage!");

            target.TakeDamage(attackRoll);
            if (!target.IsAlive())
            {
                Console.WriteLine($"{target.Name} has been defeated!");
                if (target is Enemy enemy)
                {
                    GainExperience(enemy.ExperienceReward);
                    enemy.DropLoot();
                }
            }
            else
            {
                Console.WriteLine($"{target.Name} has {target.Health} health remaining.");
            }
        }

        public void EndTurn()
        {
            Console.WriteLine($"{Name}'s turn has ended.");
            Abilities.ForEach(ability => ability.EndTurn());
        }
    }
}
