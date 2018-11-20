using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public class ItemOffensiveMelee : ItemOffensive
    {
        public int Damage;
        public int Range;
        public int Rate;
        public int Area;

        public ItemOffensiveMelee(int value, int requiredLevel, string name, string flavourText, string zone, int damage, int range, int rate, int area) : base(value, requiredLevel, name, flavourText, zone)
        {
            Damage = damage;
            Range = range;
            Rate = rate;
            Area = area;
        }

        override public List<int> EvalOffense()
        {
            List<int> ret = new List<int>();

            ret.Add(Damage);
            ret.Add(0);

            return ret;
        }


    }
}
