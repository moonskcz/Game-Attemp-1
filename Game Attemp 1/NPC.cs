using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    abstract class NPC
    {

        public int Health;
        public string Name;

        public NPC (int health, string name)
        {
            Health = health;
            Name = name;
        }

    }
}
