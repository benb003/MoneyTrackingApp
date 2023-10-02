using System.Text;

namespace MoneyTrackingApp;

public class ItemRepository
{
    private static List<Item> items = new List<Item>();
    private static string directory = @"/Users/hamzab/RiderProjects/MoneyTrackingApp/";
    private static string fileName = "items.txt";
    public static int Balance { get; set; }

    public static void AddItem()
    {
        Console.WriteLine("Do you want to create an income or expense? (for income press i for expense press e)");
        Console.Write("Your selection : ");
        string typeOfItem = Console.ReadLine() ?? String.Empty;
        if (!(typeOfItem.ToLower() == "i" || typeOfItem.ToLower() == "e"))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid Selection. Try Again.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.Write("Enter title : ");
        string title = Console.ReadLine() ?? String.Empty;
        Console.Write("Enter amount : ");
        bool isValidAmount = int.TryParse(Console.ReadLine(), out int amount);
        if (!isValidAmount)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid amount. Try Again.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        Console.Write("Enter date (format : yyyy-mm-dd) : ");
        bool isValidDate = DateTime.TryParse(Console.ReadLine(), out DateTime date);
        if (!isValidDate)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Invalid date. Try Again.");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        if (typeOfItem == "e")
        {
            Item item = new Item(title, amount, date, ItemType.Expense);
            ItemRepository.SaveItem(item);
            ItemRepository.Balance -= amount;
        }
        else if (typeOfItem == "i")
        {
            Item item = new Item(title, amount, date, ItemType.Income);
            ItemRepository.SaveItem(item);
            ItemRepository.Balance += amount;
        }
        else
        {
            Console.WriteLine("something wrong");
        }

        Console.WriteLine("Your item was added.");
    }

    public static void SaveItem(Item item)
    {
        items.Add(item);
    }

    public static void ShowAllItems()
    {
        List<Item> sortedItems = items.OrderBy(i => i.Date).Reverse().ToList();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("ITEM TYPE".PadRight(15) + "TITLE".PadRight(15) + "AMOUNT".PadRight(15) +
                          "DATE".PadRight(15));
        Console.ForegroundColor = ConsoleColor.White;
        foreach (var item in sortedItems)
        {
            Console.WriteLine(item);
        }
    }

    public static void SaveItems()
    {
        string path = $"{directory}{fileName}";
        StringBuilder sb = new StringBuilder();

        foreach (var item in items)
        {
            string type = GetItemType(item);

            sb.Append($"title:{item.Title};");
            sb.Append($"amount:{item.Amount};");
            sb.Append($"date:{item.Date};");
            sb.Append($"type:{type};");
            sb.Append(Environment.NewLine);
        }

        File.WriteAllText(path, sb.ToString());

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Items saved successfully");
        Console.WriteLine("File path is : " + path);
        Console.ResetColor();
    }

    private static string GetItemType(Item item)
    {
        return item.Type == ItemType.Expense ? "1" : "2";
    }

    public static void LoadItems()
    {
        string path = $"{directory}{fileName}";
        if (File.Exists(path))
        {
            items.Clear();
            ItemRepository.Balance = 0;

            string[] itemsAsString = File.ReadAllLines(path);
            for (int i = 0; i < itemsAsString.Length; i++)
            {
                string[] itemSplits = itemsAsString[i].Split(';');
                string title = itemSplits[0].Substring(itemSplits[0].IndexOf(':') + 1);
                int amount = int.Parse(itemSplits[1].Substring(itemSplits[1].IndexOf(':') + 1));
                DateTime date = DateTime.Parse(itemSplits[2].Substring(itemSplits[2].IndexOf(':') + 1));
                string type = itemSplits[3].Substring(itemSplits[3].IndexOf(':') + 1);
                Item item;
                if (type == "1")
                {
                    item = new Item(title, amount, date, ItemType.Expense);
                    ItemRepository.Balance -= amount;
                }
                else
                {
                    item = new Item(title, amount, date, ItemType.Income);
                    ItemRepository.Balance += amount;
                }

                items.Add(item);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loaded {items.Count} items!");
            Console.ResetColor();
        }
    }

    public static void ViewOnlyExpenses()
    {
        List<Item> expenseItems = items.FindAll(i => i.Type == ItemType.Expense).OrderBy(i => i.Date).ToList();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("ITEM TYPE".PadRight(15) + "TITLE".PadRight(15) + "AMOUNT".PadRight(15) +
                          "DATE".PadRight(15));
        Console.ForegroundColor = ConsoleColor.White;
        foreach (var item in expenseItems)
        {
            Console.WriteLine(item);
        }
    }

    public static void ViewOnlyIncomes()
    {
        List<Item> incomeItems = items.FindAll(i => i.Type == ItemType.Income).OrderBy(i => i.Date).ToList();
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("ITEM TYPE".PadRight(15) + "TITLE".PadRight(15) + "AMOUNT".PadRight(15) +
                          "DATE".PadRight(15));
        Console.ForegroundColor = ConsoleColor.White;
        foreach (var item in incomeItems)
        {
            Console.WriteLine(item);
        }
    }

    public static void EditItem()
    {
        Console.Write("Enter title : ");
        string searchedItem = Console.ReadLine() ?? String.Empty;
        var item = items.SingleOrDefault(item => item.Title == searchedItem);
        if (item != null)
        {
            items.Remove(item);
            Console.WriteLine(
                "Do you want to chance to an income or expense? (for income press i for expense press e)");
            Console.Write("Your selection : ");
            string typeOfItem = Console.ReadLine() ?? String.Empty;
            if (!(typeOfItem.ToLower() == "i" || typeOfItem.ToLower() == "e"))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid Selection. Try Again.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.Write("Enter new title : ");
            string title = Console.ReadLine() ?? String.Empty;
            Console.Write("Enter new amount : ");
            bool isValidAmount = int.TryParse(Console.ReadLine(), out int amount);
            if (!isValidAmount)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid amount. Try Again.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.Write("Enter new date (format : yyyy-mm-dd) : ");
            bool isValidDate = DateTime.TryParse(Console.ReadLine(), out DateTime date);
            if (!isValidDate)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid date. Try Again.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (typeOfItem == "e")
            {
                Item newItem = new Item(title, amount, date, ItemType.Expense);
                ItemRepository.SaveItem(newItem);
                ItemRepository.Balance -= amount;
            }
            else if (typeOfItem == "i")
            {
                Item newItem = new Item(title, amount, date, ItemType.Income);
                ItemRepository.SaveItem(newItem);
                ItemRepository.Balance += amount;
            }
            else
            {
                Console.WriteLine("something wrong");
            }

            Console.WriteLine("Your item was updated.");
            return;
        }

        Console.WriteLine("Item was not found.");
    }

    public static void RemoveItem()
    {
        Console.Write("Enter title : ");
        string searchedItem = Console.ReadLine() ?? String.Empty;
        var item = items.SingleOrDefault(item => item.Title == searchedItem);
        if (item != null)
        {
            items.Remove(item);
            Console.WriteLine("Item was removed.");
            return;
        }

        Console.WriteLine("Item was not found.");
    }
}