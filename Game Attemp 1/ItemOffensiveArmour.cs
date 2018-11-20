using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    class ItemOffensiveArmour : ItemOffensive
    {

        public int Defence;
        public int ChemicalDefence;
        public int FrostDefence;


        public ItemOffensiveArmour (int value, int requiredLevel, string name, string flavourText, string zone) : base (value, requiredLevel, name, flavourText, zone)
        {

        }

    }
}
