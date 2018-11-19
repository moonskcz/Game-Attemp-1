using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public abstract class Item
    {

        public int Value;
        public int RequiredLevel;
        public string Name;
        public string FlavourText;

        public Item ()
        {

        }

        public Item (int value, int requiredLevel, string name, string flavourText)
        {
            Value = value;
            RequiredLevel = requiredLevel;
            Name = name;
            FlavourText = flavourText;
        }

    }
}
