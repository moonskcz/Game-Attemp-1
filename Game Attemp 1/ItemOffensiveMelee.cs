using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public class ItemOffensiveMelee : ItemOffensive
    {
        public int Range;
        public int Rate;
        public int Area;

        public ItemOffensiveMelee(int value, int requiredLevel, string name, string flavourText, string zone, int range, int rate, int area) : base(value, requiredLevel, name, flavourText, zone)
        {
            Range = range;
            Rate = rate;
            Area = area;
        }

        override public void Use()
        {

        }


    }
}
