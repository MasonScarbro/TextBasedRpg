using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.Entities;
using TextBasedRpg.StateManagment;
using TextBasedRpg.Libraries;

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
        
        public static Ability GenerateRandom(int powerlevel = 1)
        {
            Random rand = new Random();
            string modifier = AbilityLibrary.Modifiers[rand.Next(AbilityLibrary.Modifiers.Count)];
            string action = AbilityLibrary.ActionVerbs[rand.Next(AbilityLibrary.ActionVerbs.Count)];
            string theme = AbilityLibrary.Themes[rand.Next(AbilityLibrary.Themes.Count)];

            string name = $"{modifier} {theme}";
            string description = $"A {modifier.ToLower()} {theme.ToLower()} that {action.ToLower()}s the enemy.";

            int damage = rand.Next(5, 11) * powerlevel;
            int cooldown = rand.Next(1, 4);

            return new Ability(name, description, damage, cooldown);
        }

        public static List<Ability> GenerateRandomAsList(int powerlevel = 1, int count = 1)
        {
            List<Ability> abilities = new List<Ability>();
            for (int i = 0; i < count; i++)
            {
                abilities.Add(GenerateRandom(powerlevel));
            }
            return abilities;
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
