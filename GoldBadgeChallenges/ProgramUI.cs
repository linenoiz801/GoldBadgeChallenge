using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeChallenges
{
    class ProgramUI
    {
        BadgeRepository repo = new BadgeRepository();
        public void Run()
        {
            int i = Menu();
            while (i >= 0)
            {
                switch (i)
                {
                    case 1:
                        AddBadge();
                        break;
                    case 2:
                        EditBadge();
                        break;
                    case 3:
                        DisplayBadges();
                        break;
                    case 4:
                        i = -1;
                        break;
                    default:
                        Console.WriteLine("I'm afraid I can't do that.");
                        Console.ReadLine();
                        break;
                }
                if (i >= 0)
                    i = Menu();
            }
        }
        private void AddBadge()
        {
            Console.WriteLine("Adding new Badge...");
            Console.Write("Enter Badge Number: ");
            if (int.TryParse(Console.ReadLine(),out int badgeNumber))
            {
                if (repo.BadgeExists(badgeNumber))
                {
                    Console.WriteLine("Error adding badge: badge already exists.");
                    Console.ReadLine();
                }
                else
                {
                    List<string> doors = new List<string>();
                    do
                    {
                        Console.Write("Enter a Door ID for access: ");
                        doors.Add(Console.ReadLine());
                        Console.WriteLine("Any more doors (y/n)? ");
                    } while (Console.ReadLine() == "y");
                    repo.AddBadge(badgeNumber, doors);
                }
            }
        }
        private void EditBadge()
        {
            Console.WriteLine("Editing existing Badge...");
            Console.Write("Enter Badge Number: ");
            if ((int.TryParse(Console.ReadLine(), out int badgeNumber)) &&
                (repo.BadgeExists(badgeNumber)))
            {
                List<string> doors;
                int selection;
                bool keepGoing = true;
                string curDoor;
                do
                {
                    doors = repo.GetDoors(badgeNumber);
                    Console.WriteLine($"{badgeNumber} has access to door(s) {repo.GetDoorList(badgeNumber," & ")}.");
                    Console.WriteLine("What would you like to do?");
                    Console.WriteLine("  1) Remove a door");
                    Console.WriteLine("  2) Add a door");
                    Console.WriteLine("  3) Go Back");
                    if (int.TryParse(Console.ReadLine(),out selection))
                    {
                        switch (selection)
                        {
                            case 1:
                                Console.Write("Enter door to remove: ");
                                curDoor = Console.ReadLine();
                                if (doors.Contains(curDoor))
                                {
                                    doors.Remove(curDoor);
                                    repo.UpdateBadge(badgeNumber, doors);
                                    Console.WriteLine("Door removed.");
                                }
                                else
                                {
                                    Console.WriteLine("Door not found.");
                                }
                                Console.ReadLine();
                                break;
                            case 2:
                                Console.Write("Enter a Door ID to add: ");
                                doors.Add(Console.ReadLine());
                                Console.WriteLine("Door added.");
                                Console.ReadLine();
                                break;
                            default:
                                keepGoing = false;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("I'm afraid I can't do that.");
                    }
                    
                } while (keepGoing);
            } 
            else if (!repo.BadgeExists(badgeNumber))
            {
                Console.WriteLine("Badge number not found.");
                Console.ReadLine();
            }
        }
        private void DisplayBadges()
        {
            Console.WriteLine("Displaying all badges...");
            Console.WriteLine("Badge #\t\tDoor Access");            
            string buildStr;
            List<string> doors;
            foreach (int badgeID in repo.GetBadgeList())
            {
                doors = repo.GetDoors(badgeID);
                buildStr = "";
                foreach (string curStr in doors)
                {
                    buildStr += curStr + ',';
                }
                buildStr = buildStr.Remove(buildStr.Length - 1);

                Console.WriteLine($"{badgeID}\t\t{repo.GetDoorList(badgeID,",")}");        
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private int Menu()
        {
            Console.Clear();
            Console.WriteLine("Hello Security Admin. What would you like to do?");
            Console.WriteLine("  1) Add a Badge");
            Console.WriteLine("  2) Edit an existing Badge");
            Console.WriteLine("  3) List all existing Badges");
            Console.WriteLine("  4) Quit");
            if (int.TryParse(Console.ReadLine(), out int selection))
                return selection;
            else
                return 0;
        }
    }
}
