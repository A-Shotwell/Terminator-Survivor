using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TerminatorSurvivor
{
    internal class Soldier
    {
        public string? name;
        public int hp;
        public string? armor;
        public Weapon? weapon;
        public bool fromBase;
        public bool isTerminator;

        public Soldier(Weapon assignedWeapon, bool isFromBase)
        {
            string[] availArmor =
            {
                "Kevlar",
                "ABS Anti-EO",
                "Heat Diffusion"
            };

            Text names = new Text();

            this.name = names.GetName();
            this.hp = 1500;   

            Random randNum = new Random();

            // Select random from available armor upon generation
            this.armor = availArmor[randNum.Next(0, availArmor.Length)];
            
            // Weapon is assigned externally to avoid repeating code in Terminator class
            this.weapon = assignedWeapon;          

            // Chance of found soldier being terminator infiltrator. Soldiers granted by bases are never infiltrators. 
            if (isFromBase)
            {
                this.isTerminator = false;
            }
            else
            {
                int isTerm = randNum.Next(1, 10);
                this.isTerminator = isTerm > 7 ? true : false;
            }
            
        }

        // Soldiers can be interviewed for detecting terminator infiltrator units 
        public void Speak()
        {
            // Return string either from normal or suspicious array??? Maybe think of a better method.
            Text dialogue = new Text();

            Console.WriteLine(dialogue.GetSpeech(this.isTerminator));
        }

        // For testing, return class object to formatted string
        public override string ToString()
        {
            return $"NAME: {this.name}\nHP: {this.hp}\nARMOR: {this.armor}\nWEAPON: {this.weapon.name}\nINFILTRATOR? -- {this.isTerminator}\n";
        }
    }
}
