using GoldBadgeChallenges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _03_Badges_Tests
{
    [TestClass]
    public class BadgeRepository_Tests
    {
        [TestMethod]
        public void Test_AddBadge()
        {
            //Arrange
            BadgeRepository repo = new BadgeRepository();

            //Act
            int originalBadgeCount = repo.GetBadgeCount();
            repo.AddBadge(1234, new List<string>());

            //Assert
            Assert.AreNotEqual(originalBadgeCount, repo.GetBadgeCount());
        }
        [TestMethod]
        public void Test_GetDoors()
        {
            //Arrange
            BadgeRepository repo = new BadgeRepository();
            List<string> doors = new List<string>();
            doors.Add("A1");
            doors.Add("A2");
            doors.Add("A3");
            repo.AddBadge(1234, doors);

            //Act
            List<string> badgesDoors = repo.GetDoors(1234);

            //Assert
            Assert.AreEqual(doors, badgesDoors);
        }
        [TestMethod]
        public void Test_UpdateBadge()
        {
            //Arrange
            BadgeRepository repo = new BadgeRepository();
            List<string> doors = new List<string>();
            doors.Add("A1");
            doors.Add("A2");
            doors.Add("A3");
            repo.AddBadge(1234, doors);

            List<string> doors2 = new List<string>();
            doors2.Add("A1");
            doors2.Add("A2");
            doors2.Add("A3");
            doors2.Add("A4");
            List<string> oldDoors = repo.GetDoors(1234);

            //Act
            repo.UpdateBadge(1234,doors2);

            List<string> newDoors = repo.GetDoors(1234);

            //Assert
            Assert.AreNotEqual(oldDoors, newDoors);
        }
        [TestMethod]
        public void Test_DeleteBadge()
        {
            //Arrange
            BadgeRepository repo = new BadgeRepository();            
            repo.AddBadge(1234, new List<string>());
            int badgeCount = repo.GetBadgeCount();

            //Act
            repo.DeleteBadge(1234);

            //Assert
            Assert.AreNotEqual(repo.GetBadgeCount(), badgeCount);
        }
        [TestMethod]
        public void Test_GetBadgeList()
        {
            //Arrange
            BadgeRepository repo = new BadgeRepository();
            List<string> doors = new List<string>();
            doors.Add("A1");
            doors.Add("A2");
            doors.Add("A3");
            repo.AddBadge(1234, doors);
            repo.AddBadge(5678, doors);

            //Act
            List<int> badgeIDs = repo.GetBadgeList();

            //Assert
            Assert.AreEqual(badgeIDs.Count,2);
            Assert.AreEqual(badgeIDs[0], 1234);
            Assert.AreEqual(badgeIDs[1], 5678);
        }
        [TestMethod]
        public void Test_GetDoorList()
        {
            //Arrange
            BadgeRepository repo = new BadgeRepository();
            List<string> doors = new List<string>();
            doors.Add("A1");
            doors.Add("A2");
            doors.Add("A3");
            repo.AddBadge(1234, doors);

            //Act
            string ampersandTest = repo.GetDoorList(1234, " & ");
            string commaTest = repo.GetDoorList(1234, ",");

            //Assert
            Assert.AreEqual(ampersandTest,"A1 & A2 & A3");
            Assert.AreEqual(commaTest,"A1,A2,A3");
        }
        [TestMethod]
        public void Test_BadgeExists()
        {
            //Arrange
            BadgeRepository repo = new BadgeRepository();            
            repo.AddBadge(1234, new List<string>());

            //Act
            bool userExists = repo.BadgeExists(1234);
            bool userDoesNotExist = repo.BadgeExists(5678);

            //Assert
            Assert.IsTrue(userExists);
            Assert.IsFalse(userDoesNotExist);
        }
    }
}
