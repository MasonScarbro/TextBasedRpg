using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.Entities;

namespace TextBasedRpg.GameObjects
{
    public class Ability
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Damage { get; set; }
        public int Cooldown { get; set; }
        private int currentCooldown;

        
        public Ability(string name, string description, int damage, int cooldown)
        {
            Name = name;
            Description = description;
            Damage = damage;
            Cooldown = cooldown;
        }

        public void Activate(Entity target)
        {
            if (currentCooldown > 0)
            {
                Console.WriteLine($"{Name} is on cooldown for {Cooldown} turns.");
                return;
            }

            Console.WriteLine($"{Name} used on {target.Name}!");
            target.TakeDamage(Damage);
            currentCooldown = Cooldown;
        }

        public void EndTurn()
        {
            if (currentCooldown > 0)
            {
                currentCooldown--;
            }
        }
    }
}
