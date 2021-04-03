using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _07_Barbecue_Repository;

namespace _07_Barbecue_Console
{
    class ProgramUI
    {        
        PartyRepository repo = new PartyRepository();        
        int _partyNumber = -1;
        readonly int REPORT_WIDTH = 66;
        
        public void Run()
        {
            int selection;
            do
            {
                selection = DoMainMenu();
                switch (selection)
                {
                    case 1:
                        DoNewParty();
                        break;
                    case 2:
                        DoPartyReport(_partyNumber);
                        break;
                    case 3:
                        DoPartyHistory();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Unknown selection. Please try again.");
                        break;
                }
                PressAnyKey();                
            } while (selection != 4);
        }
        private void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }
        private int DoMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Party Tracker");
            Console.WriteLine("Please select an option from the list:");
            Console.WriteLine("1) Start a new Party");
            Console.WriteLine("2) Show Latest Party Analysis");
            Console.WriteLine("3) Show Past Party Summary");
            Console.WriteLine("4) Quit");

            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }
        private void DoPartyHistory()
        {            
            List<int> partyList = repo.GetPartyIDList();
            Console.WriteLine("Party History Report");
            Console.WriteLine($"{partyList.Count} parties loaded in memory.");
            Console.WriteLine(StrUtils.StringOfChar('-', REPORT_WIDTH));

            foreach (int p in partyList)
            {
                DoPartyReport(p);
                Console.WriteLine(StrUtils.StringOfChar('*', REPORT_WIDTH));
            }

        }
        private void DoPartyReport(int partyNumber)
        {
            if (partyNumber == -1)
            {
                Console.WriteLine("No Party information found. Start a Party first.");
            }
            else
            {
                Console.WriteLine($"Party Report for {repo.GetPartyDescription(partyNumber)}");

                DoBoothReport(partyNumber, BoothType.FoodBooth, out int i, out decimal d);
                DoBoothReport(partyNumber, BoothType.TreatBooth, out int i2, out decimal d2);
                
                Console.WriteLine($"{StrUtils.PadRight("Party Totals:", 25)}\t\t{StrUtils.PadRight((i + i2).ToString(), 5)}\t\t{StrUtils.PadRight((d + d2).ToString("C"), 10)}");
            }
        }
        private void DoBoothReport(int partyNumber, BoothType boothType, out int ticketsSold, out decimal totalCost)
        {
            Console.WriteLine(repo.GetBoothName(partyNumber, boothType));
            Console.WriteLine("Product Description\t\tQuantity Sold\t\tTotal Cost");            
            Console.WriteLine(StrUtils.StringOfChar('-',REPORT_WIDTH));

            int tickets;
            decimal cost;
            ticketsSold = 0;
            totalCost = 0.0m;
            List<string> foodItems = repo.GetBoothItems(partyNumber, boothType);
            for (int i = 0; i < foodItems.Count; i++)
            {                
                tickets = repo.GetRedeemedCount(partyNumber, boothType, foodItems[i]);
                ticketsSold += tickets;
                cost = repo.GetRedeemedCost(partyNumber, boothType, foodItems[i]);
                totalCost += cost;
                Console.WriteLine($"{StrUtils.PadLeft(foodItems[i], 25)}\t\t{StrUtils.PadRight(tickets.ToString(), 5)}\t\t{StrUtils.PadRight(cost.ToString("C"),10)}");                
            }
            Console.WriteLine();
            Console.WriteLine($"{StrUtils.PadRight("Totals:",25)}\t\t{StrUtils.PadRight(ticketsSold.ToString(),5)}\t\t{StrUtils.PadRight(totalCost.ToString("C"), 10)}");
            Console.WriteLine();
        }
        private void DoNewParty()
        {
            Console.Clear();
            Console.WriteLine("New Party Menu");
            Console.Write("Enter party description: ");
            string description = Console.ReadLine();
            _partyNumber = repo.AddParty(description);
            int selection; 
            string confirm;
            do
            {
                selection = DoPartyMenu();
                switch (selection)
                {
                    case 1:
                        RedeemTicket(BoothType.FoodBooth);
                        break;
                    case 2:
                        RedeemTicket(BoothType.TreatBooth);
                        break;
                    case 3:
                        DoPartyReport(_partyNumber);
                        break;
                    case 4:
                        Console.WriteLine("Are you sure you want to end the party? (Y/n)");
                        confirm = Console.ReadLine();
                        if ((confirm.Length > 0) && (char.ToLower(confirm[0]) != 'y'))
                        {
                            selection = 0;
                            Console.WriteLine("Party on!");
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown selection. Please try again.");
                        break;
                }
                if (selection != 4)
                    PressAnyKey();
            } while (selection != 4);
        } 

        private int DoPartyMenu()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to {repo.GetPartyDescription(_partyNumber)}! {repo.GetPartyDate(_partyNumber) :MM/dd/yyyy}");
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("1) Redeem Meal Ticket");
            Console.WriteLine("2) Redeem Treat Ticket");
            Console.WriteLine("3) View Party Report");
            Console.WriteLine("4) End Party");

            int.TryParse(Console.ReadLine(), out int result);
            return result;
        }
        private void RedeemTicket(BoothType boothType)
        {
            Console.WriteLine($"Welcome to {repo.GetBoothName(_partyNumber, boothType)}");
            Console.WriteLine("Select item to redeem ticket:");

            List<string> foodItems = repo.GetBoothItems(_partyNumber, boothType);

            int i;
            for (i = 0; i < foodItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {foodItems[i]}");
            }
            Console.WriteLine($"{i + 1}) Cancel Ticket Redemption");
            if (int.TryParse(Console.ReadLine(), out int selection))
            {
                if (selection != i + 1)
                {
                    if (repo.RedeemTicket(_partyNumber, boothType, selection - 1) > 0)
                    {
                        Console.WriteLine($"One (1) ticket redeemed for {foodItems[selection - 1]}");
                    }
                    else
                    {
                        Console.WriteLine("Unknown error redeeming ticket");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Ticket redemption aborted.");
            }
        }
    }
}
