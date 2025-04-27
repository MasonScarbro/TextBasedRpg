using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.Entities
{
    public class Entity
    {
        public string Name;
        public int Health;
        public int AttackPower;
        public int DefensePower;
        

        public Entity(string name, int health, int attackPower, int defensePower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            DefensePower = defensePower;
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
