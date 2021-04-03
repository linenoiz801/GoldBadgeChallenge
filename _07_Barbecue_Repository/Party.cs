using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_Barbecue_Repository
{
    public enum BoothType {FoodBooth = 1, TreatBooth };
    public class BoothItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public BoothItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
    public class Booth
    {
        public string BoothName { get; set; }
        public decimal MiscCosts = 0.25m;
        public List<BoothItem> BoothItems { get; set; }
        public List<BoothItem> RedeemedItems { get; set; }

        public Booth()
        {
            BoothItems = new List<BoothItem>();
            RedeemedItems = new List<BoothItem>();
        }
    }
    public class Party
    {
        public string Description { get; set; }
        public DateTime PartyDate { get; set; }
        public Booth FoodBooth = new Booth();
        public Booth TreatBooth = new Booth();
        public int PartyNumber = 0;

        private void FillBooths()
        {
            FoodBooth.BoothName = "Bob's Burger Barn";
            FoodBooth.BoothItems.Add(new BoothItem("Hamburger",2.50m));
            FoodBooth.BoothItems.Add(new BoothItem("Hotdog",1.0m));
            FoodBooth.BoothItems.Add(new BoothItem("Veggie Burger",3.0m));

            TreatBooth.BoothName = "Sally's Sweet Shack";
            TreatBooth.BoothItems.Add(new BoothItem("Ice Cream", 1.5m));
            TreatBooth.BoothItems.Add(new BoothItem("Pop Corn", 0.5m));
            TreatBooth.BoothItems.Add(new BoothItem("Cookie", 1.0m));
        }

        public Party()
        {            
            PartyDate = DateTime.Today;
            FillBooths();
        }
        public Party(string partyDescription)
        {
            Description = partyDescription;
            PartyDate = DateTime.Today;
            FillBooths();
        }
    }
}
