using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace TerminatorSurvivor
{
    class Program
    {
        // Access all available text prompts
        static Text textRef = new Text();

        // For use in all random assignments
        static Random randInt = new Random();

        static bool isJohnAlive = true;
        static int signalsActivated = 0;

        // Get John's initial weapon and armor on game start
        static string weapon = "";
        static string armor = "";

        // Track weapon level for upgrades and future soldier weapon assignment
        static int weaponLevel = 1;

        // Track John's map location
        static int[] johnLocation = { 0, 0 };

        // Controls game continuation after game over
        static bool playAgain = true;
        static bool quitGame = false;

        static int healthPacks = 3; // Max 5 -- assign first two at starting Resistance Base

        // Current party. Assign John Connor to first index.
        static List<Soldier> soldiers = new List<Soldier>(){};

        // Track T800 locations
        static List<int[]> Terminators = new List<int[]>(){};

        // Determines if T-800s are alerted to John Connor's location
        static bool alert = false;
        static int alertTimer = 0;

        // For use in periodic updates from Resistance Bases to inform John of boss locations
        static int notifyTimer = 3;

        static void Main(string[] args)
        {
            while (playAgain)
            {
                playAgain = true;
                isJohnAlive = true;
                soldiers.Clear();
                Terminators.Clear();
                
                string play = intro();
                if (play == "3" || play == "QUIT")
                    break;

                // Prompt to select John's Armor
                string armorChoice = "";
                string[] acceptableArmor = new string[]
                {
                    "1",
                    "KEVLAR",
                    "2",
                    "ABS ANTI-EO",
                    "3",
                    "HEAT DIFFUSION"
                };

                do
                {
                    Console.Clear();
                    Console.WriteLine($"1) Kevlar (anti-projectile, weak to plasma)\n2) ABS Anti-EO (anti-explosive, weak to projectile)\n3) Heat Diffusion (anti-plasma, weak to explosive)\n");
                    Console.Write("SELECT JOHN'S ARMOR: ");
                    armorChoice = Console.ReadLine().ToUpper();
                } while (!acceptableArmor.Contains(armorChoice));

                armor = armorChoice;

                // Prompt to select John's initial Weapon
                string weaponChoice = "";
                string[] acceptableWeapons = new string[]
                {
                    "1",
                    "FN P90 TACTICAL SUBMACHINE GUN",
                    "2",
                    "M203 GRENADE LAUNCHER",
                    "3",
                    "P-40WR PLASMA RIFLE"
                };

                do
                {
                    Console.Clear();
                    Console.WriteLine($"1) FN P90 Tactical Submachine Gun (projectile)\n2) M203 Grenade Launcher (explosive)\n3) P-40WR Plasma Rifle (plasma)\n");
                    Console.Write("SELECT JOHN'S WEAPON: ");
                    weaponChoice = Console.ReadLine().ToUpper();
                } while (!acceptableWeapons.Contains(weaponChoice));

                weapon = weaponChoice;

                // Add John to soldiers, set John to selected specfications
                for (int i = 0; i < 2; i++)
                {
                    soldiers.Add(new Soldier(assignWeapon(1), true));
                }

                soldiers[0].name = "JOHN CONNOR";
                soldiers[0].hp = 3000;

                if (armor == "1" || armor == "KEVLAR")
                {
                    armor = "Kevlar";
                }
                else if (armor == "2" || armor == "ABS ANTI-EO")
                {
                    armor = "ABS Anti-EO";
                }
                else if (armor == "3" || armor == "HEAT DIFFUSION")
                {
                    armor = "Heat Diffusion";
                }

                    if (weapon == "1" || weapon == "FN P90 TACTICAL SUBMACHINE GUN")
                {
                    soldiers[0].weapon = new Weapon("FN P90 Tactical Submachine Gun", "projectile", 200, 100);
                }
                else if (weapon == "2" || weapon == "M203 GRENADE LAUNCHER")
                {
                    soldiers[0].weapon = new Weapon("M203 Grenade Launcher", "explosive", 200, 100);
                }
                else if (weapon == "3" || weapon == "P-40WR PLASMA RIFLE")
                {
                    soldiers[0].weapon = new Weapon("P-40WR Plasma Rifle", "plasma", 200, 100);
                }

                // Establish game map
                Location[,] map = generateMap();
                
                // GAME CYCLE
                while (isJohnAlive && signalsActivated < 3)
                {
                    if (alertTimer > 3)
                        alert = false;
                    
                    if (alert)
                        alertTimer++;

                    string playerChoice = mainUI(map);
                    
                    if (playerChoice == "1" ||  playerChoice == "MOVE")
                    {
                        move(map);
                    }
                    else if (playerChoice == "2" || playerChoice == "SCOUT AHEAD")
                    {
                        scout(map);
                    }
                    else if (playerChoice == "3" || playerChoice == "SURVEY AREA")
                    {
                        surveyArea(map);
                    }
                    else if (playerChoice == "4" || playerChoice == "VIEW MAP")
                    {
                        viewMap(map);
                    }
                    else if (playerChoice == "5" || playerChoice == "PARTY STATUS")
                    {
                        partyStatus();
                    }
                    else if (playerChoice == "6" || playerChoice == "SPEAK TO PARTY MEMBER")
                    {
                        speakToParty(map);
                    }
                    else if (playerChoice == "7" || playerChoice == "USE HEALTH PACK")
                    {
                        useHealthPack();
                    }
                    else if (playerChoice == "8" || playerChoice == "CAMP")
                    {
                        camp(map);
                    }
                }

                Console.Clear();

                // If John Connor has died, failure. If three signals have been activated, Skynet is destroyed, success.
                if (!isJohnAlive)
                {
                    Console.WriteLine(textRef.gameOver);
                }
                else if (signalsActivated == 3)
                {
                    Console.WriteLine(textRef.victory);
                }

                // Prompt to play again.
                string answer = "";

                // If yes, restart game
                while (answer != "Y" && answer != "N" && answer != "YES" && answer != "NO")
                {
                    Console.Write("\nPLAY AGAIN? (Y/N): ");
                    answer = Console.ReadLine().ToUpper();
                }

                // If no, end program
                if (answer == "N" || answer == "NO")
                {
                    playAgain = false;
                }
            }
        }

        // Compare location values to keep T-800s from occupying the same space
        static bool compareLocs(int[] a, int[] b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("One or both of these arrays has more or less than 2 elements. Invalid location.");
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }

        // Game introduction and explanation
        static string intro()
        {
            Console.Clear();
            string selection = "";
            string[] acceptableAnswers = new string[] { "1", "PLAY", "3", "QUIT" };

            do
            {
                Console.Clear();

                Console.WriteLine("████████╗███████╗██████╗ ███╗   ███╗██╗███╗   ██╗ █████╗ ████████╗ ██████╗ ██████╗ \r\n╚══██╔══╝██╔════╝██╔══██╗████╗ ████║██║████╗  ██║██╔══██╗╚══██╔══╝██╔═══██╗██╔══██╗\r\n   ██║   █████╗  ██████╔╝██╔████╔██║██║██╔██╗ ██║███████║   ██║   ██║   ██║██████╔╝\r\n   ██║   ██╔══╝  ██╔══██╗██║╚██╔╝██║██║██║╚██╗██║██╔══██║   ██║   ██║   ██║██╔══██╗\r\n   ██║   ███████╗██║  ██║██║ ╚═╝ ██║██║██║ ╚████║██║  ██║   ██║   ╚██████╔╝██║  ██║\r\n   ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝\r\n                                                                                   \r\n███████╗██╗   ██╗██████╗ ██╗   ██╗██╗██╗   ██╗ ██████╗ ██████╗                     \r\n██╔════╝██║   ██║██╔══██╗██║   ██║██║██║   ██║██╔═══██╗██╔══██╗                    \r\n███████╗██║   ██║██████╔╝██║   ██║██║██║   ██║██║   ██║██████╔╝                    \r\n╚════██║██║   ██║██╔══██╗╚██╗ ██╔╝██║╚██╗ ██╔╝██║   ██║██╔══██╗                    \r\n███████║╚██████╔╝██║  ██║ ╚████╔╝ ██║ ╚████╔╝ ╚██████╔╝██║  ██║                    \r\n╚══════╝ ╚═════╝ ╚═╝  ╚═╝  ╚═══╝  ╚═╝  ╚═══╝   ╚═════╝ ╚═╝  ╚═╝ ");
                Console.WriteLine("\n*************************************************************************************\n");
                Console.WriteLine($"{textRef.introduction}\n");
                Console.Write("1) PLAY\n2) EXPLANATION\n3) QUIT\n\nENTER YOUR CHOICE: ");

                selection = Console.ReadLine().ToUpper();

                if (selection == "2" || selection == "EXPLANATION")
                {
                    string cont;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(textRef.explanation);
                        Console.Write("\nTYPE 'X' TO CONTINUE: ");

                        cont = Console.ReadLine().ToUpper();
                    } while (cont != "X");
                }
            } while (!acceptableAnswers.Contains(selection));

            return selection;
        }

        // Main game menu
        static string mainUI(Location[,] map)
        {      
            string choice = "";
            string[] acceptableAnswers = new string[]
            {
                "1",
                "MOVE",
                "2",
                "SCOUT AHEAD",
                "3",
                "SURVEY AREA",
                "4",
                "VIEW MAP",
                "5",
                "PARTY STATUS",
                "6",
                "SPEAK TO PARTY MEMBER",
                "7",
                "USE HEALTH PACK",
                "8",
                "CAMP"
            };

            do
            {
                Console.Clear();
                
                // Give current area description
                Console.WriteLine(map[johnLocation[0], johnLocation[1]].closeDesc);

                // Notify player of T-800 alert status
                if (alert)
                {
                    Console.WriteLine("\nALERT! T-800 PATROLS KNOW YOUR LOCATION!");
                }              
                
                // Notify player of boss locations every three movements (increased in move() function)
                if (notifyTimer == 3)
                {
                    Console.WriteLine($"\nCOMMUNICATION FROM RESISTANCE BASE:\nSKYNET SIGNAL DETECTED {detectBoss(map)} OF YOUR CURRENT LOCATION."); // UPDATE AFTER ADDING "DETECT BOSS" FUNCTION
                }

                Console.WriteLine("\n*************************************************************************************\n");

                // SIGNALS ACTIVATED
                Console.WriteLine($"SIGNALS ACTIVATED: {signalsActivated}\n");

                // MENU HERE
                Console.WriteLine($"1) MOVE\n2) SCOUT AHEAD\n3) SURVEY AREA\n4) VIEW MAP\n5) PARTY STATUS\n6) SPEAK TO PARTY MEMBER\n7) USE HEALTH PACK ({healthPacks})\n8) CAMP\n");

                // GET STRING TO UPPER
                Console.Write("ENTER YOUR CHOICE: ");
                choice = Console.ReadLine().ToUpper();

            } while (!acceptableAnswers.Contains(choice));
            
            return choice;
        }

        // Generate game map, randomized from available location types
        static Location[,] generateMap()
        {
            List<Location> areas = new List<Location>();

            // Populate areas with 10 Resistance Bases, place John Connor in first
            bool johnPlaced = false;
            for (int i = 0; i < 10; i++)
            {
                bool placed = johnPlaced ? false : true;
                areas.Add(new Location("Resistance Base", false, placed));
                johnPlaced = true;
            }

            // Populate areas with 15 Scrapyards, populate 4 with T-800s
            for (int i = 0; i < 15; i++)
            {
                Location genLoc = new Location("Scrapyard", false, false);
                if (i < 4)
                    genLoc.IsT800 = true;
                areas.Add(genLoc);
            }

            // Populate areas with 15 Ruined Hospitals, populate 4 with T-800s
            for (int i = 0; i < 15; i++)
            {
                Location genLoc = new Location("Ruined Hospital", false, false);
                if (i < 4)
                    genLoc.IsT800 = true;
                areas.Add(genLoc);
            }

            // Populate areas with 10 Bombed Buildings, populate 4 with T-800s
            for (int i = 0; i < 10; i++)
            {
                Location genLoc = new Location("Bombed Building", false, false);
                if (i < 4)
                    genLoc.IsT800 = true;
                areas.Add(genLoc);
            }

            // Populate areas with 47 Wasteland, populate 4 with T-800s
            for (int i = 0; i < 47; i++)
            {
                Location genLoc = new Location("Wasteland", false, false);
                if (i < 4)
                    genLoc.IsT800 = true;
                areas.Add(genLoc);
            }

            // Populate with boss areas
            areas.Add(new Location("HK-VTOL", false, false));
            areas.Add(new Location("HK-Tank", false, false));
            areas.Add(new Location("Harvester", false, false));

            // Generate 10/10 2D array from areas list
            Location[,] locations = new Location[10,10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int choice = randInt.Next(0, areas.Count);
                    locations[i, j] = areas[choice];

                    areas.RemoveAt(choice);
                }
            }

            for (int i = 0; i < locations.GetLength(0); i++)
            {
                for (int j = 0; j < locations.GetLength(1); j++)
                {
                    if (locations[i, j].isT800)
                    {
                        Terminators.Add(new int[] { i, j });
                    }

                    if (locations[i, j].isJohnConnor)
                    {
                        johnLocation = new int[] { i, j };
                    }
                }
            }

            return locations;
        }

        // Display map and map legend
        static void viewMap(Location[,] map)
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map[i, j].isJohnConnor)
                    {
                        Console.Write(" █ ");
                    }
                    else if (map[i, j].hasVisited)
                    {
                        switch (map[i, j].type)
                        {
                            case "Wasteland":
                                Console.Write(" w ");
                                break;
                            case "Bombed Building":
                                Console.Write(" b ");
                                break;
                            case "Scrapyard":
                                Console.Write(" s ");
                                break;
                            case "Ruined Hospital":
                                Console.Write(" h ");
                                break;
                            case "Resistance Base":
                                Console.Write(" # ");
                                break;
                            case "HK-VTOL":
                                Console.Write(" ▼ ");
                                break;
                            case "HK-Tank":
                                Console.Write(" ▼ ");
                                break;
                            case "Harvester":
                                Console.Write(" ▼ ");
                                break;
                            default:
                                Console.Write(" ? ");
                                break;
                        }
                    }
                    else
                    {
                        Console.Write(" - ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write("\n█ - John Connor\nw - Wasteland\nb - Bombed Building\ns - Scrapyard\nh - Ruined Hospital\n# - Resistance Base\n▼ - Boss\n");

            string exit;
            do
            {
                Console.Write("\nTYPE 'X' TO EXIT: ");
                exit = Console.ReadLine();
            } while (exit.ToUpper() != "X");

            Console.Clear();
        }

        // Individual T-800 patrol, directly chasing John Connor upon alert
        static int[] hunt(int[] termLocation, Location[,] map)
        {
            // REFERENCE: johnLocation [#,#]
            
            int locY = termLocation[0];
            int locX = termLocation[1];
            
            // If alert is active, chase John Connor. If not, patrol randomly. Never step into another T-800's space.
            if (alert)
            {
                // If NORTH exists, and John Connor is NORTH of current location, move NORTH
                if (termLocation[0] - 1 >= 0 && johnLocation[0] < termLocation[0])
                {
                    locY--;
                }

                // If SOUTH exists, and John Connor is SOUTH of current location, move SOUTH
                if (termLocation[0] + 1 <= map.GetLength(0) - 1 && johnLocation[0] > termLocation[0])
                {
                    locY++;
                }

                // If WEST exists, and John Connor is WEST of current location, move WEST
                if (termLocation[1] - 1 >= 0 && johnLocation[1] < termLocation[1])
                {
                    locX--;
                }

                // If EAST exists, and John Connor is EAST of current location, move EAST
                if (termLocation[1] + 1 <= map.GetLength(1) - 1 && johnLocation[1] > termLocation[1])
                {
                    locX++;
                }
            }
            else
            {
                // CONTINUE HERE -- RANDOM MOVEMENT
                List<int[]> randLocs = new List<int[]> {};
                
                // CHECK FOR NORTH
                if (termLocation[0] - 1 >= 0)
                {
                    randLocs.Add(new int[] { termLocation[0] - 1, termLocation[1] }); 
                }

                // CHECK FOR SOUTH
                if (termLocation[0] + 1 <= map.GetLength(0) - 1)
                {
                    randLocs.Add(new int[] { termLocation[0] + 1, termLocation[1] });
                }

                // CHECK FOR EAST
                if (termLocation[1] + 1 <= map.GetLength(1) - 1)
                {
                    randLocs.Add(new int[] { termLocation[0], termLocation[1] + 1 });
                }

                // CHECK FOR WEST
                if (termLocation[1] - 1 >= 0)
                {
                    randLocs.Add(new int[] { termLocation[0], termLocation[1] - 1 });
                }

                // CHECK FOR NORTHEAST
                if (termLocation[0] - 1 >= 0 && termLocation[1] + 1 <= map.GetLength(1) - 1)
                {
                    randLocs.Add(new int[] { termLocation[0] - 1, termLocation[1] + 1 });
                }

                // CHECK FOR NORTHWEST
                if (termLocation[0] - 1 >= 0 && termLocation[1] - 1 >= 0)
                {
                    randLocs.Add(new int[] { termLocation[0] - 1, termLocation[1] - 1 });
                }

                // CHECK FOR SOUTHEAST
                if (termLocation[0] + 1 <= map.GetLength(0) - 1 && termLocation[1] + 1 <= map.GetLength(1) - 1)
                {
                    randLocs.Add(new int[] { termLocation[0] + 1, termLocation[1] + 1 });
                }

                // CHECK FOR SOUTHWEST
                if (termLocation[0] + 1 <= map.GetLength(0) - 1 && termLocation[1] - 1 >= 0)
                {
                    randLocs.Add(new int[] { termLocation[0] + 1, termLocation[1] - 1 });
                }

                // SEARCH FOR VALID LOCATION AND ASSIGN, REMOVE INVALID LOCATIONS
                while (true)
                {
                    if (randLocs.Count == 0)
                    {
                        locY = termLocation[0];
                        locX = termLocation[1];
                        break;
                    }
                    
                    int choice = randInt.Next(0, randLocs.Count);

                    if (map[randLocs[choice][0], randLocs[choice][1]].isT800)
                    {
                        randLocs.RemoveAt(choice);
                        continue;
                    }
                    else
                    {
                        locY = randLocs[choice][0];
                        locX = randLocs[choice][1];
                        break;
                    }
                }
            }          

            return new int[] { locY, locX };
        }

        // Collection T-800 patrol
        static void termPatrol(Location[,] map)
        {
            Console.Clear();
            Console.WriteLine("Terminators are hunting you...\n");
            Console.Write("\nTYPE 'X' TO CONTINUE: ");
            string cont;
            do
            {
                cont = Console.ReadLine();
            } while (cont.ToUpper() != "X");

            foreach (Location location in map)
            {
                if (location.isT800)
                    location.IsT800 = false;
            }

            for (int i = 0; i < Terminators.Count; i++)
            {
                int[] newLoc = hunt(Terminators[i], map);
                Terminators[i] = newLoc;
            }

            foreach (int[] terminator in Terminators.ToList())
            {
                if (map[terminator[0], terminator[1]].isTermTrapActive)
                {
                    Console.Clear();

                    Terminators.Remove(terminator);

                    map[terminator[0], terminator[1]].IsT800 = false;
                    map[terminator[0], terminator[1]].isTermTrapActive = false;
                    map[terminator[0], terminator[1]].isTermTrap = false;

                    Console.Write("A T-800 TRIGGERED YOUR TRAP! T-800 DESTROYED!\n\nTYPE 'X' TO CONTINUE: ");
                    string cont2;
                    do
                    {
                        cont2 = Console.ReadLine();
                    } while (cont2.ToUpper() != "X");
                }
                else
                {
                    map[terminator[0], terminator[1]].IsT800 = true;
                }
            }

            // Reassign T-600 placement. Added to improve enemy encounter rate.
            foreach (Location loc in map)
            {
                loc.isT600 = randInt.Next(1, 11) > 6 ? true : false;
            }
        }

        // Turn-based combat
        static void combat(Location[,] map, bool interview)
        {
            // To be flagged if enemy is a T800 so it can be removed from T800 locations list when destroyed
            bool T800 = false;
            
            if (map[johnLocation[0], johnLocation[1]].type == "Resistance Base")
            {
                return;
            }              

            List<Terminator> enemies = new List<Terminator>() { };

            if (interview)
            {
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
            }
            else if (map[johnLocation[0], johnLocation[1]].type == "HK-VTOL")
            {
                // JOHN VS HK-VTOL
                enemies.Add(new Terminator("HK-VTOL", new Weapon("Heavy Plama Turrets", "plasma", 400, 300)));
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
            }
            else if (map[johnLocation[0], johnLocation[1]].type == "HK-TANK")
            {
                // JOHN VS HK-TANK
                enemies.Add(new Terminator("HK-TANK", new Weapon("Rail Cannons", "projectile", 400, 300)));
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
            }
            else if (map[johnLocation[0], johnLocation[1]].type == "Harvester")
            {
                // JOHN VS HARVESTER
                enemies.Add(new Terminator("Harvester", new Weapon("Tracking Rockets", "explosive", 400, 300)));
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
            }
            else if (map[johnLocation[0], johnLocation[1]].isT800)
            {
                // JOHN VS T-800
                T800 = true;
                enemies.Add(new Terminator("T-800", assignWeapon(2)));
                enemies.Add(new Terminator("T-600", assignWeapon(1)));
                alert = true;
            }
            else if (map[johnLocation[0], johnLocation[1]].isT600)
            {
                // JOHN VS T600 PARTY
               int termCount = randInt.Next(2, 4);
                
               for (int i = 0; i < termCount; i++)
               {
                   enemies.Add(new Terminator("T-600", assignWeapon(1)));
               }
            }

            bool isEnemyAlive = true;

            // Combat while both John Connor and enemy party are alive
            while (isJohnAlive && isEnemyAlive)
            {
                if (soldiers[0].hp <= 0)
                {
                    isJohnAlive = false;

                    // PLACE FINAL READOUT HERE
                    finalReadout();

                    break;
                }

                bool terminatorsRemain = false;
                foreach (Terminator terminator in enemies)
                {
                    if (terminator.hp > 0)
                        terminatorsRemain = true;
                }
                if (!terminatorsRemain)
                {
                    // PLACE FINAL READOUT HERE
                    finalReadout();

                    Console.Clear();
                    Console.WriteLine("ENEMY DESTROYED!\n\n");

                    // Chance at receiving a health pack after a battle
                    if (randInt.Next(1, 11) > 5 && healthPacks < 5)
                    {
                        Console.WriteLine("YOU FOUND A HEALTH PACK!\n\n");
                        healthPacks++;
                    }

                    string cont;
                    do
                    {
                        Console.Write("TYPE 'X' TO CONTINUE: ");
                        cont = Console.ReadLine();
                    } while (cont.ToUpper() != "X");

                    if (T800 && !interview)
                    {
                        foreach (int[] terminator in Terminators)
                        {
                            if (compareLocs(terminator, johnLocation))
                            {
                                Terminators.Remove(terminator);
                                break;
                            }
                        }
                    }
                    sweepDeadSoldiers();

                    // If encounter was a boss encounter, activate boss signal and set area type to Wasteland
                    List<string> bossLocations = new List<string>() { "HK-VTOL", "HK-TANK", "Harvester" };

                    if (bossLocations.Contains(map[johnLocation[0], johnLocation[1]].type))
                    {
                        Console.Clear();
                        signalsActivated++;
                        Console.Write($"{map[johnLocation[0], johnLocation[1]].type} SIGNAL HIJACKED! ONE OF THREE NEEDED TO TAKE DOWN SKYNET.\n\nSIGNALS TO GO: {3 - signalsActivated}\n\nTYPE 'X' TO CONTINUE: ");
                        map[johnLocation[0], johnLocation[1]].type = "Wasteland";
                        
                        string[] newDescs = textRef.GetDescs("Wasteland");

                        map[johnLocation[0], johnLocation[1]].distantDesc = newDescs[0];
                        map[johnLocation[0], johnLocation[1]].closeDesc = newDescs[1];

                        string cont2;
                        do
                        {
                            cont2 = Console.ReadLine();
                        } while (cont2.ToUpper() != "X");
                    }

                    break;
                }
                
                string choice = combatMenu();

                if (choice == "1" || choice == "ATTACK")
                {
                    tradeFire();
                }
                else if (choice == "2" || choice == "USE HEALTH PACK")
                {
                    useHealthPack();
                }
                else if (choice == "3" || choice == "RUN")
                {
                    Console.Clear();
                    int runChance = randInt.Next(1, 11);
                    if (runChance > 5)
                    {
                        Console.Write("YOU MANAGED TO ESCAPE!\n\nTYPE 'X' TO CONTINUE: ");
                        sweepDeadSoldiers();
                        string cont;
                        do
                        {
                            cont = Console.ReadLine();
                        } while (cont.ToUpper() != "X");
                        break;
                    }
                    else
                    {
                        Console.Write("YOU COULDN'T ESCAPE!\n\nTYPE 'X' TO CONTINUE: ");
                        string cont;
                        do
                        {
                            cont = Console.ReadLine();
                        } while (cont.ToUpper() != "X");
                        tradeFire();
                    }
                }

                // Main combat interface
                string combatMenu()
                {
                    Console.Clear();
                    Console.WriteLine($"{enemies[0].model} ENCOUNTER!\n-------------------------------------------\n\n");

                    int allyCount = soldiers.Count;
                    int enemyCount = enemies.Count;
                    int printSteps = allyCount > enemyCount ? allyCount : enemyCount;

                    for (int i = 0; i < printSteps; i++)
                    {
                        string allyStatus;
                        string enemyStatus;
                        string allyWeapon;
                        string enemyWeapon;
                        string allyArmor;
                        string enemyArmor;

                        // Width of printed columns
                        const int colSpacing = -50;

                        if (i > allyCount - 1 || soldiers[i].hp <= 0)
                        {
                            allyStatus = $"{"", colSpacing}";
                        }
                        else
                        {
                            allyStatus = $"{$"{soldiers[i].name}" + $" -- HP: {soldiers[i].hp}", colSpacing}";
                        }

                        if (i > enemyCount - 1 || enemies[i].hp <= 0)
                        {
                            enemyStatus = $"{"", colSpacing}";
                        }
                        else
                        {
                            enemyStatus = $"{$"{enemies[i].model}" + $" -- HP: {enemies[i].hp}", colSpacing}";
                        }

                        if (i > allyCount - 1)
                        {
                            allyWeapon = $"{"", colSpacing}";
                        }
                        else if (soldiers[i].hp <= 0)
                        {
                            allyWeapon = $"{"-- TERMINATED --", colSpacing}";
                        }
                        else
                        {
                            allyWeapon = $"{soldiers[i].weapon.name, colSpacing}";
                        }

                        if (i > enemyCount - 1)
                        {
                            enemyWeapon = $"{"",colSpacing}";
                        }
                        else if (enemies[i].hp <= 0)
                        {
                            enemyWeapon = $"{"-- TERMINATED --", colSpacing}";
                        }
                        else
                        {
                            enemyWeapon = $"{enemies[i].weapon.name, colSpacing}";
                        }

                        if (i > allyCount - 1 || soldiers[i].hp <= 0)
                        {
                            allyArmor = $"{"",colSpacing}";
                        }
                        else
                        {
                            allyArmor = $"{$"{soldiers[i].armor}", colSpacing}";
                        }

                        if (i > enemyCount - 1 || enemies[i].hp <= 0)
                        {
                            enemyArmor = $"{"",colSpacing}";
                        }
                        else
                        {
                            enemyArmor = $"{$"{enemies[i].armor}", colSpacing}";
                        }

                        Console.WriteLine($"{allyStatus}{enemyStatus}");
                        Console.WriteLine($"{allyWeapon}{enemyWeapon}");
                        Console.WriteLine($"{allyArmor}{enemyArmor}\n");
                    }

                    Console.WriteLine("\n-------------------------------------------\n");
                    Console.Write($"1) ATTACK\n2) USE HEALTH PACK ({healthPacks})\n3) RUN\n");

                    string answer;
                    string[] acceptable = { "1", "2", "3", "ATTACK", "USE HEALTH PACK", "RUN" };
                    do
                    {
                        Console.Write("\nENTER YOUR ACTION: ");
                        answer = Console.ReadLine();
                        answer = answer.ToUpper();
                    } while (!acceptable.Contains(answer));
                    
                    return answer;
                }

                // Exchange fire with enemies
                void tradeFire()
                {
                    const int colSpacing = -30;

                    Console.Clear();
                    Console.WriteLine("\nFIRING...\n");
                    
                    foreach (Soldier soldier in soldiers)
                    {
                        if (soldier.hp > 0)
                        {
                            List<int> possibleSoldierTargets = new List<int>();
                            for (int i = 0; i < enemies.Count; i++)
                            {
                                if (enemies[i].hp > 0)
                                    possibleSoldierTargets.Add(i);
                            }
                            if (possibleSoldierTargets.Count == 0)
                                return;

                            int soldierTarget = possibleSoldierTargets[randInt.Next(0, possibleSoldierTargets.Count)]; // INDEX OUT OF RANGE EXCEPTION
                            int terminatorDamage = soldier.weapon.Fire(enemies[soldierTarget].armor, map[johnLocation[0], johnLocation[1]].cover, true);
                            if (enemies[soldierTarget].hp - terminatorDamage < 0)
                                terminatorDamage = enemies[soldierTarget].hp;
                            Console.WriteLine($"{$"{soldier.name} ",colSpacing}{$">>>>>>>> {terminatorDamage} >>>>>>>>",colSpacing}{$"{enemies[soldierTarget].model} HP: {enemies[soldierTarget].hp} ==> {enemies[soldierTarget].hp - terminatorDamage}",colSpacing}");
                            enemies[soldierTarget].hp -= terminatorDamage;
                        }
                    }

                    Console.WriteLine("\nRETURN FIRE...\n");

                    foreach (Terminator terminator in enemies)
                    {
                        if (terminator.hp > 0)
                        {
                            List<int> possibleTerminatorTargets = new List<int>();
                            for (int i = 0; i < soldiers.Count; i++)
                            {
                                if (soldiers[i].hp > 0)
                                    possibleTerminatorTargets.Add(i);
                            }
                            if (possibleTerminatorTargets.Count == 0)
                                return;

                            int terminatorTarget = possibleTerminatorTargets[randInt.Next(0, possibleTerminatorTargets.Count)];
                            int soldierDamage = terminator.weapon.Fire(soldiers[terminatorTarget].armor, map[johnLocation[0], johnLocation[1]].cover, false);
                            if (soldiers[terminatorTarget].hp - soldierDamage < 0)
                                soldierDamage = soldiers[terminatorTarget].hp;
                            Console.WriteLine($"{$"{terminator.model} ",colSpacing}{$">>>>>>>> {soldierDamage} >>>>>>>>",colSpacing}{$"{soldiers[terminatorTarget].name} HP: {soldiers[terminatorTarget].hp} ==> {soldiers[terminatorTarget].hp - soldierDamage}",colSpacing}");
                            soldiers[terminatorTarget].hp -= soldierDamage;
                        }
                    }

                    Console.Write("\nTYPE 'X' TO CONTINUE: ");
                    string cont;
                    do
                    {
                        cont = Console.ReadLine();
                    } while (cont.ToUpper() != "X");
                }

                // Display battle aftermath
                void finalReadout()
                {
                    string cont = "";
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"{enemies[0].model} ENCOUNTER!\n-------------------------------------------\n\n");

                        int allyCount = soldiers.Count;
                        int enemyCount = enemies.Count;
                        int printSteps = allyCount > enemyCount ? allyCount : enemyCount;

                        for (int i = 0; i < printSteps; i++)
                        {
                            string allyStatus;
                            string enemyStatus;
                            string allyWeapon;
                            string enemyWeapon;
                            string allyArmor;
                            string enemyArmor;

                            // Width of printed columns
                            const int colSpacing = -50;

                            if (i > allyCount - 1 || soldiers[i].hp <= 0)
                            {
                                allyStatus = $"{"",colSpacing}";
                            }
                            else
                            {
                                allyStatus = $"{$"{soldiers[i].name}" + $" -- HP: {soldiers[i].hp}",colSpacing}";
                            }

                            if (i > enemyCount - 1 || enemies[i].hp <= 0)
                            {
                                enemyStatus = $"{"",colSpacing}";
                            }
                            else
                            {
                                enemyStatus = $"{$"{enemies[i].model}" + $" -- HP: {enemies[i].hp}",colSpacing}";
                            }

                            if (i > allyCount - 1)
                            {
                                allyWeapon = $"{"",colSpacing}";
                            }
                            else if (soldiers[i].hp <= 0)
                            {
                                allyWeapon = $"{"-- TERMINATED --",colSpacing}";
                            }
                            else
                            {
                                allyWeapon = $"{soldiers[i].weapon.name,colSpacing}";
                            }

                            if (i > enemyCount - 1)
                            {
                                enemyWeapon = $"{"",colSpacing}";
                            }
                            else if (enemies[i].hp <= 0)
                            {
                                enemyWeapon = $"{"-- TERMINATED --",colSpacing}";
                            }
                            else
                            {
                                enemyWeapon = $"{enemies[i].weapon.name,colSpacing}";
                            }

                            if (i > allyCount - 1 || soldiers[i].hp <= 0)
                            {
                                allyArmor = $"{"",colSpacing}";
                            }
                            else
                            {
                                allyArmor = $"{$"{soldiers[i].armor}",colSpacing}";
                            }

                            if (i > enemyCount - 1 || enemies[i].hp <= 0)
                            {
                                enemyArmor = $"{"",colSpacing}";
                            }
                            else
                            {
                                enemyArmor = $"{$"{enemies[i].armor}",colSpacing}";
                            }

                            Console.WriteLine($"{allyStatus}{enemyStatus}");
                            Console.WriteLine($"{allyWeapon}{enemyWeapon}");
                            Console.WriteLine($"{allyArmor}{enemyArmor}\n");
                        }

                        Console.Write("TYPE 'X' TO CONTINUE: ");
                        cont = Console.ReadLine().ToUpper();
                    } while (cont != "X");
                }
            }
        }

        // Use Health Pack
        static void useHealthPack()
        {
            if (healthPacks > 0)
            {
                Console.Clear();

                foreach (Soldier soldier in soldiers)
                {
                    if (soldier.hp > 0)
                    {
                        if (soldier.name == "JOHN CONNOR")
                        {
                            soldier.hp = 3000;
                        }
                        else
                        {
                            soldier.hp = 1500;
                        }
                    }
                }

                healthPacks--;
                Console.Write($"PARTY HEALED! HEALTH PACKS REMAINING: {healthPacks}\n\nTYPE 'X' TO CONTINUE: ");

                string cont;
                do
                {
                    cont = Console.ReadLine();
                } while (cont.ToUpper() != "X");
            }
            else
            {
                Console.Clear();
                Console.Write($"OUT OF HEALTH PACKS!\n\nTYPE 'X' TO CONTINUE: ");
                string cont;
                do
                {
                    cont = Console.ReadLine();
                } while (cont.ToUpper() != "X");
            }
        }

        // Display party HP, armor, and weapons
        static void partyStatus()
        {
            string cont = "";
            do
            {
                Console.Clear();
                foreach (Soldier soldier in soldiers)
                {
                    Console.WriteLine($"{soldier.name} -- HP: {soldier.hp}\n{soldier.armor}\n{soldier.weapon.name}\n");
                }

                Console.Write("\nTYPE 'X' TO CONTINUE: ");
                cont = Console.ReadLine().ToUpper();
            } while (cont != "X");
        }

        // Eliminate dead soldiers from party list
        static void sweepDeadSoldiers()
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                if (soldiers[i].hp == 0)
                    soldiers.RemoveAt(i);
            }
        }

        // Assign initial weapon. Modify to add upper-level weapons per Connor level
        static Weapon assignWeapon(int level)
        {
            Weapon[] availWeapons1 =
            {
                new Weapon("FN P90 Tactical Submachine Gun", "projectile", 200, 100),
                new Weapon("M203 Grenade Launcher", "explosive", 200, 100),
                new Weapon("P-40WR Plasma Rifle", "plasma", 200, 100)
            };

            Weapon[] availWeapons2 =
            {
                new Weapon("US M4A1 Battle Rifle", "projectile", 300, 200),
                new Weapon("Milkor MGL", "explosive", 300, 200),
                new Weapon("P-120WR Heavy Plasma Repeater", "plasma", 300, 200)
            };

            Weapon[] availWeapons3 =
            {
                new Weapon("M134-A2 Vulcan Minigun", "projectile", 400, 300),
                new Weapon("RPG-7", "explosive", 400, 300),
                new Weapon("CPA \"Dead Flash\" Plasma Cannon", "plasma", 400, 300)
            };

            Random randNum = new Random();

            switch(level)
            {
                case 1:
                    return availWeapons1[randNum.Next(0, availWeapons1.Length)];
                case 2:
                    return availWeapons2[randNum.Next(0, availWeapons2.Length)];
                case 3:
                    return availWeapons3[randNum.Next(0, availWeapons3.Length)];
                default:
                    return availWeapons1[randNum.Next(0, availWeapons1.Length)];
            }
        }   

        // Speak to individual soldier
        static void speak(Soldier curSoldier, Location[,] map)
        {
            bool cont = true;
            do
            {
                Console.Clear();
                curSoldier.Speak();
                Console.WriteLine("1) TALK\n2) DISMISS\n3) QUIT\n");
                string answer;
                List<string> acceptableanswers = new List<string> { "1", "2", "3", "TALK", "DISMISS", "QUIT" };
                do
                {
                    Console.Write("\nENTER YOUR ACTION: ");
                    answer = Console.ReadLine();
                    answer = answer.ToUpper();
                } while (!acceptableanswers.Contains(answer));

                if (answer == "1" || answer.ToUpper() == "TALK")
                {
                    continue;
                }

                if (answer == "2" || answer.ToUpper() == "DISMISS")
                {
                    if (curSoldier.isTerminator)
                    {
                        Console.Clear();
                        soldiers.Remove(curSoldier);
                        Console.WriteLine($"{curSoldier.name} was a Terminator!");
                        Console.Write("\nTYPE 'X' TO CONTINUE: ");
                        string x;
                        do
                        {
                            x = Console.ReadLine();
                        } while (x.ToUpper() != "X");

                        map[johnLocation[0], johnLocation[1]].IsT800 = true;
                        Terminators.Add(johnLocation);
                        combat(map, true);
                        sweepDeadSoldiers();
                        soldiers.Remove(curSoldier);
                        return;
                    }
                    else
                    {
                        string x;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"{curSoldier.name} has left the party.\n");
                            Console.Write("TYPE 'X' TO CONTINUE: ");
                            x = Console.ReadLine();
                        } while (x.ToUpper() != "X");
                        soldiers.Remove(curSoldier);
                        return;
                    }
                }

                if (answer == "3" || answer.ToUpper() == "QUIT")
                {
                    cont = false;
                    return;
                }

            } while (cont);
        }

        // Soldier party interview user interface
        static void speakToParty(Location[,] map)
        {
            Console.Clear();
            Console.WriteLine("SPEAK WITH WHOM?\n");
            
            for (int i = 1; i < soldiers.Count; i++)
            {
                Console.WriteLine($"{i}) {soldiers[i].name}");
            }
            
            string soldierChoice;
            do
            {
                Console.Write("\nENTER YOUR CHOICE (TYPE 'X' TO EXIT): ");
                soldierChoice = Console.ReadLine();
            } while (!assessValidChoice(soldierChoice));

            // speak() TO SOLDIER SELECTED
            if (int.TryParse(soldierChoice, out int result))
            {
                speak(soldiers[Int32.Parse(soldierChoice)], map);
            }
            else
            {
                return;
            }

            bool assessValidChoice(string choice)
            {
                if (int.TryParse(choice, out int result))
                {
                    int verifiedChoice = Int32.Parse(choice);
                    if (verifiedChoice >= 1 && verifiedChoice < soldiers.Count)
                        return true;
                }
                else
                {
                    if (choice.ToUpper() == "X")
                        return true;
                }
                
                return false;
            }
        }

        // Check for and extract any available resources at John's current location
        static void surveyArea(Location[,] map)
        {
            Console.Clear();
            Console.WriteLine("You search the area...\n");

            bool foundSomething = false;

            if (map[johnLocation[0], johnLocation[1]].isHealthPack && healthPacks < 5)
            {
                foundSomething = true;
                Console.WriteLine("You found a HEALTH PACK!\n");
                healthPacks++;
                map[johnLocation[0], johnLocation[1]].isHealthPack = false;
            }

            if (map[johnLocation[0], johnLocation[1]].isUpgrade && weaponLevel < 3)
            {
                foundSomething = true;
                Console.WriteLine("Among the debris, you find a cache of WEAPONS! All party weapons upgraded!\n");
                foreach (Soldier soldier in soldiers)
                {
                    soldier.weapon.Upgrade(weaponLevel + 1);
                    map[johnLocation[0], johnLocation[1]].isUpgrade = false;
                }
                weaponLevel++;
            }

            if (map[johnLocation[0], johnLocation[1]].isTermTrap)
            {
                foundSomething = true;
                Console.WriteLine(textRef.TrapDescs[randInt.Next(0, textRef.TrapDescs.Length)]);
                map[johnLocation[0], johnLocation[1]].isTermTrapActive = true;
                map[johnLocation[0], johnLocation[1]].isTermTrap = false;
            }

            if (!foundSomething)
            {
                Console.WriteLine("You found no resources here.");
            }

            string cont;
            do
            {
                Console.Write("\n\nTYPE 'X' TO CONTINUE: ");
                cont = Console.ReadLine();
            } while (cont.ToUpper() != "X");

            if (map[johnLocation[0], johnLocation[1]].isSoldier && soldiers.Count < 4 && map[johnLocation[0], johnLocation[1]].type != "Resistance Base")
            {
                Console.Clear();
                soldiers.Add(new Soldier(assignWeapon(weaponLevel), false));
                Console.WriteLine("You've encountered a wayward soldier...\n");
                Console.WriteLine($"{soldiers[soldiers.Count - 1].name}: {textRef.rescueLines[randInt.Next(0, textRef.rescueLines.Length)]}\n");
                map[johnLocation[0], johnLocation[1]].isSoldier = false;

                string cont2;
                do
                {
                    Console.Write("\nTYPE 'X' TO CONTINUE: ");
                    cont2 = Console.ReadLine();
                } while (cont2.ToUpper() != "X");

                speak(soldiers[soldiers.Count - 1], map);
            }
        }

        // Look ahead to surrounding areas, unveil their type on the map
        static void scout(Location[,] map)
        {
            // SCOUT HERE
            string direction;
            do
            {
                Console.Clear();
                Console.WriteLine("LOOKING AHEAD...\n");
                Console.WriteLine("1) NORTH\n2) SOUTH\n3) EAST\n4) WEST\n5) NORTH-EAST\n6) NORTH-WEST\n7) SOUTH-EAST\n8) SOUTH-WEST\n");
                Console.Write("SELECT DIRECTION (TYPE 'X' TO EXIT): ");

                // CONTINUE
                direction = Console.ReadLine().ToUpper();

                Console.Clear();
                if (new string[] { "1", "N", "NORTH" }.Contains(direction))
                {
                    checkDirection(new int[] { -1, 0 }, map);
                }

                if (new string[] { "2", "S", "SOUTH" }.Contains(direction))
                {
                    checkDirection(new int[] { 1, 0 }, map);
                }

                if (new string[] { "3", "E", "EAST" }.Contains(direction))
                {
                    checkDirection(new int[] { 0, 1 }, map);
                }

                if (new string[] { "4", "W", "WEST" }.Contains(direction))
                {
                    checkDirection(new int[] { 0, -1 }, map);
                }

                if (new string[] { "5", "NE", "NORTHEAST", "NORTH EAST", "NORTH-EAST" }.Contains(direction))
                {
                    checkDirection(new int[] { -1, 1 }, map);
                }

                if (new string[] { "6", "NW", "NORTHWEST", "NORTH WEST", "NORTH-WEST" }.Contains(direction))
                {
                    checkDirection(new int[] { -1, -1 }, map);
                }

                if (new string[] { "7", "SE", "SOUTHEAST", "SOUTH WEST", "SOUTH-WEST" }.Contains(direction))
                {
                    checkDirection(new int[] { 1, 1 }, map);
                }

                if (new string[] { "8", "SW", "SOUTHWEST", "SOUTH WEST", "SOUTH-WEST" }.Contains(direction))
                {
                    checkDirection(new int[] { 1, -1 }, map);
                }

            } while (direction != "X");
        }

        // Move John and his party to a nearby location
        static void move(Location[,] map)
        {
            Console.Clear();
            Console.WriteLine("MOVING OUT...\n");
            Console.WriteLine("1) NORTH\n2) SOUTH\n3) EAST\n4) WEST\n5) NORTH-EAST\n6) NORTH-WEST\n7) SOUTH-EAST\n8) SOUTH-WEST\n");

            // CONTINUE
            string direction;
            string[] acceptableAnswers = new string[]
            {
                "1",
                "N",
                "NORTH",
                "2",
                "S",
                "SOUTH",
                "3",
                "E",
                "EAST",
                "4",
                "W",
                "WEST",
                "5",
                "NE",
                "NORTHEAST",
                "NORTH EAST",
                "NORTH-EAST",
                "6",
                "NW",
                "NORTHWEST",
                "NORTH WEST",
                "NORTH-WEST",
                "7",
                "SE",
                "SOUTHEAST",
                "SOUTH EAST",
                "SOUTH-EAST",
                "8",
                "SW",
                "SOUTHWEST",
                "SOUTH WEST",
                "SOUTH-EAST",
                "X"
            };

            do
            {
                Console.Write("SELECT DIRECTION (TYPE 'X' TO EXIT): ");
                direction = Console.ReadLine().ToUpper();
            } while (!acceptableAnswers.Contains(direction));

            // Increase timer to next boss location notification
            notifyTimer++;
            if (notifyTimer > 3)
                notifyTimer = 0;

            Console.Clear();
            if (direction == "X")
                return;
            
            if (new string[] { "1", "N", "NORTH" }.Contains(direction))
            {
                moveDirection(new int[] { -1, 0 }, map);
            }

            if (new string[] { "2", "S", "SOUTH" }.Contains(direction))
            {
                moveDirection(new int[] { 1, 0 }, map);
            }

            if (new string[] { "3", "E", "EAST" }.Contains(direction))
            {
                moveDirection(new int[] { 0, 1 }, map);
            }

            if (new string[] { "4", "W", "WEST" }.Contains(direction))
            {
                moveDirection(new int[] { 0, -1 }, map);
            }

            if (new string[] { "5", "NE", "NORTHEAST", "NORTH EAST", "NORTH-EAST" }.Contains(direction))
            {
                moveDirection(new int[] { -1, 1 }, map);
            }

            if (new string[] { "6", "NW", "NORTHWEST", "NORTH WEST", "NORTH-WEST" }.Contains(direction))
            {
                moveDirection(new int[] { -1, -1 }, map);
            }

            if (new string[] { "7", "SE", "SOUTHEAST", "SOUTH WEST", "SOUTH-WEST" }.Contains(direction))
            {
                moveDirection(new int[] { 1, 1 }, map);
            }

            if (new string[] { "8", "SW", "SOUTHWEST", "SOUTH WEST", "SOUTH-WEST" }.Contains(direction))
            {
                moveDirection(new int[] { 1, -1 }, map);
            }

            // Notify of Resistance Base bonus
            if (map[johnLocation[0], johnLocation[1]].type == "Resistance Base")
            {
                string cont = "";
                do
                {
                    Console.Clear();
                    Console.WriteLine("YOU'VE REACHED A RESISTANCE BASE!");
                    if (soldiers.Count < 4)
                        Console.WriteLine("\nA NEW SOLDIER JOINS YOUR RANKS!");
                    if (healthPacks < 3)
                    {
                        Console.WriteLine("\nACQUIRED HEALTH PACKS!");
                        healthPacks = 3;
                    }
                    if (soldiers.Count < 4)
                        soldiers.Add(new Soldier(assignWeapon(weaponLevel), true));

                    Console.Write("\n\nTYPE 'X' TO CONTINUE: ");
                    cont = Console.ReadLine().ToUpper();
                } while (cont != "X");
            }
        }

        // Verify and describe the location in a specified direction
        static void checkDirection(int[] loc, Location[,] map)
        {
            if (johnLocation[0] + loc[0] < 0 || johnLocation[0] + loc[0] >= map.GetLength(0) || johnLocation[1] + loc[1] < 0 || johnLocation[1] + loc[1] >= map.GetLength(1))
            {
                Console.WriteLine(textRef.noScout);
                Console.Write("\nTYPE 'X' TO CONTINUE: ");
                string cont;
                do
                {
                    cont = Console.ReadLine();
                } while (cont.ToUpper() != "X");
            }
            else
            {
                Console.WriteLine(map[johnLocation[0] + loc[0], johnLocation[1] + loc[1]].distantDesc);
                map[johnLocation[0] + loc[0], johnLocation[1] + loc[1]].hasVisited = true;

                if (map[johnLocation[0] + loc[0], johnLocation[1] + loc[1]].isT600)
                    Console.WriteLine("\nYou see enemy movement. T600s on patrol. You can expect a fight there.");

                if (map[johnLocation[0] + loc[0], johnLocation[1] + loc[1]].isT800)
                    Console.WriteLine("\nYou see enemy movement. A T800 is tracking you. You'd do well to be cautious.");

                string cont;
                do
                {
                    Console.Write("\nTYPE 'X' TO CONTINUE: ");
                    cont = Console.ReadLine();
                } while (cont.ToUpper() != "X");
            }
        }

        // Execute movement for John's party
        static void moveDirection(int[] loc, Location[,] map)
        {
            if (johnLocation[0] + loc[0] < 0 || johnLocation[0] + loc[0] >= map.GetLength(0) || johnLocation[1] + loc[1] < 0 || johnLocation[1] + loc[1] >= map.GetLength(1))
            {
                Console.WriteLine("Cannot move in this direction. You're straying too far from the mission objective.");
                Console.Write("\nTYPE 'X' TO CONTINUE: ");
                string cont;
                do
                {
                    cont = Console.ReadLine();
                } while (cont.ToUpper() != "X");
            }
            else
            {
                map[johnLocation[0], johnLocation[1]].hasVisited = true;
                map[johnLocation[0], johnLocation[1]].isJohnConnor = false;
                johnLocation[0] = johnLocation[0] + loc[0];
                johnLocation[1] = johnLocation[1] + loc[1];
                map[johnLocation[0], johnLocation[1]].hasVisited = true;
                map[johnLocation[0], johnLocation[1]].isJohnConnor = true;

                // Combat encountered T-800s and T-600s
                if (map[johnLocation[0], johnLocation[1]].isT600 || map[johnLocation[0], johnLocation[1]].isT800)
                    combat(map, false);

                // Introduce and combat encountered boss
                if (map[johnLocation[0], johnLocation[1]].type == "HK-VTOL" || map[johnLocation[0], johnLocation[1]].type == "HK-Tank" || map[johnLocation[0], johnLocation[1]].type == "Harvester")
                {
                    string cont = "";
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(map[johnLocation[0], johnLocation[1]].closeDesc);
                        Console.Write("\nTYPE 'X' TO CONTINUE: ");
                        cont = Console.ReadLine().ToUpper();
                    } while (cont != "X");
                    combat(map, false);
                }

                termPatrol(map);
                if (map[johnLocation[0], johnLocation[1]].isT800)
                    combat(map, false);
            }
        }

        // Rest party to restore HP, initiate assassination attempt if party member is covert terminator
        static void camp(Location[,] map)
        {
            Console.Clear();

            if (!isJohnAlive)
            {
                throw new Exception("JOHN IS ALREADY DEAD");
            }

            Console.WriteLine("It's been a hard fight. Best to get some rest while you can.\n");
            bool terminator = false;
            int index = -1;

            for (int i = 0; i < soldiers.Count; i++)
            {
                if (soldiers[i].isTerminator)
                {
                    terminator = true;
                    index = i;
                    break;
                }
            }

            int assassinChance = randInt.Next(1, 11);
            if (terminator && assassinChance > 5 && map[johnLocation[0], johnLocation[1]].type != "Resistance Base")
            {

                Console.WriteLine($"{soldiers[index].name} was a Terminator!\n");
                Console.WriteLine("He attempts to assassinate you in your sleep...\n");

                int successChance = randInt.Next(1, 11);

                if (successChance <= 7)
                {
                    Console.WriteLine("His attempt fails! Defend yourself!\n");
                    Console.Write("TYPE 'X' TO CONTINUE: ");
                    string cont;
                    do
                    {
                        cont = Console.ReadLine();
                    } while (cont.ToUpper() != "X");

                    soldiers.RemoveAt(index);
                    map[johnLocation[0], johnLocation[1]].IsT800 = true;

                    // PROBLEM: COMBAT DOES NOT START CONSISTENTLY
                    combat(map, true);
                }
                else
                {
                    Console.Write("You awake to the feel of a blade slipping between your ribs. You are dead, and the future is doomed.\n\nTYPE 'X' TO CONTINUE: ");
                    string cont;
                    do
                    {
                        cont = Console.ReadLine();
                    } while (cont.ToUpper() != "X");
                    isJohnAlive = false;
                    return;
                }
            }
            else
            {                
                foreach (Soldier soldier in soldiers)
                {
                    if (soldier.name == "JOHN CONNOR")
                    {
                        soldier.hp = 3000;
                    }
                    else
                    {
                        soldier.hp = 1500;
                    }
                }
                
                Console.WriteLine("Party health restored.\n");
                foreach (Soldier soldier in soldiers)
                    Console.WriteLine($"{soldier.name}: {soldier.hp}");
                
                string cont;
                do
                {
                    Console.Write("\nTYPE 'X' TO CONTINUE: ");
                    cont = Console.ReadLine();
                } while (cont.ToUpper() != "X");
                termPatrol(map);
            }
        }

        // Detect boss location, return direction from John position toward boss position
        static string detectBoss(Location[,] map)
        {
            // DETECT BOSS HERE
            List<int> location = new List<int>();
            string[] bossTypes = new string[] { "HK-VTOL", "HK-TANK", "Harvester" };

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (bossTypes.Contains(map[i, j].type)){
                        location.Add(i);
                        location.Add(j);
                        break;
                    }
                    if (location.Count > 0)
                    {
                        break;
                    }
                }
            }

            if (location[0] < johnLocation[0] && location[1] == johnLocation[1])
            {
                return "NORTH";
            }

            if (location[0] > johnLocation[0] && location[1] == johnLocation[1])
            {
                return "SOUTH";
            }

            if (location[0] == johnLocation[0] && location[1] > johnLocation[1])
            {
                return "EAST";
            }

            if (location[0] == johnLocation[0] && location[1] < johnLocation[1])
            {
                return "WEST";
            }

            if (location[0] < johnLocation[0] && location[1] > johnLocation[1])
            {
                return "NORTH-EAST";
            }

            if (location[0] < johnLocation[0] && location[1] < johnLocation[1]){
                return "NORTH-WEST";
            }

            if (location[0] > johnLocation[0] && location[1] > johnLocation[1])
            {
                return "SOUTH-EAST";
            }

            if (location[0] > johnLocation[0] && location[1] < johnLocation[1])
            {
                return "NORTH-WEST";
            }

            return "INVALID LOCATION";
        }
    }
}