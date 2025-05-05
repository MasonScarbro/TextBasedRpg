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

        public Inventory Inventory { get; set; }
        public int MaxHealth { get; set; } = 100;

        public Player(string name = "Hero", int health = 100, int attackPower = 10, int defensePower = 5, List<Ability> abilities = null)
            : base(name, health, attackPower, defensePower, abilities)
        {
            Level = 1;
            Experience = 0;
            Inventory = new Inventory();
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
            MaxHealth = Health; 
            Console.WriteLine($"{Name} leveled up to level {Level}!");
        }

        public EquipResult UseItem(Item item)
        {
            if (!Inventory.AllItems.Contains(item)) return EquipResult.NotInInventory;
            if (item.Type == ItemType.Consumable) 
            {
                Health += item.HealthRestore;
                if (Health > MaxHealth)
                {
                    Health = MaxHealth;
                }
                DefensePower += item.DefensePower;
                AttackPower += item.AttackPower; // if the consumable gives any it can just be permanent?
                Inventory.RemoveItem(item);
                return EquipResult.Consumable;
            } else
            {
                if (item.Type == ItemType.Weapon && Inventory.WeaponCount >= Inventory.WeaponLimit)
                    return EquipResult.WeaponSlotFull;

                if (item.Type == ItemType.Armor && Inventory.ArmorCount >= Inventory.ArmorLimit)
                    return EquipResult.ArmorSlotFull;

                Inventory.EquippedItems.Add(item);
                Inventory.AllItems.Remove(item);
                if (item.Type == ItemType.Weapon)
                    Inventory.WeaponCount++;
                else if (item.Type == ItemType.Armor)
                    Inventory.ArmorCount++;

                return EquipResult.Success;

            }

        }


        public void AddItemToInventory(Item item)
        {
            Inventory.AddItem(item);
            Console.WriteLine($"{item.Name} has been added to the inventory.");
        }

        
        public void Attack(Entity target, Dice dice, Ability ability = null)
        {
            int attackRoll;
            if (ability != null)
            {
                ability.StartCooldown();
                Console.WriteLine($"{Name} uses {ability.Name} on {target.Name}!");
                attackRoll = dice.Roll() + (ability.Damage / 2);
                
            }
            else
            {
                Console.WriteLine($"{Name} attacks {target.Name}!");
                attackRoll = dice.Roll() + (AttackPower / 2);
                
            }
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
