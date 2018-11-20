using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public class ItemOffensiveRanged : ItemOffensive
    {
        public int Damage;
        public int Range;
        public int Reload;
        public int Accuracity;
        public int Rate;
        public int Magazine;

        public ItemOffensiveRanged (int value, int requiredLevel, string name, string flavourText, string zone, int damage, int range, int reload, int accuracity, int rate, int magazine) : base (value, requiredLevel, name, flavourText, zone)
        {
            Damage = damage;
            Rate = rate;
            Range = range;
            Accuracity = accuracity;
            Reload = reload;
            Magazine = magazine;
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
