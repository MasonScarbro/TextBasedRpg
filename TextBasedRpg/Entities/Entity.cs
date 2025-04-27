using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.GameObjects;

namespace TextBasedRpg.Entities
{
    public class Entity
    {
        public string Name;
        public int Health;
        public int AttackPower;
        public int DefensePower;
        public List<Ability> Abilities = new List<Ability> { new("quack", "does nothing", 0, 1) };

        public Entity(string name, int health, int attackPower, int defensePower, List<Ability> abilities)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            DefensePower = defensePower;
            if (abilities != null) Abilities = abilities;
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = damage - DefensePower;
            if (actualDamage < 0)
            {
                actualDamage = 0;
            }
            Health -= actualDamage;
            if (Health < 0)
            {
                Health = 0;
            }
        }

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
