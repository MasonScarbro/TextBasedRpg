using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.Entities;
using TextBasedRpg.GameObjects;

namespace TextBasedRpg.Libraries
{
    static internal class EnemyLibrary 
    {
        public static List<string> EnemyTypes = new List<string>
        {
            "Undead", "Beast", "Bandit", "Elemental", "Demon", "Machine", "Dragonkin"
        };

        public static List<string> Adjectives = new List<string>
        {
            "Savage", "Ancient", "Cursed", "Dark", "Vicious", "Mutated", "Burning", "Frozen"
        };

        public static List<string> BaseNames = new List<string>
        {
            "Goblin", "Skeleton", "Wolf", "Raider", "Slime", "Automaton", "Wyrmling"
        };

        

    }
}
