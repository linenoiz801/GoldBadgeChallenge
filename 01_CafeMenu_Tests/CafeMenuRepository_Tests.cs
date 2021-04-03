using _01_CafeMenu_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _01_CafeMenu_Tests
{
    [TestClass]
    public class CafeMenuRepository_Tests
    {
        private MenuRepository repo;
        [TestInitialize]
        public void Arrange()
        {
            repo = new MenuRepository();
            repo.AddMeal(1, "test1", "description1", new List<string>(), 1.99m);
            repo.AddMeal(2, "test2", "description2", new List<string>(), 2.99m);
        }
        [TestMethod]
        public void Test_AddMeal()
        {
            //Arrange
            int menuCount = repo.GetMenuCount();

            //Act
            repo.AddMeal(3, "test3", "description3", new List<string>(), 3.99m);

            //Assert
            Assert.AreNotEqual(menuCount, repo.GetMenuCount());
            Assert.IsTrue(repo.MealNumberExists(3));
        }
        [TestMethod]
        public void Test_DeleteMeal()
        {
            //Arrange
            int menuCount = repo.GetMenuCount();

            //Act
            repo.DeleteMeal(1);

            //Assert
            Assert.AreNotEqual(menuCount, repo.GetMenuCount());
            Assert.IsFalse(repo.MealNumberExists(1));
        }
        [TestMethod]
        public void Test_GetIngredients()
        {
            //Arrange
            List<string> ingredients = new List<string>();
            ingredients.Add("Item 1");
            ingredients.Add("Item 2");
            repo.AddMeal(3, "test3", "description3", ingredients, 3.99m);

            //Act
            List<string> result = repo.GetIngredients(3);

            //Assert
            Assert.AreEqual(ingredients, result);
        }
        [TestMethod]
        public void Test_GetMenuNumbers()
        {
            //Arrange

            //Act
            List<int> menuIDs = repo.GetMenuNumbers();

            //Assert
            Assert.AreEqual(menuIDs.Count, 2);
            Assert.AreEqual(menuIDs[0], 1);
            Assert.AreEqual(menuIDs[1], 2);
        }
        [TestMethod]
        public void Test_MealNumberExists()
        {
            //Arrange

            //Act
            bool oneExists = repo.MealNumberExists(1);
            bool nineExists = repo.MealNumberExists(9);

            //Assert
            Assert.IsTrue(oneExists);
            Assert.IsFalse(nineExists);
        }
        [TestMethod]
        public void Test_GetMealName()
        {
            //Arrange

            //Act            
            string test1 = repo.GetMealName(1);
            string test2 = repo.GetMealName(2);

            //Assert
            Assert.AreEqual(test1, "test1");
            Assert.AreEqual(test2, "test2");
        }
        [TestMethod]
        public void Test_GetMealDescription()
        {
            //Arrange

            //Act            
            string test1 = repo.GetMealDescription(1);
            string test2 = repo.GetMealDescription(2);

            //Assert
            Assert.AreEqual(test1, "description1");
            Assert.AreEqual(test2, "description2");
        }
        [TestMethod]
        public void Test_GetMealPrice()
        {
            //Arrange            

            //Act            
            decimal test1 = repo.GetMealPrice(1);
            decimal test2 = repo.GetMealPrice(2);

            //Assert
            Assert.AreEqual(test1, 1.99m);
            Assert.AreEqual(test2, 2.99m);
        }
    }
}
