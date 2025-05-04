using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.GameObjects
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable,
        
    }
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }

        public int HealthRestore { get; set; }
        public int AttackPower { get; set; }
        public int DefensePower { get; set; }

        public Item(string name, ItemType type, int healthRestore = 0, int attackPower = 0, int defensePower = 0, string description = "An Item")
        {
            Name = name.ToLower();
            Type = type;
            Description = description;
            HealthRestore = healthRestore;
            AttackPower = attackPower;
            DefensePower = defensePower;
        }

        public override string ToString()
        {
            return $"{Name} - {Description}\n" +
                   $"Type: {Type}\n" +
                   $"Health Restore: {HealthRestore}\n" +
                   $"Attack Power: {AttackPower}\n" +
                   $"Defense Power: {DefensePower}" +
                   $"\n------------------------------";
        }
    }
}
