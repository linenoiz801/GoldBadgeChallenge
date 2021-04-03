using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_CafeMenu_Repository;


namespace _01_CafeMenu
{
    class ProgramUI
    {
        MenuRepository repo = new MenuRepository();
        public void Run()
        {
            int i = Menu();
            while (i >= 0)
            {
                switch (i)
                {
                    case 1:
                        AddMenuItem();
                        break;
                    case 2:
                        DeleteMenuItem();
                        break;
                    case 3:
                        DisplayMenuItems();
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

        private int Menu()
        {
            Console.Clear();
            Console.WriteLine("Greetings Cafe Manager. What would you like to do?");
            Console.WriteLine("  1) Add a Menu Item");
            Console.WriteLine("  2) Delete an existing Menu Item");
            Console.WriteLine("  3) List all existing Menu Items");
            Console.WriteLine("  4) Quit");

            if (int.TryParse(Console.ReadLine(), out int selection))
                return selection;
            else
                return 0;
        }

        private void AddMenuItem()
        {
            Console.WriteLine("Adding menu item...");
            Console.Write("Enter meal number: ");
            if (int.TryParse(Console.ReadLine(), out int mealNumber))
            {
                if (repo.MealNumberExists(mealNumber))
                {
                    Console.WriteLine("Error adding meal: meal number already exists.");
                }
                else
                {
                    Console.Write("Enter meal name: ");
                    string mealName = Console.ReadLine();
                    Console.Write("Enter meal description: ");
                    string description = Console.ReadLine();
                    Console.Write("Enter meal price: ");
                    decimal price;
                    while (!decimal.TryParse(Console.ReadLine(), out price))
                    {
                        Console.Write("Error reading input. Please enter valid price: ");
                    }
                    List<string> ingredients = new List<string>();
                    string repeat = "y";
                    while ((repeat.Length >= 1) && (repeat[0] == 'y'))
                    {
                        Console.WriteLine("Enter meal ingredient: ");
                        ingredients.Add(Console.ReadLine());
                        Console.WriteLine("Enter additional ingredient (y/N)?");
                        repeat = Console.ReadLine().ToLower();
                    }
                    repo.AddMeal(mealNumber, mealName, description, ingredients, price);
                    Console.WriteLine("Meal added.");
                }
            }
            else
                Console.WriteLine("Invalid meal number. Enter a valid integer value.");

            Console.ReadLine();
        }
        private void DeleteMenuItem()
        {
            Console.WriteLine("Deleteing menu item...");
            Console.Write("Enter meal number to delete: ");
            if (int.TryParse(Console.ReadLine(), out int mealNumber))
            {
                if (repo.MealNumberExists(mealNumber))
                {
                    Console.WriteLine($"#{mealNumber} - {repo.GetMealName(mealNumber)} will be deleted. Are you sure (y/N)?");
                    if (Console.ReadLine().ToLower()[0] == 'y')
                    {
                        repo.DeleteMeal(mealNumber);
                        Console.WriteLine("Meal deleted.");
                    }
                }
                else
                    Console.WriteLine("Meal number not found.");
            }
            else
                Console.WriteLine("Invalid meal number. Enter a valid integer value.");

            Console.ReadLine();
        }
        private void DisplayMenuItems()
        {
            Console.WriteLine("Displaying menu items...");
            List<int> menuNumbers = repo.GetMenuNumbers();
            List<string> ingredients;
            Console.WriteLine("#\tName\t\tDescription\tPrice");
            foreach (int item in menuNumbers)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.Write($"{item}\t{repo.GetMealName(item)}\t\t{repo.GetMealDescription(item)}\t{repo.GetMealPrice(item)}\n");
                ingredients = repo.GetIngredients(item);
                foreach (string ingredient in ingredients)
                {
                    Console.WriteLine($"\t{ingredient}");
                }
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}

