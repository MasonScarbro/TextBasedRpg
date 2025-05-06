using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.GameObjects
{
    public enum EquipResult
    {
        Success,
        WeaponSlotFull,
        ArmorSlotFull,
        NotInInventory,
        Consumable
    }
    public class Inventory
    {
        public List<Item> AllItems { get; set; }
        public List<Item> EquippedItems { get; set; }
        public int WeaponCount = 0;
        public int ArmorCount = 0;
        public int WeaponLimit = 1;
        public int ArmorLimit = 3;
        public Inventory()
        {
            AllItems = new List<Item>();
            EquippedItems = new List<Item>();
        }

        public void ShowInventory()
        {
            Console.WriteLine("Inventory:");
            foreach (var item in AllItems)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void ShowEquippedItems()
        {
            Console.WriteLine("Equipped Items:");
            foreach (var item in EquippedItems)
            {
                Console.WriteLine(item.ToString());
            }
        }

        

        public void AddItem(Item item)
        {
            AllItems.Add(item);
        }

        public void RemoveItem(Item item)
        {
            AllItems.Remove(item);
        }

        
    }
}
