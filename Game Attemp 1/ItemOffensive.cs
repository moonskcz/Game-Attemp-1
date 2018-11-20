using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public class ItemOffensive : Item, ItemInterfaceMain
    {

        public string Zone;

        public ItemOffensive(int value, int requiredLevel, string name, string flavourText, string zone) : base(value, requiredLevel, name, flavourText)
        {
            Zone = zone;
        }

        virtual public void Use ()
        {



        }

        virtual public List<int> EvalOffense ()
        {
            List<int> ret = new List<int>(); //1st Offense 2nd Defence
            ret.Add(5);
            ret.Add(0);
            return ret;
        }

    }
}
