using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Attemp_1
{
    public class Player
    {

        public string Name = "Player Name";

        public List<Item> Inventory = new List<Item>();
        public Dictionary<string, int> Properties = new Dictionary<string, int>();
        public List<Skill> Skills = new List<Skill>();
        public int Level = 1;
        public int Exp = 0;

        public Dictionary<string, ItemOffensive> Equiped = new Dictionary<string, ItemOffensive>();

        public Player ()
        {
            Properties.Add("Health", 100);
            Properties.Add("Inteligence", 4000);
            Properties.Add("Strenght", 2000);
            Properties.Add("Speed", 2000);
            Properties.Add("Perception", 5000);
            Properties.Add("Money", 0);
            Properties.Add("Attack", 0);
            Properties.Add("Defence", 0);

            AddToInv(new ItemOffensiveRanged(80, 1, "JoJos Friend", "sum flavour text", "Weapon", 40, 7, 5, 1, 3, 1));
            AddToInv(new ItemOffensiveRanged(80, 1, "JoJos Friends CLONE", "sum flavour text", "Weapon", 40, 7, 5, 1, 3, 1));
            AddToInv(new ItemOffensiveRanged(9999999, 1, "Chastity weapon", "would rather not describe", "Weapon", 35, 7, 5, 1, 5, 3));
            AddToInv(new ItemOffensiveMelee(40 , 4, "JoJos Other Friend", "sum flavour text", "Weapon", 85, 1, 3, 5));

            AddToInv(new ItemUtyl(15, 1, "Health potion", "A bitter piece of shit that somehow heals your wounds"));

        }

        public Player (Player player)
        {
            Name = player.Name;
            Inventory = player.Inventory;
            Properties = player.Properties;
            Skills = player.Skills;
            Level = player.Level;
            Exp = player.Exp;
            Equiped = player.Equiped;
        }

        public List<int> EvalOffense ()
        {

            List<int> CombatForce = new List<int>();
            CombatForce.Add(0);
            CombatForce.Add(0);
            
            List<int> tmp;

            foreach (ItemOffensive item in Equiped.Values)
            {
                tmp = item.EvalOffense();
                CombatForce[0] += tmp[0];
                CombatForce[1] += tmp[1];
            }

            return CombatForce;

        }

        public void RefreshOffenseStats ()
        {
            List<int> tmp = EvalOffense();

            Properties["Attack"] = tmp[0];
            Properties["Defence"] = tmp[1];

        }

        public void AddToInv (Item item)
        {
            Inventory.Add(item);
        }

        public void Equip(ItemOffensive item)
        {

            if (Level >= item.RequiredLevel && item is ItemOffensive)
            {
                ItemOffensive tmp;

                if (Inventory.Contains(item))
                {
                    Inventory.Remove(item);
                }

                if (Equiped.TryGetValue(item.Zone, out tmp))
                {
                    if (Equiped[item.Zone] is Item)
                    {
                        AddToInv(Equiped[item.Zone]);
                    }
                }

            }

            RefreshOffenseStats();

        }

        public void Equip(string itemName)
        {
            ItemOffensive tmp;

            foreach (Item item in Inventory)
            {

                if (item.Name.Equals(itemName) && item is ItemOffensive && Level >= item.RequiredLevel)
                {
                    ItemOffensive iitem = (ItemOffensive)item;

                    string zone = iitem.Zone;

                    if (Inventory.Contains(item))
                    {
                        Inventory.Remove(item);
                    }

                    if (Equiped.TryGetValue(iitem.Zone, out tmp))
                    {
                        if (Equiped[iitem.Zone] is Item)
                        {
                            AddToInv(Equiped[iitem.Zone]);
                        }
                    }

                    Equiped[iitem.Zone] = iitem;

                    RefreshOffenseStats();

                    return;

                }
            }

        }

        public void Unequip (ItemOffensive item)
        {

            if (Equiped.Values.Contains(item))
            {
                AddToInv(Equiped[item.Zone]);
                Equiped.Remove(item.Zone);
                RefreshOffenseStats();
            }

        }

        public void Unequip (string itemName)
        {

            Dictionary<string, ItemOffensive> copy = new Dictionary<string, ItemOffensive>(Equiped);

            foreach (ItemOffensive item in copy.Values)
            {
                if (item.Name.Equals(itemName))
                {
                    AddToInv(Equiped[item.Zone]);
                    Equiped.Remove(item.Zone);

                    RefreshOffenseStats();

                    return;
                }

            }

        }

        public void PerformActivity (string property, int value)
        {
            if (Properties.Keys.Contains(property))
            {
                Properties[property] += value;
            }
        }

    }
}
