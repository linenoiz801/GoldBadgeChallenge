using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_CafeMenu_Repository
{
    public class MenuRepository
    {
        public Dictionary<int, MenuItem> Menu = new Dictionary<int, MenuItem>();

        public void AddMeal(int mealNumber, string name, string description, List<string> ingredients, decimal price)
        {
            if (!Menu.ContainsKey(mealNumber))
            {
                Menu.Add(mealNumber, new MenuItem(mealNumber, name, description, ingredients, price));
            }
        }
        public void DeleteMeal(int mealNumber)
        {
            if (Menu.ContainsKey(mealNumber))
            {
                Menu.Remove(mealNumber);
            }
        }
        public List<string> GetIngredients(int mealNumber)
        {
            if (Menu.ContainsKey(mealNumber))
            {
                return Menu[mealNumber].Ingredients;
            }
            else
            {
                return new List<string>();
            }
        }
        public List<int> GetMenuNumbers()
        {
            List<int> result = new List<int>();
            foreach (KeyValuePair<int,MenuItem> item in Menu)
            {
                result.Add(item.Key);
            }
            return result;
        }
        public int GetMenuCount()
        {
            return Menu.Count;
        }
        public bool MealNumberExists(int mealNumber)
        {
            return Menu.ContainsKey(mealNumber);
        }
        public string GetMealName(int mealNumber)
        {
            if (Menu.ContainsKey(mealNumber))
            {
                return Menu[mealNumber].Name;
            }
            else
                return "";
        }        
        public string GetMealDescription(int mealNumber)
        {
            if (Menu.ContainsKey(mealNumber))
            {
                return Menu[mealNumber].Description;
            }
            else
                return "";
        }
        public decimal GetMealPrice(int mealNumber)
        {
            if (Menu.ContainsKey(mealNumber))
            {
                return Menu[mealNumber].Price;
            }
            else
                return 0.0m;
        }
    }
}
