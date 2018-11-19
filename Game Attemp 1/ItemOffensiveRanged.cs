using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public class ItemOffensiveRanged : ItemOffensive
    {

        public int Range;
        public int Reload;
        public int Accuracity;
        public int Rate;

        public ItemOffensiveRanged (int value, int requiredLevel, string name, string flavourText, string zone, int range, int reload, int accuracity, int rate) : base (value, requiredLevel, name, flavourText, zone)
        {
            Rate = rate;
            Range = range;
            Accuracity = accuracity;
            Rate = rate;
        }

    }
}
