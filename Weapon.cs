using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TerminatorSurvivor
{
    /*
     * Weapons will come in three classes: "projectile", "plasma", "explosive". 
     * Armor will come in three classes: "ballistic / steel", "ordnance / polycarbonate", "titanium / coltan alloy".
     * 
     * Depending on the type of armor, a weapon's damage output will be received at a percentage 
     * of its total; increased for effective, decreased for ineffective:
     * 
     * Projectile: [- Ballistic / Steel] [+ Ordnance / Polycarbonate]
     * Plasma: [- Titanium / Coltan Alloy] [+ Ballistic / Steel]
     * Explosive: [- Ordnance / Polycarbonate] [+ Titanium / Coltan Alloy]
     * 
     * Weapon damage will be calculated as follows:
     * - Damage total will be calculated randomly between damageHigh and damageLow
     * - Defense total will 
     * - Damage penalty or bonus will be calculated at armor's type penalty, or armor bonus percentage.
    */

    internal class Weapon
    {
        string[] weaponNames = {
            "FN P90 Tactical Submachine Gun",
            "US M4A1 Battle Rifle",
            "M134-A2 Vulcan Minigun",
            "M203 Grenade Launcher",
            "Milkor MGL",
            "RPG-7",
            "P-40WR Phased Plasma Rifle",
            "P-120WR Heavy Plasma Repeater",
            "CPA-200WR “Dead Flash” Plasma Cannon"
        };

        string[] weaponTypes =
        {
            "projectile",
            "explosive",
            "plasma"
        };

        public string? name;
        public string? type;
        public int damageHigh;
        public int damageLow;

        public Weapon(string aName, string aType, int aDamageHigh, int aDamageLow) 
        {
            this.name = aName;
            this.type = aType;
            this.damageHigh = aDamageHigh;
            this.damageLow = aDamageLow;
        }

        // Fire function willl return random damage within given range
        // Character armor penalty/bonus and stage cover bonus will be added to/subtracted from damage returned
        // If target is Terminator, there is no cover bonus, only armor bonus/penalty
        public int Fire(string armor, double coverBonus, bool isTerminator)
        {
            int damageInt = RandomNumberGenerator.GetInt32(this.damageLow, this.damageHigh);
            double damage = damageInt;

            double armorBonus;

            switch (armor) {
                case "Kevlar":
                    if (this.type == "projectile")
                    {
                        armorBonus = 0.2;
                    }
                    else if (this.type == "plasma")
                    {
                        armorBonus = -0.2;
                    }
                    else
                    {
                        armorBonus = 0.0;
                    }
                    break;
                case "Steel":
                    if (this.type == "projectile")
                    {
                        armorBonus = 0.2;
                    }
                    else if (this.type == "plasma")
                    {
                        armorBonus = -0.2;
                    }
                    else
                    {
                        armorBonus = 0.0;
                    }
                    break;
                case "ABS Anti-EO":
                    if (this.type == "explosive")
                    {
                        armorBonus = 0.2;
                    }
                    else if (this.type == "projectile")
                    {
                        armorBonus = -0.2;
                    }
                    else
                    {
                        armorBonus = 0.0;
                    }
                    break;
                case "Polycarbonate":
                    if (this.type == "explosive")
                    {
                        armorBonus = 0.2;
                    }
                    else if (this.type == "projectile")
                    {
                        armorBonus = -0.2;
                    }
                    else
                    {
                        armorBonus = 0.0;
                    }
                    break;
                case "Heat Diffusion":
                    if (this.type == "plasma")
                    {
                        armorBonus = 0.2;
                    }
                    else if (this.type == "explosive")
                    {
                        armorBonus = -0.2;
                    }
                    else
                    {
                        armorBonus = 0.0;
                    }
                    break;
                case "Coltan Alloy":
                    if (this.type == "plasma")
                    {
                        armorBonus = 0.2;
                    }
                    else if (this.type == "explosive")
                    {
                        armorBonus = -0.2;
                    }
                    else
                    {
                        armorBonus = 0.0;
                    }
                    break;
                default:
                    Console.WriteLine($"\nINVALID ARMOR TYPE: {armor}\n)");
                    armorBonus = 0.0;
                    break;
            }

            if (isTerminator)
            {
                return Convert.ToInt32(Math.Round(damage - (damage * armorBonus), 0));
            }

            return Convert.ToInt32(Math.Round(damage - (damage * armorBonus) - (damage * coverBonus), 0));
        }

        // Upgrade weapon per weapon type and John Connor weapon level
        public void Upgrade(int level)
        {
            string[] weapons = new string[] {
                "FN P90 Tactical Submachine Gun",
                "M203 Grenade Launcher",
                "P-40WR Plasma Rifle",
                "US M4A1 Battle Rifle",
                "Milkor MGL",
                "P-120WR Heavy Plasma Repeater"
            };

            if (weapons.Contains(this.name))
            {
                switch (this.type)
                {
                    case "projectile":
                        if (level == 2)
                        {
                            this.name = weaponNames[1];
                        }
                        else if (level == 3)
                        {
                            this.name = weaponNames[2];
                        }
                        break;
                    case "explosive":
                        if (level == 2)
                        {
                            this.name = weaponNames[4];
                        }
                        else if (level == 3)
                        {
                            this.name = weaponNames[5];
                        }
                        break;
                    case "plasma":
                        if (level == 2)
                        {
                            this.name = weaponNames[7];
                        }
                        else if (level == 3)
                        {
                            this.name = weaponNames[8];
                        }
                        break;
                    default:
                        Console.WriteLine($"INVALID WEAPON: {this.name}, {this.type}");
                        break;
                }

                if (level == 2)
                {
                    this.damageHigh = 300;
                    this.damageLow = 200;
                }
                else if (level == 3)
                {
                    this.damageHigh = 400;
                    this.damageLow = 300;
                }
                else { Console.WriteLine($"INVALID LEVEL: {level}"); }
            }
            else
            {
                throw new Exception($"THIS WEAPON DOES NOT UPGRADE: {this.name}");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (weaponNames.Contains(value))
                {
                    name = value;
                };
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                if (weaponTypes.Contains(value))
                {
                    type = value;
                };
            }
        }
    }
}
