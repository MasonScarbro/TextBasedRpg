using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.Entities;
using TextBasedRpg.StateManagment;

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
        
        public bool IsOnCoolDown() => currentCooldown > 0;

        public void StartCooldown()
        {
            if (currentCooldown == 0)
            {
                currentCooldown = Cooldown + 1; // plus one because the turn you use it is not a cooldown turn
            }
            else
            {
                Console.WriteLine($"{Name} is on cooldown for {currentCooldown} turns.");
            }
        }

        public void EndTurn()
        {
            if (currentCooldown > 0)
            {
                currentCooldown--;
            }
        }
        
        public override string ToString()
        {
            return $"{Name} - {Description}\n" +
                   $"{Damage} | {currentCooldown}";
        }
    }
}
