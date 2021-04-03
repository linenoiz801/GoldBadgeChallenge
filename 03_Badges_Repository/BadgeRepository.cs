using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeChallenges
{
    public class BadgeRepository
    {
        private Dictionary<int, Badge> Badges = new Dictionary<int, Badge>();
        
        public void AddBadge(int ID, List<string> doors)
        {
            if (Badges.ContainsKey(ID))
            {
                UpdateBadge(ID, doors);
            }
            else
            {
                Badges.Add(ID, new Badge(ID, doors));
            }
        }
        public List<string> GetDoors(int ID)
        {
            if (Badges.ContainsKey(ID))
            {
                return Badges[ID].DoorNames;
            }
            else
            {
                return new List<string>();
            }
        }
        public void UpdateBadge(int ID, List<string> doors)
        {
            if (Badges.ContainsKey(ID))
            {
                Badges[ID].DoorNames = doors;
            }
        }
        public void DeleteBadge(int ID)
        {
            if (Badges.ContainsKey(ID))
            {
                Badges.Remove(ID);
            }
        }
        public int GetBadgeCount()
        {
            return Badges.Count;
        }
        public List<int> GetBadgeList()
        {
            List<int> result = new List<int>();

            foreach (KeyValuePair<int, Badge> badge in Badges)
            {
                result.Add(badge.Key);
            }
            return result;
        }
        public string GetDoorList(int ID, string Separator)
        {
            string result = "";
            if (Badges.ContainsKey(ID))
            {
                List<string> doors = GetDoors(ID);
                string doorStr = "";
                foreach (string str in doors)
                {
                    doorStr += str + Separator;
                }
                if (doorStr.Length - Separator.Length > 0)
                {
                    result = doorStr.Remove(doorStr.Length - Separator.Length);
                }
            }
            return result;
        }
        public bool BadgeExists(int ID)
        {
            return Badges.ContainsKey(ID);
        }
    }
}
