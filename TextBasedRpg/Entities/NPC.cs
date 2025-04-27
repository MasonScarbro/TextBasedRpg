using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.Entities
{
    public class NPC : Entity
    {
        public string Dialogue { get; set; }

        public NPC(string name, int health, int attackPower, int defensePower, string dialogue)
            : base(name, health, attackPower, defensePower)
        {
            Dialogue = dialogue;
        }

        public void Speak()
        {
            Console.WriteLine($"{Name} says: {Dialogue}");
        }
    }
}
