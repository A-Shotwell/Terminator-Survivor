using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TerminatorSurvivor
{
    internal class Location
    {
        // Location details
        public string type;
        public string distantDesc;
        public string closeDesc;

        // Character presence
        public bool isT600; // Determines if T-600 is at this locaiton
        public bool isT800; // Determines if T-800 is at this locaiton
        public bool isJohnConnor; // Determinds if John Connor is currently at this location
        public bool hasVisited; // Determines if John Connor has visited this location

        // Resources
        public bool isHealthPack;
        public bool isUpgrade;
        public bool isTermTrap;
        public bool isTermTrapActive;
        public bool isSoldier;

        // Environmental protection
        public double cover;

        // Terminators should not appear in these zones
        public string[] nullZones = { "Resistance Base", "HK-VTOL", "HK-TANK", "Harvester" };

        // For all random assignments and operations
        Random randNum = new Random();

        public Location(string type, bool isT800, bool isJohnConnor)
        {
            this.type = type;

            string[] assignedDescs;
            Text descText = new Text();

            // Location's descriptions assigned randomly from descriptions based on location type
            switch (type)
            {
                case "Wasteland":
                    assignedDescs = descText.GetDescs("Wasteland");
                    break;
                case "Bombed Building":
                    assignedDescs = descText.GetDescs("Bombed Building");
                    break;
                case "Scrapyard":
                    assignedDescs = descText.GetDescs("Scrapyard");
                    break;
                case "Ruined Hospital":
                    assignedDescs = descText.GetDescs("Ruined Hospital");
                    break;
                case "Resistance Base":
                    assignedDescs = descText.GetDescs("Resistance Base");
                    break;
                case "HK-VTOL":
                    assignedDescs = descText.GetDescs("HK-VTOL");
                    break;
                case "HK-TANK":
                    assignedDescs = descText.GetDescs("HK-TANK");
                    break;
                case "Harvester":
                    assignedDescs = descText.GetDescs("Harvester");
                    break;
                default:
                    Console.WriteLine($"INVALID LOCATION: {type}");
                    assignedDescs = new string[] { "INVALID LOCATION", "INVALID LOCATION" };
                    break;
            }

            this.distantDesc = assignedDescs[0];
            this.closeDesc = assignedDescs[1];

            // T-600 units only appear at certain locations
            if (!nullZones.Contains(this.type))
            {
                this.isT600 = randNum.Next(1, 11) > 3 ? true : false;
            }
            else
            {
                this.isT600 = false;
            }

            // T-800 units show up alone (Omitted to improve enemy contact rate)
            /*if (isT800)
            {
                this.isT600 = false;
            }*/

            // Initiate character presence. John connor placed in first established base.
            this.isJohnConnor = isJohnConnor;
            this.hasVisited = isJohnConnor ? true : false;

            // Health Packs are only offered at these three locations
            string[] healthLocs = { "Bombed Building", "Scrapyard", "Ruined Hospital" };
            if (healthLocs.Contains(this.type))
            {
                this.isHealthPack = randNum.Next(1, 11) > 5 ? true : false;
            }

            // Weapon upgrades are only found in Scrapyards, and only if that Scrapyard doesn't yield a Health Pack
            // Omitted health pack qualifier to improve item acquisition rate
            if (this.type == "Scrapyard" /*&& this.isHealthPack == false*/)
            {
                this.isUpgrade = randNum.Next(1, 11) > 8 ? true : false;
            }

            // Random chance of fallen Terminator aooearing in Scrapyard to booby trap against roaming T-800s. Initially inactive.
            this.isTermTrap = this.type == "Scrapyard" && randNum.Next(1, 11) > 5 ? true : false;
            this.isTermTrapActive = false;
            this.isSoldier = randNum.Next(1, 11) >= 4 ? true : false;

            // Bombed Buildings and Ruined Hospitals provide 15% cover bonus, Scrapyards provide 10%
            if (this.type == "Bombed Building" || this.type == "Ruined Hospital")
            {
                this.cover = 0.15;
            }
            else if (this.type == "Scrapyard" || this.type == "HK-VTOL" || this.type == "HK-TANK" || this.type == "Harvester")
            {
                this.cover = 0.1;
            }
            else
            {
                this.cover = 0;
            }
        }

        public override string ToString()
        {
            return $"{this.type.ToUpper()}:\n\tDISTANT DESCRIPTION: {this.distantDesc}\n\tCLOSE DESCRIPTION: {this.closeDesc}\n\tT600: {this.isT600}\n\tT800: {this.isT800}\n\tHEALTH PACK: {this.isHealthPack}\n\tWEAPON UPGRADE: {this.isUpgrade}\n\tDOWNED TERMINATOR: {this.isTermTrap}\n\tCOVER BONUS: {this.cover}\n";
        }

        public bool IsT800
        {
            get { return this.isT800; }
            set
            {
                // this.isT600 = false; (Omitted to improve enemy contact rate)
                this.isT800 = value;
            }
        }
    }
}
