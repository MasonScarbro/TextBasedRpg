using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextBasedRpg.GameObjects;

namespace TextBasedRpg.Entities
{
    public class NPC : Entity
    {
        public string Dialogue { get; set; }

        public NPC(string name, int health, int attackPower, int defensePower, string dialogue, List<Ability> abilities = null)
            : base(name, health, attackPower, defensePower, abilities)
        {
            Dialogue = dialogue;
        }

        public void Speak()
        {
            Console.WriteLine($"{Name} says: {Dialogue}");
        }
    }
}
