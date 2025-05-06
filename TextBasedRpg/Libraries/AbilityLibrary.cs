using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.Libraries
{
    static internal class AbilityLibrary
    {
        public static List<string> ActionVerbs = new List<string>
        {
            "Slash", "Blast", "Sting", "Crush", "Pierce", "Burn", "Freeze", "Shock", "Drain", "Poison"
        };

        public static List<string> Modifiers = new List<string>
        {
            "Fiery", "Venomous", "Dark", "Thunderous", "Chilling", "Savage", "Holy", "Corrupting"
        };

        public static List<string> Themes = new List<string>
        {
            "Strike", "Wave", "Ray", "Bite", "Bolt", "Pulse", "Claw", "Smash"
        };
    }
}
