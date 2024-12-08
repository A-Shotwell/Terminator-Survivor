using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// NOTE FOR FUTURE ENTRIES: All paragraphs must end with "@split" for printLineLimit() to function correctly.
// Be sure that there are no empty spaces before or after "@split", as this can cause an indexing error.

namespace TerminatorSurvivor
{
    internal class Text
    {
        // Game introduction text
        public string introduction = "\tWelcome to Terminator Survivor, a text-based survival game based on the Terminator movie franchise. Take control of John Connor, humanity's last hope against Skynet's marauding machines. Lead a team of soldiers in search of three Skynet communication signals. Hijack those signals, bring down Skynet, and save humanity.@split\n";

        // Game tutorial text
        public string explanation = "\r\rTUTORIAL@split\r-----------------@split\n\r\r\tTo begin, select John Connor's initial armor and weapon. This game uses a strength/weakness weapon and armor system. Each soldier or terminator armor will receive a bonus, penalty, or neutrality according to these attributes:@split\r\n\rKevlar/Steel >>> Strong against \"projectile\", weak against \"plasma\"\n@split\r\rABS Anti-EO/Polycarbonate >>> Strong against \"explosive\", weak against \"projectile\"@split\r\r\nHeat Diffusion/Coltan Alloy >>> Strong against \"plasma\", weak against \"explosive\"@split\r\r\n\tThis game takes place on a 10x10 grid of varyious location types. These location types provide varying cover bonuses for your party, along with chances to find certain resources. These resources include weapon upgrades, health packs, opportunities to set traps for pursuing enemies, and soldiers that can be recruited to your party. John Connor will start with one ally and can have a party of up to four soldiers, including himself.@split\r\r\n\tYou will encounter three types of enemies in this game. T-600 terminators, which spawn randomly. T-800s, which patrol or actively pursue John Connor, depending on alert status. Encountering a T-800 alerts all other T-800s of your location for three turns. And finally, bosses, which are immobile on the game grid. There are three bosses in this game, each safeguarding a Skynet communication signal. Find and hijack these signals to win the game.@split\r\rThe game's main interface includes a description of your current location, periodic updates from the Resistance about a signal's current location, your current number of activated signals, and the main actions menu. \n\nYour main menu options are as follows:\n@split\r\r– MOVE –\n@split\r\tThis option allows you to move John Connor's party in any compass direction on the game grid, so long as a grid space exists in that direction. If no space exists, you will be notified that you are attempting to move outside the game grid and prompted to select another direction. Take caution in your movement; landing on a space occupied by a terminator squad or a boss will initiate combat. T-800s will also patrol or pursue following this action. If a T-800 lands on your space, this will also initiate combat.@split\n\r\r– SCOUT AHEAD –\n@split\r\tThis option allows you to look ahead to any neighboring game grid space without moving and without triggering combat or terminator patrol. This will give you a brief description of what John Connor sees in your selected direction and inform you of any enemy presence. It will also mark the searched space with its type on the game map. It would be prudent to check surrounding areas and plan your next movement accordingly.@split\n\r\r– SURVEY AREA –\n@split\r\tThis option allows you to check the area for resources, soldiers, and opportunities to set traps. Any T-800 that encounters a set trap will be destroyed on contact. Any soldier encountered on the battlefield is a potential T-800 infiltrator unit. Encountering a soldier will automatically add him to your party, but offer an immediate chance to speak. Here, you can interview the soldier and either keep him or dismiss him. If a T-800 infiltrator is dismissed, it will initiate combat. This option will not trigger terminator patrol/pursuit.@split\n\r\r– VIEW MAP –\n@split\r\tThis option will display a graphic representation of the game map, including your current location, a legend, and the marked area types of any area that has either been visited or scouted. This option will not trigger terminator patrol/pursuit.@split\n\r\r– PARTY STATUS –\n@split\r\tThis option will display the current status of all party members, including current remaining HP, armor type, and current weapon. This option will not trigger terminator patrol/pursuit.@split\n\r\r– SPEAK TO PARTY MEMBER –\n@split\r\tThis option will allow you to speak to a selected party member. From here, the party member can be dismissed from your party if he arouses suspicion. Dismissing a T-800 infiltrator will initiate combat. This option will not trigger terminator patrol/pursuit.@split\n\r\r– USE HEALTH PACK –\n@split\r\tThis option will allow you to use one of your health packs, if available. This will fully restore your party's HP. This option will not trigger terminator patrol/pursuit.@split\n\r\r– CAMP –\n@split\r\tThis option will allow you to set up camp and rest your party. This will fully recover your party's HP, but beware. Only select this option if you trust your current party. If you are harboring a T-800 infiltrator, there is a chance that it will attempt to assassinate you in your sleep. A successful attempt results in game over. An unsuccessful attempt triggers combat, and your party HP will not be restored. Selecting this option will trigger terminator patrol/pursuit.\n\n\tGood luck, and good hunting!";

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
            "Suspicious Speech Test 5\n",
        };

