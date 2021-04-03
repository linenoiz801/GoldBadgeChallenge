using _07_Barbecue_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace _07_Barbecue_Tests
{
    [TestClass]
    public class PartyRepository_Tests
    {
        private PartyRepository repo;
        private int _partyNumber;
        [TestInitialize]
        public void Arrange()
        {
            repo = new PartyRepository();
            _partyNumber = repo.AddParty("Test Party");
        }

        [TestMethod]
        public void Test_AddParty()
        {
            //Arragne
            int startCount = repo.GetPartyCount();

            //Act
            int partyNumber = repo.AddParty("Test Party 2");
            int endCount = repo.GetPartyCount();

            //Assert
            Assert.AreEqual(endCount, partyNumber);
            Assert.AreNotEqual(startCount, endCount);
        }
        [TestMethod]
        public void Test_GetPartyDate()
        {
            //Arrange

            //Act
            DateTime partyDate = repo.GetPartyDate(_partyNumber);

            //Assert
            Assert.AreEqual(partyDate, DateTime.Today);
        }
        [TestMethod]
        public void Test_GetPartyDescription()
        {
            //Arrange

            //Act
            String partyDescription = repo.GetPartyDescription(_partyNumber);

            //Assert
            Assert.AreEqual(partyDescription, "Test Party");
        }
        [TestMethod]
        public void Test_GetBoothName()
        {
            //Arrange

            //Act
            String foodBoothName = repo.GetBoothName(_partyNumber, BoothType.FoodBooth);
            String treatBoothName = repo.GetBoothName(_partyNumber, BoothType.TreatBooth);

            //Assert
            Assert.AreEqual(foodBoothName, "Bob's Burger Barn");
            Assert.AreEqual(treatBoothName, "Sally's Sweet Shack");
        }
        [TestMethod]
        public void Test_GetBoothItems()
        {
            //Arrange

            //Act
            List<String> foodBoothList = repo.GetBoothItems(_partyNumber, BoothType.FoodBooth);
            List<String> treatBoothList = repo.GetBoothItems(_partyNumber, BoothType.TreatBooth);

            //Assert
            Assert.AreEqual(foodBoothList.Count, 3);
            Assert.AreEqual(treatBoothList.Count, 3);
        }
        [TestMethod]
        public void Test_RedeemTicket()
        {
            //Arrange
            int startFoodCount = repo.GetTicketCount(_partyNumber, BoothType.FoodBooth);
            int startTreatCount = repo.GetTicketCount(_partyNumber, BoothType.TreatBooth);

            //Act
            repo.RedeemTicket(_partyNumber, BoothType.FoodBooth, 0);
            repo.RedeemTicket(_partyNumber, BoothType.TreatBooth, 0);

            int endFoodCount = repo.GetTicketCount(_partyNumber, BoothType.FoodBooth);
            int endTreatCount = repo.GetTicketCount(_partyNumber, BoothType.TreatBooth);

            //Assert
            Assert.AreNotEqual(startFoodCount, endFoodCount);
            Assert.AreNotEqual(startTreatCount, endTreatCount);
        }
        [TestMethod]
        public void Test_GetTicketCount()
        {
            //Arrange
            int startFoodCount = repo.GetTicketCount(_partyNumber, BoothType.FoodBooth);
            int startTreatCount = repo.GetTicketCount(_partyNumber, BoothType.TreatBooth);

            //Act
            repo.RedeemTicket(_partyNumber, BoothType.FoodBooth, 0);
            repo.RedeemTicket(_partyNumber, BoothType.TreatBooth, 0);

            int endFoodCount = repo.GetTicketCount(_partyNumber, BoothType.FoodBooth);
            int endTreatCount = repo.GetTicketCount(_partyNumber, BoothType.TreatBooth);

            //Assert
            Assert.AreEqual(startFoodCount, 0);
            Assert.AreEqual(startTreatCount, 0);
            Assert.AreEqual(endFoodCount, 1);
            Assert.AreEqual(endTreatCount, 1);
        }
        [TestMethod]
        public void Test_GetRedeemedCount()
        {
            //Arrange
            int startFoodCount = repo.GetRedeemedCount(_partyNumber, BoothType.FoodBooth, "Hamburger");
            int startTreatCount = repo.GetRedeemedCount(_partyNumber, BoothType.TreatBooth, "Ice Cream");

            //Act
            repo.RedeemTicket(_partyNumber, BoothType.FoodBooth, 0);
            repo.RedeemTicket(_partyNumber, BoothType.TreatBooth, 0);

            int endFoodCount = repo.GetRedeemedCount(_partyNumber, BoothType.FoodBooth, "Hamburger");
            int endTreatCount = repo.GetRedeemedCount(_partyNumber, BoothType.TreatBooth, "Ice Cream");

            //Assert
            Assert.AreEqual(startFoodCount, 0);
            Assert.AreEqual(startTreatCount, 0);
            Assert.AreEqual(endFoodCount, 1);
            Assert.AreEqual(endTreatCount, 1);
        }
        [TestMethod]
        public void Test_GetRedeemedCost()
        {
            //Arrange

            //Act
            repo.RedeemTicket(_partyNumber, BoothType.FoodBooth, 0);
            repo.RedeemTicket(_partyNumber, BoothType.TreatBooth, 0);

            decimal foodCost = repo.GetRedeemedCost(_partyNumber, BoothType.FoodBooth, "Hamburger");
            decimal treatCost = repo.GetRedeemedCost(_partyNumber, BoothType.TreatBooth, "Ice Cream");

            //Assert
            Assert.AreEqual(foodCost, 2.75m); //$2.50 item cost + $0.25 miscellaneous expense
            Assert.AreEqual(treatCost, 1.75m); //$1.50 item cost + $0.25 miscellaneous expense
        }
        [TestMethod]
        public void Test_GetPartyCount()
        {
            //Arrange
            int startPartyCount = repo.GetPartyCount();

            //Act
            repo.AddParty("Test Party 2");

            int endPartyCount = repo.GetPartyCount();

            //Assert
            Assert.AreEqual(startPartyCount, 1);
            Assert.AreEqual(endPartyCount, 2);
        }
        [TestMethod]
        public void Test_GetPartyIDList()
        {
            //Arrange
            List<int> startList = repo.GetPartyIDList();

            //Act
            repo.AddParty("Test Party 2");

            List<int> endList = repo.GetPartyIDList();

            //Assert
            Assert.AreNotEqual(startList.Count, endList.Count);
            Assert.AreEqual(startList.Count, 1);
            Assert.AreEqual(endList.Count, 2);            
        }
    }
}
