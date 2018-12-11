using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    class ENM : NPC
    {
        
        public List<Item> Inventory;
        public int Level;
        public int RangedAttack;
        public int MeleeAttack;
        public int Speed;


        public ENM(string name, int health, List<Item> inv, int Level, int rangedAtt, int meleeAtt, int speed) : base(health, name)
        {
            Inventory = inv;
            RangedAttack = rangedAtt;
            MeleeAttack = meleeAtt;
            Speed = speed;
        }

        public ENM(string name, int health, int Level, int rangedAtt, int meleeAtt, int speed) : base(health, name)
        {
            RangedAttack = rangedAtt;
            MeleeAttack = meleeAtt;
            Speed = speed;
        }

    }
}
