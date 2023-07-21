using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminatorSurvivor
{
    internal class Text
    {
        // Game introduction text
        public string introduction = "INTRO TEXT\n";

        // Game tutorial text
        public string explanation = "Welcome to Terminator Survivor, a text-based survival game based on the Terminator movie franchise. Take control of John Connor, humanity's last hope against Skynet's marauding machines. Lead a team of soldiers in a search of three Skynet communication signals. Hijack those signals, bring down Skynet, and save humanity.\n\nTUTORIAL\n-----------------\n\nTo begin, select John Connor's initial armor and weapon. This game uses a strength/weakness weapon and armor system. Each soldier or terminator armor will receive a bonus, penalty, or neutrality according to these attributes:\n\nKevlar/Steel >>> Strong against \"projectile\", weak against \"plasma\"\nABS Anti-EO/Polycarbonate >>> Strong against \"explosive\", weak against \"projectile\"\nHeat Diffusion/Coltan Alloy >>> Strong against \"plasma\", weak against \"explosive\"\n\nThis game takes place on a 10x10 map of varying location types. These location types provide varying cover bonuses for your party, along with chances to find certain resources. These resources include weapon upgrades, health packs, opportunities to set traps for pursuing enemies, and soldiers that can be recruited to your party. John Connor will start with one ally and can have a party of up to four soldiers, including himself.\n\nYou will encounter three types of enemies in this game. T-600 terminators, which spawn randomly. T-800s, which patrol or actively pursue John Connor, depending on alert status. Encountering a T-800 alerts all other T-800s of your location for three turns. And finally, bosses, which are immobile on the game grid. There are three bosses in this game, each safeguarding a Skynet communication signal. Find and hijack these signals to win the game. \n\nThe game's main interface includes a description of your current location, periodic updates from the Resistance about a boss's current location, your current number of activated signals, and the main actions menu. Your main menu options are as follows:\n\n-- MOVE --\n\tThis option allows you to move John Connor's party in any compass direction on the game grid, so long as a grid space exists in that direction. If no space exists, you will be notified that you are attempting to move outside the game grid and prompted to select another direction. Take caution in your movement; landing on a space occupied by a terminator squad or a boss will initiate combat. T-800s will also patrol or pursue following this action. If a T-800 lands on your space, this will also initiate combat.\n\n-- SCOUT AHEAD --\n\tThis option allows you to look ahead to any neighboring game grid space without moving without triggering combat or terminator patrol. This will give you a brief description of what John Connor sees in your selected direction and inform you of any enemy presence. It will also mark the searched space with its type on the game map. It would be prudent to check surrounding areas and plan your next movement accordingly.\n\n-- SURVEY AREA --\n\tThis option allows you to check the area for resources, soldiers, and opportunities to set traps. Any T-800 that encounters a set trap will be destroyed on contact. Any soldier encountered on the battlefield is a potential T-800 infiltrator unit. Encountering a soldier will automatically add him to your party, but offer an immediate chance to speak. Here, you can interview the soldier and either keep him or dismiss him. If a T-800 infiltrator is dismissed, it will initiate combat. This option will not trigger terminator patrol/pursuit.\n\n-- VIEW MAP --\n\tThis option will display a graphic representation of the game map, including your current location, a legend, and the marked area types of any area that has either been visited or scouted. This option will not trigger terminator patrol/pursuit.\n\n-- PARTY STATUS --\n\tThis option will display the current status of all party members, including current remaining HP, armor type, and current weapon. This option will not trigger terminator patrol/pursuit.\n\n-- SPEAK TO PARTY MEMBER --\n\tThis option will allow you to speak to a selected party member. From here, the party member can be dismissed from your party if he arouses suspicion. Dismissing a T-800 infiltrator will initiate combat. This option will not trigger terminator patrol/pursuit.\n\n-- USE HEALTH PACK --\n\tThis option will allow you to use one of your health packs, if available. This will fully restore your party's HP. This option will not trigger terminator patrol/pursuit.\n\n-- CAMP --\n\tThis option will allow you to set up camp and rest your party. This will fully recover your party's HP, but beware. Only select this option if you trust your current party. If you are harboring a T-800 infiltrator, there is a chance that it will attempt to assassinate you in your sleep. A successful attempt results in game over. An unsuccessful attempt triggers combat, and your party HP will not be restored. Selecting this option will trigger terminator patrol/pursuit.\n\n";
        
        // Normal soldier speech lines
        public string[] normalSpeech =
        {
            "Normal Speech Test 1\n",
            "Normal Speech Test 2\n",
            "Normal Speech Test 3\n",
            "Normal Speech Test 4\n",
            "Normal Speech Test 5\n",
        };

        // Suspicious soldier speech lines
        public string[] suspiciousSpeech =
        {
            "Suspicious Speech Test 1\n",
            "Suspicious Speech Test 2\n",
            "Suspicious Speech Test 3\n",
            "Suspicious Speech Test 4\n",
        };

        // Descriptions of Wasteland locations
        public string[][] wastelandDescs =
        {
            new string[] { "WASTELAND DISTANT DESC 1", "WASTELAND CLOSE DESC 1" },
            new string[] { "WASTELAND DISTANT DESC 2", "WASTELAND CLOSE DESC 2" },
            new string[] { "WASTELAND DISTANT DESC 3", "WASTELAND CLOSE DESC 3" },
            new string[] { "WASTELAND DISTANT DESC 4", "WASTELAND CLOSE DESC 4" },
            new string[] { "WASTELAND DISTANT DESC 5", "WASTELAND CLOSE DESC 5" }
        };

        // Descriptions of Bombed Building locations
        public string[][] bombedBuildingDescs =
        {
            new string[] { "BOMBED BUILDING DISTANT DESC 1", "BOMBED BUILDING CLOSE DESC 1" },
            new string[] { "BOMBED BUILDING DISTANT DESC 2", "BOMBED BUILDING CLOSE DESC 2" },
            new string[] { "BOMBED BUILDING DISTANT DESC 3", "BOMBED BUILDING CLOSE DESC 3" },
            new string[] { "BOMBED BUILDING DISTANT DESC 4", "BOMBED BUILDING CLOSE DESC 4" },
            new string[] { "BOMBED BUILDING DISTANT DESC 5", "BOMBED BUILDING CLOSE DESC 5" }
        };

        // Descriptions of Scrapyard locations
        public string[][] scrapyardDescs =
        {
            new string[] { "SCRAPYARD DISTANT DESC 1", "SCRAPYARD CLOSE DESC 1" },
            new string[] { "SCRAPYARD DISTANT DESC 2", "SCRAPYARD CLOSE DESC 2" },
            new string[] { "SCRAPYARD DISTANT DESC 3", "SCRAPYARD CLOSE DESC 3" },
            new string[] { "SCRAPYARD DISTANT DESC 4", "SCRAPYARD CLOSE DESC 4" },
            new string[] { "SCRAPYARD DISTANT DESC 5", "SCRAPYARD CLOSE DESC 5" }

        };

        // Descriptions of Ruined Hospital locations
        public string[][] ruinedHospitalDescs =
        {
            new string[] { "RUINED HOSPITAL DISTANT DESC 1", "RUINED HOSPITAL CLOSE DESC 1" },
            new string[] { "RUINED HOSPITAL DISTANT DESC 2", "RUINED HOSPITAL CLOSE DESC 2" },
            new string[] { "RUINED HOSPITAL DISTANT DESC 3", "RUINED HOSPITAL CLOSE DESC 3" },
            new string[] { "RUINED HOSPITAL DISTANT DESC 4", "RUINED HOSPITAL CLOSE DESC 4" },
            new string[] { "RUINED HOSPITAL DISTANT DESC 5", "RUINED HOSPITAL CLOSE DESC 5" }
        };

        // Descriptions of Resistance Base locations
        public string[][] resistanceBaseDescs =
        {
            new string[] { "RESISTANCE BASE DISTANT DESC 1", "RESISTANCE BASE CLOSE DESC 1" },
            new string[] { "RESISTANCE BASE DISTANT DESC 2", "RESISTANCE BASE CLOSE DESC 2" },
            new string[] { "RESISTANCE BASE DISTANT DESC 3", "RESISTANCE BASE CLOSE DESC 3" },
            new string[] { "RESISTANCE BASE DISTANT DESC 4", "RESISTANCE BASE CLOSE DESC 4" },
            new string[] { "RESISTANCE BASE DISTANT DESC 5", "RESISTANCE BASE CLOSE DESC 5" }
        };

        // Descriptions of boss and area of HK-VTOL boss area
        public string[] HKVTOLdesc =
        {
            "HK-VTOL DISTANT DESC",
            "HK-VTOL CLOSE DESC"
        };

        // Descriptions of boss and area of HK-TANK boss area
        public string[] HKTANKdesc =
        {
            "HK-TANK DISTANT DESC",
            "HK-TANK CLOSE DESC"
        };

        // Descriptions of boss and area of Harvester boss area
        public string[] HARVESTERdesc =
        {
            "HARVESTER DISTANT DESC",
            "HARVESTER CLOSE DESC"
        };

        // Descriptions of trap set-up scenarios
        public string[] TrapDescs =
        {
            "TRAP DESC 1",
            "TRAP DESC 2",
            "TRAP DESC 3"
        };

        public string[] rescueLines =
        {
            "YOU SAVED ME 1",
            "YOU SAVED ME 2",
            "YOU SAVED ME 3"
        };

        public string noScout = "THERE IS NOTHING IN THIS DIRECTION";

        public string gameOver = "GAME OVER (TEMPORARY)";
        public string victory = "YOU WIN! (TEMPORARY)";

        // Use for all random selection
        private Random randNum = new Random();
        
        public string GetName() // Return random soldier name
        {
            string[] availNames =
            {
                "NAME 1",
                "NAME 2",
                "NAME 3",
                "NAME 4"
            };

            return availNames[randNum.Next(0, availNames.Length - 1)];
        }
        public string GetSpeech(bool isTerminator) // Return soldier/infiltrator dialogue
        {
            // Return random dialoge, vary suspicious lines according to terminator status
            int chance = randNum.Next(1, 10);
            
            if (isTerminator)
            {
                return randNum.Next(1, 10) > 4 
                    ? normalSpeech[randNum.Next(0, normalSpeech.Length)] 
                    : suspiciousSpeech[randNum.Next(0, suspiciousSpeech.Length)];
            }
            else
            {
                return randNum.Next(1, 10) > 1 
                    ? normalSpeech[randNum.Next(0, normalSpeech.Length)] 
                    : suspiciousSpeech[randNum.Next(0, suspiciousSpeech.Length)];
            }
        }

        public string[] GetDescs(string type)
        {
            switch (type)
            {
                case "Wasteland":
                    return wastelandDescs[randNum.Next(0, wastelandDescs.Length)];
                case "Bombed Building":
                    return bombedBuildingDescs[randNum.Next(0, bombedBuildingDescs.Length)];
                case "Scrapyard":
                    return scrapyardDescs[randNum.Next(0, scrapyardDescs.Length)];
                case "Ruined Hospital":
                    return ruinedHospitalDescs[randNum.Next(0, ruinedHospitalDescs.Length)];
                case "Resistance Base":
                    return resistanceBaseDescs[randNum.Next(0, resistanceBaseDescs.Length)];
                case "HK-VTOL":
                    return HKVTOLdesc;
                case "HK-Tank":
                    return HKTANKdesc;
                case "Harvester":
                    return HARVESTERdesc;
                default:
                    Console.WriteLine($"INVALID LOCATION: {type}");
                    return new string[] { "INVALID LOCATION", "INVALID LOCATION" };
            }
        }
    }
}
