using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.Entities;
using TextBasedRpg.Libraries;

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


        public static Item GenerateRandom(Player player)
        {
            Random rng = new Random();

            ItemType type = (ItemType)rng.Next(0, 2); // 0 = Weapon, 1 = Armor, 2 = Consumable
            string material = LootLibrary.Materials[rng.Next(LootLibrary.Materials.Count)];

            string baseName;

            int atk = 0;
            int def = 0;

            if (type == ItemType.Weapon)
            {
                baseName = LootLibrary.WeaponTypes[rng.Next(LootLibrary.WeaponTypes.Count)];
                atk = rng.Next(1, 5) + player.Level * rng.Next(1, 4);
            }
            else
            {
                baseName = LootLibrary.ArmorTypes[rng.Next(LootLibrary.ArmorTypes.Count)];
                def = rng.Next(1, 5) + player.Level * rng.Next(1, 4);
            }

            return new Item($"{material} {baseName}", type, 0, atk, def, $"A {baseName.ToLower()} forged from {material.ToLower()}.");
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
