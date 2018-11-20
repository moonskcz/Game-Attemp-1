using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public class ItemOffensiveArmour : ItemOffensive
    {

        public int Defence;
        public int ChemicalDefence;
        public int FrostDefence;


        public ItemOffensiveArmour (int value, int requiredLevel, string name, string flavourText, string zone, int defence, int chemDefence, int frostDefence) : base (value, requiredLevel, name, flavourText, zone)
        {
            Defence = defence;
            ChemicalDefence = chemDefence;
            FrostDefence = frostDefence;
        }

        override public List<int> EvalOffense ()
        {
            List<int> ret = new List<int>();
            ret.Add(0);
            ret.Add(Defence);
            return ret;
        }

    }
}