        // Descriptions of Wasteland locations
        public string[][] wastelandDescs =
        {
            new string[] { "You see a relatively small, fenced-off area at the top of a low hill, flanked by a couple of low structures. The place is veiled in smoke, visibility is low. You hear the faint, somewhat nostalgic jangling of chains. Swing sets?", "You arrive at the smoldering ruins of what was once a children’s playground. Swing set chains and rubber seats scorched black. A warped and twisted slide. A busted, broken merry-go-round. A few melted spring riders that used to be animals, or maybe vehicles, and brittle patches of glass that used to be sand.\n@splitThis fire was recent, whatever started it. But the damage here is older. Sadder. You shudder to think of the young lives that were lost here on Judgment Day. You can only hope it was quick. The only comforting thought is that they didn’t live to grow up in this nightmare.\n@splitThere’s nothing of value to be found here, only haunting memories of what was lost. It would be best to move on." },
            new string[] { "You see a trail of toppled and splintered trees cutting through some nearby woods. Among them, you see scraps of metal and a few shards of broken glass. A large panel of painted metal here. The twisted and rended remains of an empty seat there.", "Following a trail of broken trees and metal scraps, you arrive at the site of an old plane crash. One wing entirely missing, an engine reduced to scrap. The cabin, torn in half and exposed to the elements, seating the skeletal remains of its passengers.\n@splitA commercial airliner with the intact engine riddled with bullet holes. Taken down by Skynet’s UAVs in the early days after Judgment Day, before the resistance, by the look of it.\n@splitThe plane was stripped of components and cargo decades ago. You’ll find nothing of use here." },
            new string[] { "You see an uninterrupted horizon and a stretch of seldom-travelled road. Nothing in particular stands out as worth exploring. This is a place for passing through, nothing more.", "Following the road for hours, the only landmark you find is an abandoned gas station. The shelves were raided long ago, and the tanks pumped dry. The building may provide shelter for a night, but otherwise, there’s nothing to be found here. It would be best to move on." },
            new string[] { "You see an expanse of what was once a grassy field with a somewhat dirty stream leading to a small, strangely undisturbed set of park structures surrounding a low statue.", "You arrive at the centeral garden area of a neighborhood park. Largely undamaged except for what was lost to time. Dead rose bushes, a small pond that likely once held fish before the birds got to them. A set of shaded benches, and a band stand, all still standing and whole.\n@splitThe sole exception is the broken statue at the center, half of it fallen to a pile of shaped rubble with a missing inscription plate. This place meant something once, a memorial of some kind. Now, it’s just another part of the wasteland.\n@splitThere is nothing of value to be found here." },
            new string[] { "You see a number of vehicles overturned, toppled, and piled against one another. Some trailers and RVs, each blown to the same side.", "Pushing past a few overturned vehicles, you arrive at a mass of toppled trailers. A few are still standing, most crushed into mangled heaps. This was a trailer park before the blast wave cleared the lot and clumped them together.\n@splitYou may find shelter here, but little else. Anything useful would be trapped within or beneath the wreckage, unreachable." },
            new string[] { "You see a ruined field pocked with large craters, lined with deep trenches and barricades. The air is still and silent.", "You arrive at an abandoned battlefield. Trenches littered with corpses, bones and skulls, but little enemy wreckage to be found. Battle after battle has been lost here over the years. It seems that, at some point, the territory became no longer worth fighting for. You’re grateful the forces that crushed us here are no longer around, but you suspect regular patrols still run through here.\n@splitThere is nothing of use to be found here, and you’re too exposed. It would be wise to move on." },
            new string[] { "You see tall buildings in various states of disrepair, some utterly destroyed but still standing. The streets are drowned and flow like rivers.", "You arrive at a flooded city block, trudging through water up to your knees. Cars and debris swept down the city streets by the flow. There has been no rain, and there is no body of water nearby. Evidently, a water main burst and left the area in this sorry state.\n@splitThe machines would have difficulty traversing this mess, but likely not as much as you. You should move on as quickly as you are able." },
            new string[] { "You see rows of wrecked suburban homes, many blasted to splinters, few standing at all. Lawns reduced to ash like salted earth.", "You arrive at what was once a peaceful, suburban neighborhood. Now, it’s a graveyard. Shattered homes littering the streets with debris. Shadows burnt into the sides of houses, ghosts of people tending their lawns and gardens, and children at play. Mass death in an instant.\n@splitWhatever salvage was left here was scavenged years ago. These homes wouldn’t even provide adequate shelter. It would be best to move on." },
            new string[] { "You see the beams of a half-finished building, and the structure is failing. It’s a wonder it survived the blast at all.", "You arrive at an abandoned construction site. The machines are damaged and rickety. The concrete and foundation are crumbling, and there are no corpses of workers to be found. It would appear that they ran for their lives, though they couldn’t have gotten far.\n@splitThe structure is barely standing. It looks like it could fail at any moment. It would be best to not be here when it does." },
            new string[] { "You see a row of shops and a mostly empty parking lot. A few fallen lamp posts, cars scattered in the blast, and what was clearly a hail of fallen debris.", "You arrive at a ruined strip mall. Collapsing roofs, some toppled walls. Windows blasted out, some with cars blown through them. Most of these shops were empty in the first place, a strip mall mostly going out of business. Not much was lost here. It’s one of the few structures that actually looks at home in the wasteland.\n@splitWhat’s left of the stores has already been looted. There’s nothing of value to be found here." },
            new string[] { "In a clearing, through the trees, you see several headstones standing out of a patch of overgrown grass, partially concealed by a veil of fog. You hear nothing. Silence, but for a few crows cawing in the distance.", "You arrive at an abandoned graveyard. Many damaged headstones. Few legible. There was no decisive battle here, but there was a small skirmish. One that we evidently won.\n@splitYou see two fallen T-600s reduced to unusable scrap, one of which is somehow decapitated. You find the missing head deliberately placed atop one of the graves at the foot of its broken headstone. Clearly, this gesture meant something to someone, and they thought it would mean something to whoever is interred here.\n@splitYou've seen many shadows of vendetta like this since the war began. Somber as it is, it at least inspires hope that the will to fight lives on.\n@splitThere are no resources to be found here. It would be best to move on and leave this place undisturbed any more than it has already endured." },
            new string[] { "You see a burnt and cratered field covered in scattered rubble. Corpses here and there, no fallen Skynet machines.", "You arrive at the site of an unknown building, shelled to oblivion. You don’t see a single piece of rubble large enough that you couldn’t carry it with your own hands. Utter decimation. Whatever was here was obviously important at one point. But Skynet, apparently, didn’t find it necessary to lose any forces trying to capture it. Its only goal seems to have been destruction.\n@splitWhatever might have been useful here isn’t here anymore. You should move on." },
            new string[] { "You see a mostly empty rail yard. A few abandoned box cars and an engine in disrepair, but mostly empty tracks.", "You arrive at an abandoned rail yard. There are many just like this in the wasteland. Our supply lines were among the first of Skynet’s targets, and the machines have long since appropriated the railways for their own purposes. We’ve taken a few back at great cost, and defend them at even greater cost to our resources, but the war can’t be won without them. We’ll defend them to the last man.\n@splitThere is nothing else to find here." }
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
                case "HK-TANK":
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
