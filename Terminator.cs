using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminatorSurvivor
{
    // Classes are declared as their own files.
    internal class Terminator
    {
        // Varialbes that are accessible to the rest of the project are declared "public"
        public string model;
        public int hp;
        public string armor;
        public Weapon weapon;

        public Terminator(string aModel, Weapon assignedWeapon)
        {
            string[] availArmor =
            {
                "Steel",
                "Polycarbonate",
                "Coltan Alloy"
            };

            this.model = aModel;

            Random randNum = new Random();

            this.weapon = assignedWeapon;

            switch (this.model)
            {
                case "T-600":
                    this.hp = 1000;
                    this.armor = availArmor[randNum.Next(0, availArmor.Length - 1)];
                    break;
                case "T-800":
                    this.hp = 2500;
                    this.armor = availArmor[randNum.Next(0, availArmor.Length - 1)];
                    break;
                case "HK-VTOL":
                    this.hp = 3000;
                    this.armor = "Coltan Alloy";
                    break;
                case "HK-TANK":
                    this.hp = 3000;
                    this.armor = "Polycarbonate";
                    break;
                case "Harvester":
                    this.hp = 3000;
                    this.armor = "Steel";
                    break;
                default:
                    throw new Exception($"Invalid Terminator Model: {this.model}");
            }
        }

        public int[] Hunt(int[] currLoc, int[] johnLoc, bool alert)
        {
            if (this.model != "T-800")
            {
                Console.WriteLine($"IVALID MOVE: {this.model} DOES NOT HUNT. RETURNED TO [0,0]");
                return new int[] { 0, 0 };
            }

            int[] newLoc = currLoc;

            if (alert)
            {
                // Seeking behavior. Chase John Connor's currentl location.
                if (johnLoc[0] > currLoc[0])
                {
                    newLoc[0] = currLoc[0] + 1;
                }
                else if (johnLoc[0] < currLoc[0])
                {
                    newLoc[0] = currLoc[0] - 1;
                }
                else
                {
                    newLoc[0] = currLoc[0];
                }

                if (johnLoc[1] > currLoc[1])
                {
                    newLoc[1] = currLoc[1] + 1;
                }
                else if (johnLoc[1] < currLoc[1])
                {
                    newLoc[1] = currLoc[1] - 1;
                }
                else
                {
                    newLoc[1] = currLoc[1];
                }

                return newLoc;
            }
            else
            {
                // Random wandering.
                Random random = new Random();

                int loc1 = 0;
                int loc2 = 0;

                while (loc1 == 0 && loc2 == 0)
                {
                    loc1 = random.Next(-1, 2);
                    loc2 = random.Next(-1, 2);
                }

                newLoc[0] = currLoc[0] + loc1;
                newLoc[1] = currLoc[1] + loc2;

                return newLoc;
            }
        }

        public override string ToString()
        {
            return $"MODEL: {this.model}\nWEAPON: {this.weapon.name}\n    DAMAGE HIGH: {this.weapon.damageHigh}\n    DAMAGE LOW: {this.weapon.damageLow}\nARMOR: {this.armor}\nHP: {this.hp}\n";
        }
    }
}
