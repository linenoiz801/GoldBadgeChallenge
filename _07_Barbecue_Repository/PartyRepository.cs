using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Barbecue_Repository
{
    public class PartyRepository
    {
        readonly Dictionary<int, Party> Repository = new Dictionary<int, Party>();

        public int AddParty(string partyDescription)
        {
            int result = Repository.Count + 1;
            Repository.Add(result, new Party(partyDescription));
            Repository[result].PartyNumber = result;
            return result;
        }
        public DateTime GetPartyDate(int partyNumber)
        {
            if (Repository.ContainsKey(partyNumber))
            {
                return Repository[partyNumber].PartyDate;
            }
            else
            {
                return DateTime.Now;
            }
        }
        public string GetPartyDescription(int partyNumber)
        {
            if (Repository.ContainsKey(partyNumber))
            {
                return Repository[partyNumber].Description;
            }
            else
            {
                return "<invalid party number>";
            }
        }
        public string GetBoothName(int partyNumber, BoothType boothType)
        {
            if (Repository.ContainsKey(partyNumber))
            {
                if (boothType == BoothType.FoodBooth)
                    return Repository[partyNumber].FoodBooth.BoothName;
                else
                    return Repository[partyNumber].TreatBooth.BoothName;
            }
            else
                return "<Invalid party number>";
        }
        public List<string> GetBoothItems(int partyNumber, BoothType boothType)
        {
            List<string> result = new List<string>();
            if (Repository.ContainsKey(partyNumber))
            {
                if (boothType == BoothType.FoodBooth)
                {
                    foreach (BoothItem item in Repository[partyNumber].FoodBooth.BoothItems)
                    {
                        result.Add(item.Name);
                    }
                }                    
                else
                {
                    foreach (BoothItem item in Repository[partyNumber].TreatBooth.BoothItems)
                    {
                        result.Add(item.Name);
                    }
                }
            }
            return result;
        }
        public int RedeemTicket(int partyNumber, BoothType boothType, int foodItem)
        {
            Booth booth;
            if (Repository.ContainsKey(partyNumber))
            {
                if (boothType == BoothType.FoodBooth)
                    booth = Repository[partyNumber].FoodBooth;
                else
                    booth = Repository[partyNumber].TreatBooth;

                if (foodItem <= booth.BoothItems.Count)
                    booth.RedeemedItems.Add(booth.BoothItems[foodItem]);
            }
            return GetTicketCount(partyNumber,boothType);
        }
        public int GetTicketCount(int partyNumber, BoothType boothType)
        {
            if (Repository.ContainsKey(partyNumber))
            {
                if (boothType == BoothType.FoodBooth)
                    return Repository[partyNumber].FoodBooth.RedeemedItems.Count;
                else
                    return Repository[partyNumber].TreatBooth.RedeemedItems.Count;
            }
            else
                return -1;
        }
        public int GetRedeemedCount(int partyNumber, BoothType boothType, string foodItem)
        {
            int result = 0;
            if (Repository.ContainsKey(partyNumber))
            {
                Booth booth;
                if (boothType == BoothType.FoodBooth)
                    booth = Repository[partyNumber].FoodBooth;
                else
                    booth = Repository[partyNumber].TreatBooth;

                for (int i = 0; i < booth.RedeemedItems.Count; i++)
                {
                    if (booth.RedeemedItems[i].Name == foodItem)
                        result++;                    
                }                
            }
            return result;
        }
        public decimal GetRedeemedCost(int partyNumber, BoothType boothType, string foodItem)
        {
            decimal result = 0;
            if (Repository.ContainsKey(partyNumber))
            {
                Booth booth;
                if (boothType == BoothType.FoodBooth)
                    booth = Repository[partyNumber].FoodBooth;
                else
                    booth = Repository[partyNumber].TreatBooth;

                for (int i = 0; i < booth.RedeemedItems.Count; i++)
                {
                    if (booth.RedeemedItems[i].Name == foodItem)
                        result += booth.RedeemedItems[i].Price + booth.MiscCosts;
                }
            }
            return result;
        }
        public int GetPartyCount()
        {
            return Repository.Count;
        }
        public List<int> GetPartyIDList()
        {
            List<int> result = new List<int>();
            foreach (KeyValuePair<int,Party> p in Repository)
            {
                result.Add(p.Key);
            }
            return result;
        }
    }
}
