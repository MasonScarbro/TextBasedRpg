using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.Libraries
{
    internal static class LootLibrary
    {
        public static List<string> WeaponTypes = new List<string> { "Sword", "Axe", "Dagger", "Staff", "Bow" };
        public static List<string> ArmorTypes = new List<string> { "Helmet", "Chestplate", "Leggings", "Boots", "Shield" };
        public static List<string> Materials = new List<string> { "Iron", "Steel", "Obsidian", "Mythril", "Dragonbone", "Voidstone" };
    }
}
