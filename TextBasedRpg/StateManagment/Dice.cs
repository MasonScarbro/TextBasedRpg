using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRpg.StateManagment
{
    public class Dice
    {

        private Random random;
        private int sides; // Default to a 6-sided die

        public Dice(int? seed = null, int sides = 6)
        {
            if (seed.HasValue)
                random = new Random(seed.Value);
            else
                random = new Random();

            this.sides = sides;
        }

        public int Roll()
        {
            if (sides < 1)
                throw new ArgumentException("Number of sides must be greater than 0.");
            return random.Next(1, sides + 1);
        }
    }
}
